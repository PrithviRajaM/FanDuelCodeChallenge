using DepthChart_Models.DTOs;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Interfaces;

public interface IDepthChartBusiness
{
    public BusinessResult<PlayersInPosition> GetPlayersInPosition(Guid teamId, string positionCode);
    public BusinessResult<PlayersInPosition> GetBackups(Guid teamId, string positionCode, Guid playerId);
    public BusinessResult<FullDepthChart> GetFullDepthChart(Guid teamId);
}
