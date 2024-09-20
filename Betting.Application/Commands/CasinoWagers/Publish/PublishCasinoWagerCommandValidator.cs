using Betting.Application.Interfaces;
using FluentValidation;

namespace Betting.Application.Commands.CasinoWagers.Publish
{
    public class PublishCasinoWagerCommandValidator : AbstractValidator<PublishCasinoWagerCommand>
    {
        private readonly IRepository<Domain.CasinoWager> _casinoWagerRepository;

        public PublishCasinoWagerCommandValidator(IRepository<Domain.CasinoWager> casinoWagerRepository)
        {
            _casinoWagerRepository = casinoWagerRepository;

            RuleFor(request => request.WagerId)
             .NotEmpty()
             .WithMessage("WagerId is required")
             .Must(NotBeAnExistingWager)
             .WithMessage("Given wager was already publish and processed succefully");

            RuleFor(request => request.Theme)
             .NotEmpty()
             .WithMessage("Theme is required");

            RuleFor(request => request.Provider)
             .NotEmpty()
             .WithMessage("Provider is required");

            RuleFor(request => request.GameName)
             .NotEmpty()
             .WithMessage("GameName is required");

            RuleFor(request => request.TransactionId)
             .NotEmpty()
             .WithMessage("TransactionId is required");

            RuleFor(request => request.BrandId)
             .NotEmpty()
             .WithMessage("BrandId is required");

            RuleFor(request => request.AccountId)
             .NotEmpty()
             .WithMessage("AccountId is required");

            RuleFor(request => request.Username)
             .NotEmpty()
             .WithMessage("Username is required");

            RuleFor(request => request.ExternalReferenceId)
             .NotEmpty()
             .WithMessage("ExternalReferenceId is required");

            RuleFor(request => request.TransactionTypeId)
             .NotEmpty()
             .WithMessage("TransactionTypeId is required");

            RuleFor(request => request.Amount)
             .NotEmpty()
             .WithMessage("Amout is required")
             .GreaterThan(0)
             .WithMessage("Amount must be greater than zero");

            RuleFor(request => request.CreatedDateTime)
             .NotEmpty()
             .WithMessage("CreatedDateTime is required");

            RuleFor(request => request.CreatedDateTime)
             .NotEmpty()
             .WithMessage("CreatedDateTime is required");

            RuleFor(request => request.NumberOfBets)
             .NotEmpty()
             .WithMessage("NumberOfBets is required")
             .GreaterThan(0)
             .WithMessage("NumberOfBets must be greater than zero");

            RuleFor(request => request.CountryCode)
             .NotEmpty()
             .WithMessage("CountryCode is required");

            RuleFor(request => request.SessionData)
             .NotEmpty()
             .WithMessage("SessionData is required");

            RuleFor(request => request.Duration)
             .NotEmpty()
             .WithMessage("Duration is required")
             .GreaterThan(0)
             .WithMessage("Duration must be greater than zero");
        }

        private bool NotBeAnExistingWager(Guid wagerId)
        {
            return !(_casinoWagerRepository.Exists(wager => wager.Id == wagerId));
        }
    }
}
