using Betting.Application.Interfaces;
using Betting.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Betting.Application.Commands.Providers.Create
{
    public class CreateProviderCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
        {
            private readonly IRepository<Provider> _providerRepository;

            public CreateProviderCommandHandler(IRepository<Provider> providerRepository)
            {
                _providerRepository = providerRepository;
            }

            public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
            {
                if (IsExisting(request.Name))
                {
                    var provider = await _providerRepository.Where(provider => provider.Name == request.Name).FirstOrDefaultAsync(cancellationToken);

                    return provider!.Id;
                }

                var newProvider = new Provider { Id = Guid.NewGuid(), Name = request.Name };
                await _providerRepository.AddAsync(newProvider, cancellationToken);
                await _providerRepository.SaveChangesAsync(cancellationToken);

                return newProvider!.Id;
            }

            private bool IsExisting(string providerName)
            {
                return _providerRepository.Exists(provider => provider.Name == providerName);
            }
        }
    }
}
