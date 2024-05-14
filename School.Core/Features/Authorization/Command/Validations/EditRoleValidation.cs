using FluentValidation;
using School.Core.Features.Authorization.Command.Model;
using School.Service.IService;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class EditRoleValidation : AbstractValidator<EditRoleCommand>
    {
        private readonly IAuthorizationService authorizationService;
        public EditRoleValidation(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The {PropertyName} must be not empty")
                .NotNull().WithMessage("The {PropertyName} must be not empty");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("The {PropertyName} must be not empty") // false
                .NotNull().WithMessage("The {PropertyName} must be not empty");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) =>
                !await authorizationService.IsRoleExistByName(Key))
                .WithMessage("The {PropertyName} is already Exist in the system");

            RuleFor(x => x.Id)
               .MustAsync(async (Key, CancellationToken) =>
                await authorizationService.IsRoleExistById(Key))
               .WithMessage("The {PropertyName} is already Exist in the system");
        }


    }
}
