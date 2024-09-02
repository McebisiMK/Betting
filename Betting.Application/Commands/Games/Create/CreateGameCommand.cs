using Betting.Application.Interfaces;
using Betting.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Betting.Application.Commands.Games.Create
{
    public class CreateGameCommand : IRequest<Guid>
    {
        public Guid ProdiverId { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }

        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
        {
            private readonly IRepository<Game> _gameRepository;

            public CreateGameCommandHandler(IRepository<Game> gameRepository)
            {
                _gameRepository = gameRepository;
            }

            public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                if (IsExisting(request.Name))
                {
                    var game = await _gameRepository.Where(game => game.Name == request.Name).FirstOrDefaultAsync(cancellationToken);

                    return game!.Id;
                }

                var newGame = new Game { Id = Guid.NewGuid(), Name = request.Name, Theme = request.Theme, ProviderId = request.ProdiverId };
                await _gameRepository.AddAsync(newGame, cancellationToken);
                await _gameRepository.SaveChangesAsync(cancellationToken);

                return newGame!.Id;
            }

            private bool IsExisting(string gameName)
            {
                return _gameRepository.Exists(game => game.Name == gameName);
            }
        }
    }
}
