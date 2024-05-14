using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Command.Model;
using School.Core.Features.Users.Command.Validations;
using School.Data.Entities;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Users.Command.Handler
{
    public class UserCommandHandler(
        IEmailService emailService,

        IHttpContextAccessor httpContextAccessor,
        IAuthenticationService authenticationService,
       IStringLocalizer<SharedResourses> localizer,
       IMapper mapper,
       UserManager<User> userManager)

     : ResponseHandler(localizer),
         IRequestHandler<AddUserCommand, Response<string>>,
         IRequestHandler<DeleteUserCommand, Response<string>>,
         IRequestHandler<ChangeUserPasswordCommand, Response<string>>,
         IRequestHandler<EditUserCommand, Response<string>>

    {
        private readonly IMapper mapper = mapper;
        private readonly IAuthenticationService authenticationService = authenticationService;
        private readonly IStringLocalizer<SharedResourses> localizer = localizer;
        private readonly UserManager<User> userManager = userManager;


        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddUserValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                var userAfterMapping = mapper.Map<User>(request);
                userAfterMapping.RefreshToken = (await authenticationService.GenerateJWTToken(userAfterMapping)).RefreshToken;
                var result = await userManager
                    .CreateAsync(userAfterMapping, request.Password);

                if (result.Succeeded)
                {

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(userAfterMapping);
                    var resquestAccessor = httpContextAccessor.HttpContext.Request;


                    // this code is not correct we need to add Url.Action(contrlloer , action , pars)
                    var url = "ConfirmEmail";

                    var returnUrl = resquestAccessor.Scheme + $"://localhost:7016/Api/v1/" + url;
                    var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                    await emailService.SendEmailAsync(request.Email, "Confirm Email", message, true);
                    return Success("created successfully go to your email to confirm...");
                }
                return BadRequest<string>($"Some thing went wrong , {result.Errors.FirstOrDefault().Description}");
            }
            else
            {
                return BadRequest<string>("This User already exist");
            }
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user == null)
                return NotFound<string>("User does not exist in the system");

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded) return Success("The User Deleted Successfully");

            return BadRequest<string>($"Some thing went wrong , {result.Errors.FirstOrDefault().Description}");

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeUserPasswordValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }



            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return NotFound<string>("User does not exist in the system");

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return Success("Change User Password is completed successfully");

            return BadRequest<string>(result.Errors.FirstOrDefault().Description);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditUserValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return NotFound<string>("User does not exist in the system");

            //  var newUser = mapper.Map(request, user);

            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Address = request.Address != null ? request.Address : user.Address;
            user.PhoneNumber = request.PhoneNumber != null ? request.PhoneNumber : user.PhoneNumber;
            user.Country = request.Country != null ? request.Country : user.Country;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return Success("The User Updated Successfully");

            return BadRequest<string>($"Some thing went wrong , {result.Errors.FirstOrDefault().Description}");

        }


    }
}
