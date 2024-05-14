using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Command.Model;
using School.Core.Features.Authentication.Command.Result;
using School.Core.Features.Authentication.Command.Validations;
using School.Data.Entities;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler(
       IHttpContextAccessor httpContextAccessor,
       IAuthenticationService authenticationService,
       IStringLocalizer<SharedResourses> localizer,
       IMapper mapper,
       SignInManager<User> signInManager,
       UserManager<User> userManager,
       IEmailService emailService
       )

     : ResponseHandler(localizer),
      IRequestHandler<SignInCommand, Response<JwtToken>>,
      IRequestHandler<RefreshTokenCommand, Response<RefreshTokenResult>>,
      IRequestHandler<ConfirmEmailCommand, Response<string>>,
      IRequestHandler<SendResetPasswordCommand, Response<string>>,
      IRequestHandler<ConfirmResetPasswordCommand, Response<string>>,
     IRequestHandler<ResetNewPasswordCommand, Response<string>>
    {

        public async Task<Response<JwtToken>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var validator = new SignInValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<JwtToken>(error);
            }

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return BadRequest<JwtToken>($"User with email {request.Email} is not exist in the system");


            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.Succeeded)
                return BadRequest<JwtToken>("Sign in fail , check password is correct!");

            try
            {
                var result = await authenticationService.GenerateJWTToken(user);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<JwtToken>(ex.Message);
            }
        }

        public async Task<Response<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await authenticationService.RefreshTokenAsync(request.refreshToken);
                var data = mapper.Map<RefreshTokenResult>(result);

                return Success(data);

            }
            catch (Exception ex)
            {
                return BadRequest<RefreshTokenResult>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.CodeConfirmation == null)
                return BadRequest<string>("Error When ConfirmEmail");

            var user = await userManager.FindByIdAsync(request.UserId);
            var confirmEmail = await userManager.ConfirmEmailAsync(user, request.CodeConfirmation);
            if (!confirmEmail.Succeeded)
                return BadRequest<string>("Error When ConfirmEmail");
            return Success("Email Confirmed Successfully");

        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new SendResetPasswordValidation();
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
                return BadRequest<string>("User Not Found");

            Random generator = new Random();
            string randomNumber = generator.Next(0, 1000000).ToString("D6");

            user.Code = randomNumber;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest<string>("Error In Updating User");
            var message = "Code To Reset Passsword : " + user.Code;

            await emailService.SendEmailAsync(user.Email, "Reset Password", message);

            return Success("Check Your Email to see the code we sent it to you.");
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ConfirmResetPasswordvalidation();
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
                return BadRequest<string>("User Not Found");

            var userCode = user.Code;

            if (userCode == request.Code) return Success("Graet!,Now You Can set your new password");
            return BadRequest<string>("Enter The correct code we send it to you.");
        }

        public async Task<Response<string>> Handle(ResetNewPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResetNewPasswordvalidation();
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
                return BadRequest<string>("User Not Found");


            await userManager.RemovePasswordAsync(user);

            var result = await userManager.AddPasswordAsync(user, request.Password);

            if (result.Succeeded) return Success("Successfully you set a new password.");
            return BadRequest<string>("something went wrong!!");
        }
    }

}
