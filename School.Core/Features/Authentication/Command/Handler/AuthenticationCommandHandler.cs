using AutoMapper;
using MediatR;
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
       IAuthenticationService authenticationService,
       IStringLocalizer<SharedResourses> localizer,
       IMapper mapper,
       SignInManager<User> signInManager,
       UserManager<User> userManager
       )

     : ResponseHandler(localizer),
      IRequestHandler<SignInCommand, Response<JwtToken>>,
      IRequestHandler<RefreshTokenCommand, Response<RefreshTokenResult>>
    {
        private readonly IMapper mapper = mapper;
        private readonly IStringLocalizer<SharedResourses> localizer = localizer;
        private readonly SignInManager<User> signInManager = signInManager;
        private readonly UserManager<User> userManager = userManager;
        private readonly IAuthenticationService authenticationService = authenticationService;

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
    }

}
