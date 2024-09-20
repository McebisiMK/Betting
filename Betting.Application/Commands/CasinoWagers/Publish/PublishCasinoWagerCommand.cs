using Betting.Application.Interfaces;
using MediatR;

namespace Betting.Application.Commands.CasinoWagers.Publish
{
    public class PublishCasinoWagerCommand : IRequest
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

        public class PublishCasinoWagerCommandHandler : IRequestHandler<PublishCasinoWagerCommand, Unit>
        {
            private readonly IMessagePublisher _messagePublisher;

            public PublishCasinoWagerCommandHandler(IMessagePublisher messagePublisher)
            {
                _messagePublisher = messagePublisher;
            }

            public async Task<Unit> Handle(PublishCasinoWagerCommand request, CancellationToken cancellationToken)
            {
                _messagePublisher.Publish(request);

                return Unit.Value;
            }
        }
    }
}
