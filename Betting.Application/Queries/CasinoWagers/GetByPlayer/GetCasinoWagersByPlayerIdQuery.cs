using AutoMapper;
using Betting.Application.Interfaces;
using Betting.Common.DTOs;
using Betting.Common.Responses;
using Betting.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Betting.Application.Queries.CasinoWagers.GetByPlayer
{
    public class GetCasinoWagersByPlayerIdQuery : IRequest<PaginatedResponse<IList<CasinoWagerDTO>>>
    {
        public Guid PlayerId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetCasinoWagersByPlayerIdQueryHandler : IRequestHandler<GetCasinoWagersByPlayerIdQuery, PaginatedResponse<IList<CasinoWagerDTO>>>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<CasinoWager> _casinoWagerRepository;

            public GetCasinoWagersByPlayerIdQueryHandler(IMapper mapper, IRepository<CasinoWager> casinoWagerRepository)
            {
                _mapper = mapper;
                _casinoWagerRepository = casinoWagerRepository;
            }

            public async Task<PaginatedResponse<IList<CasinoWagerDTO>>> Handle(GetCasinoWagersByPlayerIdQuery request, CancellationToken cancellationToken)
            {
                var casinoWagersDB = await _casinoWagerRepository
                                            .Where(casinoWager => casinoWager.AccountId == request.PlayerId)
                                            .Include(casinoWager => casinoWager.Game)
                                                .ThenInclude(game => game.Provider)
                                            .AsNoTracking()
                                            .Skip((request.Page - 1) * request.PageSize)
                                            .Take(request.PageSize)
                                            .ToListAsync(cancellationToken);
                var casinoWagers = _mapper.Map<IList<CasinoWagerDTO>>(casinoWagersDB) ?? [];
                var totalRecords = await _casinoWagerRepository.CountAsync(casinoWager => casinoWager.AccountId == request.PlayerId, cancellationToken: cancellationToken);


                return new PaginatedResponse<IList<CasinoWagerDTO>>(casinoWagers, request.Page, request.PageSize)
                            .AddPagination(totalRecords);
            }
        }
    }
}
