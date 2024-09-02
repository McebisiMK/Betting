using AutoMapper;
using Betting.Common.DTOs;
using Betting.Domain;

namespace Betting.Application.Mapping
{
    public class BettingMappingProfile : Profile
    {
        public BettingMappingProfile()
        {
            CreateMap<Game, CasinoWagerDTO>()
             .ForMember(dest => dest.Game, config => config.MapFrom(src => src.Name))
             .AfterMap((src, dest, context) => context.Mapper.Map(src.Provider, dest));

            CreateMap<Provider, CasinoWagerDTO>()
             .ForMember(dest => dest.Provider, config => config.MapFrom(src => src.Name));

            CreateMap<CasinoWager, CasinoWagerDTO>()
             .ForMember(dest => dest.WagerId, config => config.MapFrom(src => src.Id))
             .ForMember(dest => dest.CreatedDate, config => config.MapFrom(src => src.CreatedDateTime))
             .AfterMap((src, dest, context) => context.Mapper.Map(src.Game, dest));
        }
    }
}
