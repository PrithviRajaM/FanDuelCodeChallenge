#nullable enable
using DepthChart_DB.Interface;
using DepthChart_Models.Entities;

namespace DepthChart_DB.Repositories;

public class DepthChartRepository : CoreRepository, IDepthChartRepository
{
    public DepthChartRepository()
    {
        GetSchema();
    }

    // Collect a specific Teams details
    public TeamsEntity? GetTeamDetails(Guid teamId)
        => dbSchema.Teams.FirstOrDefault(x => x.TeamId == teamId);

    // Collects all active players under a team irrespective of any positions
    public List<PositionDepthEntity> GetAllPlayersByTeam(Guid teamId)
        => dbSchema
            .PositionDepths
            .Where(x => !x.IsDeleted
                && !x.IsArchived
                && x.TeamId == teamId
                && x.Year == DateTime.UtcNow.Year)
            .ToList();

    // Collects all past archived date of position depth chnages
    public List<DateTime> GetArchiveDateTimesByTeam(Guid teamId)
        => [.. dbSchema
            .PositionDepths
            .Where(x => x.IsArchived)
            .Select(x => x.UpdatedOn)
            .Distinct()
            .OrderDescending()];
}
