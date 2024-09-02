using Betting.Application.Interfaces;
using Betting.Domain;
using MediatR;

namespace Betting.Application.Commands.Accounts.Create
{
    public class CreateAccountCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Unit>
        {
            private readonly IRepository<Account> _accountRepository;

            public CreateAccountCommandHandler(IRepository<Account> accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                if (IsExisting(request.Id)) return Unit.Value;

                await _accountRepository.AddAsync(new Account { Id = request.Id, Username = request.Username, }, cancellationToken);
                await _accountRepository.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            private bool IsExisting(Guid accountId)
            {
                return _accountRepository.Exists(account => account.Id == accountId);
            }
        }
    }
}
