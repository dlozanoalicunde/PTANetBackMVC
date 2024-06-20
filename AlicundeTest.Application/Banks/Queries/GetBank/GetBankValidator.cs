using FluentValidation;

namespace AlicundeTest.Application.Banks.Queries.GetBank;

internal class GetBankValidator : AbstractValidator<GetBankRequest>
{
    GetBankValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
