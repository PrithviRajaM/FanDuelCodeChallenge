#nullable enable
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;

namespace DepthChart_DB.Interface;

public interface IDepthChartRepository
{
    public List<PositionDepthEntity> GetCurrentPlayersInPosition(Guid teamId, PlayerPositionsEnum position);

    public TeamsEntity? GetTeamDetails(Guid teamId);

    public List<PositionDepthEntity> GetAllPlayersByTeam(Guid teamId);

    public List<DateTime> GetArchiveDateTimesByTeam(Guid teamId);
}
