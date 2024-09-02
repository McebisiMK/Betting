using Betting.Application.Commands.Accounts.Create;
using Betting.Application.Commands.CasinoWagers.Create;
using Betting.Application.Commands.Games.Create;
using Betting.Application.Commands.Providers.Create;
using Betting.Application.Interfaces;
using Betting.Domain;
using MediatR;

namespace Betting.Application.Commands.CasinoWagers.Consume
{
    public class ConsumeCasinoWagerCommand : IRequest
    {
        public Guid WagerId { get; set; }
        public string Theme { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
        public Guid TransactionId { get; set; }
        public Guid BrandId { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
        public Guid ExternalReferenceId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string SessionData { get; set; } = string.Empty;
        public long Duration { get; set; }

        public class ConsumeCasinoWagerCommandHandler : IRequestHandler<ConsumeCasinoWagerCommand, Unit>
        {
            private readonly IMediator _mediator;

            public ConsumeCasinoWagerCommandHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ConsumeCasinoWagerCommand request, CancellationToken cancellationToken)
            {
                await _mediator.Send(new CreateAccountCommand { Id = request.AccountId, Username = request.Username }, cancellationToken);
                var providerId = await _mediator.Send(new CreateProviderCommand { Name = request.Provider }, cancellationToken);
                var gameId = await _mediator.Send(new CreateGameCommand { Name = request.GameName, Theme = request.Theme, ProdiverId = providerId }, cancellationToken);
                await _mediator.Send(CreateCasinoWagerCommand(request, gameId), cancellationToken);

                return Unit.Value;
            }

            private static CreateCasinoWagerCommand CreateCasinoWagerCommand(ConsumeCasinoWagerCommand request, Guid gameId)
            {
                return new CreateCasinoWagerCommand
                {
                    GameId = gameId,
                    Amount = request.Amount,
                    WagerId = request.WagerId,
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
        }
    }
}
