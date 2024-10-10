using AutoMapper;
using DepthChart_Models.Entities;
using DepthChart_Models.Outputs;

namespace DepthChart_Models.MappingProfiles;

/// <summary>
/// Auto mapper profile class to transfer data between objects
/// </summary>
public class PlayerMapProfile : Profile
{
    public PlayerMapProfile()
    {
        CreateMap<PositionDepthEntity, PlayerDetail>()
            .ForMember(s => s.PlayerId, m => m.MapFrom(o => o.PlayerId))
            .ForMember(s => s.PositionDepth, m => m.MapFrom(o => o.PositionDepth));
        CreateMap<PlayerEntity, PlayerDetail>()
            .ForMember(s => s.PlayerNumber, m => m.MapFrom(o => o.PlayerNumber))
            .ForMember(s => s.FirstName, m => m.MapFrom(o => o.FirstName))
            .ForMember(s => s.LastName, m => m.MapFrom(o => o.LastName))
            .ForMember(s => s.DepthChartkey, m => m.MapFrom(o => o.DepthChartkey));
    }
}
