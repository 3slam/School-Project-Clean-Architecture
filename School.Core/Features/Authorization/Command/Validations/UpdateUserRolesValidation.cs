using FluentValidation;
using School.Core.Features.Authorization.Command.Model;
using School.Service.IService;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class UpdateUserRolesValidation : AbstractValidator<UpdateUserRolesCommand>
    {
        private readonly IAuthorizationService authorizationService;

        public UpdateUserRolesValidation(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Must be not empty")
                 .NotNull().WithMessage("{PropertyName} requerid");

            RuleFor(x => x.Roles).NotEmpty().WithMessage("Must be not empty")
               .NotNull().WithMessage("{PropertyName} requerid");


            RuleFor(x => x.Roles)
               .MustAsync(async (Key, CancellationToken) =>
                await authorizationService.AreRolesExistById(Key))
               .WithMessage("Some The ids of roles is not exist in the system");

            RuleFor(x => x.Roles)
            .MustAsync(async (Key, CancellationToken) =>
             await authorizationService.AreRolesExistByName(Key))
            .WithMessage("Some The names of roles is not exist in the system");
        }

    }
}
