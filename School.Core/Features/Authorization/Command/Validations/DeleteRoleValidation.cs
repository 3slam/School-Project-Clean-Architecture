using FluentValidation;
using School.Core.Features.Authorization.Command.Model;
using School.Service.IService;

namespace School.Core.Features.Authorization.Command.Validations
{
    public class DeleteRoleValidation : AbstractValidator<DeleteRoleCommand>
    {
        private readonly IAuthorizationService authorizationService;
        public DeleteRoleValidation(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;

            ApplyCustomValidationsRules();
        }


        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (Key, CancellationToken) =>
                 await authorizationService.IsRoleExistById(Key))
                .WithMessage("The {PropertyName} is not Exist in the system");
        }

    }
}
