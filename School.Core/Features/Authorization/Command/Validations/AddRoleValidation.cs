using FluentValidation;
using School.Core.Features.Authorization.Command.Model;
using School.Service.IService;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class AddRoleValidation : AbstractValidator<AddRoleCommand>
    {
        private readonly IAuthorizationService authorizationService;
        public AddRoleValidation(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("The {PropertyName} must be not empty")
                .NotNull().WithMessage("The {PropertyName} must be not empty");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) =>
                 !await authorizationService.IsRoleExistByName(Key)) // yes - no
                .WithMessage("The {PropertyName} is already Exist in the system");
        }


    }
}
