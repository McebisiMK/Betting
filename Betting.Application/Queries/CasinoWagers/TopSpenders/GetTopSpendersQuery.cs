using Betting.Application.Interfaces;
using Betting.Common.DTOs;
using Betting.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Betting.Application.Queries.CasinoWagers.TopSpenders
{
    public class GetTopSpendersQuery : IRequest<IList<SpendingDetailDTO>>
    {
        public int Count { get; set; }

        public class GetTopSpendersQueryHandler : IRequestHandler<GetTopSpendersQuery, IList<SpendingDetailDTO>>
        {
            private readonly IRepository<CasinoWager> _casinoWagerRepository;

            public GetTopSpendersQueryHandler(IRepository<CasinoWager> casinoWagerRepository)
            {
                _casinoWagerRepository = casinoWagerRepository;
            }

            public async Task<IList<SpendingDetailDTO>> Handle(GetTopSpendersQuery request, CancellationToken cancellationToken)
            {
                var topSpenders = await _casinoWagerRepository.GetAll()
                                            .Include(casinoWager => casinoWager.Account)
                                            .GroupBy(casinoWager => casinoWager.AccountId)
                                            .OrderByDescending(group => group.Sum(casinoWager => casinoWager.Amount))
                                            .Take(request.Count)
                                            .Select(group => new SpendingDetailDTO
                                            {
                                                AccountId = group.Key,
                                                Username = group.First().Account.Username,
                                                TotlaAmountSpend = group.Sum(wager => wager.Amount)
                                            }).ToListAsync(cancellationToken);

                return topSpenders;
            }
        }
    }
}
