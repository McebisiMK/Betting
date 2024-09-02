using FluentValidation;

namespace Betting.Application.Queries.CasinoWagers.TopSpenders
{
    public class GetTopSpendersQueryValidator : AbstractValidator<GetTopSpendersQuery>
    {
        public GetTopSpendersQueryValidator()
        {
            RuleFor(request => request.Count)
             .NotEmpty()
             .WithMessage("Count is required")
             .GreaterThan(0)
             .WithMessage("Count must be greater than zero");
        }
    }
}
