using Betting.Application.Interfaces;
using Betting.Domain;
using MediatR;

namespace Betting.Application.Commands.CasinoWagers.Create
{
    public class CreateCasinoWagerCommand : IRequest
    {
        public Guid WagerId { get; set; }
        public Guid GameId { get; set; }
        public decimal Amount { get; set; }
        public Guid BrandId { get; set; }
        public long Duration { get; set; }
        public Guid AccountId { get; set; }
        public string CountryCode { get; set; }
        public string SessionData { get; set; }
        public int NumberOfBets { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid ExternalReferenceId { get; set; }

        public class CreateCasinoWagerCommandHandler : IRequestHandler<CreateCasinoWagerCommand, Unit>
        {
            private readonly IRepository<CasinoWager> _casinoWagerRepository;

            public CreateCasinoWagerCommandHandler(IRepository<CasinoWager> casinoWagerRepository)
            {
                _casinoWagerRepository = casinoWagerRepository;
            }

            public async Task<Unit> Handle(CreateCasinoWagerCommand request, CancellationToken cancellationToken)
            {
                if (IsExisting(request)) return Unit.Value;

                await _casinoWagerRepository.AddAsync(CreateCasinoWager(request), cancellationToken);
                await _casinoWagerRepository.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }

            private static CasinoWager CreateCasinoWager(CreateCasinoWagerCommand request)
            {
                return new CasinoWager
                {
                    Id = request.WagerId,
                    GameId = request.GameId,
                    Amount = request.Amount,
                    BrandId = request.BrandId,
                    Duration = request.Duration,
                    AccountId = request.AccountId,
                    CountryCode = request.CountryCode,
                    SessionData = request.SessionData,
                    NumberOfBets = request.NumberOfBets,
                    TransactionId = request.TransactionId,
                    CreatedDateTime = request.CreatedDateTime,
                    TransactionTypeId = request.TransactionTypeId,
                    ExternalReferenceId = request.ExternalReferenceId
                };
            }

            private bool IsExisting(CreateCasinoWagerCommand request)
            {
                return _casinoWagerRepository.Exists(casinoWager => casinoWager.Id == request.WagerId);
            }
        }
    }
}
