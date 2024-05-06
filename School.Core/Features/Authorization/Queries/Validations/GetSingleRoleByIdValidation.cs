
using FluentValidation;
using School.Core.Features.Authorization.Queries.Model;
using School.Service.IService;

namespace School.Core.Features.Authorization.Queries.Validations
{
    public class GetSingleRoleByIdValidation : AbstractValidator<GetSingleRoleByIdQuery>
    {

        private readonly IAuthorizationService authorizationService;
        public GetSingleRoleByIdValidation(IAuthorizationService authorizationService)
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
