namespace DepthChart_Models.Entities;

/// <summary>
/// This object simulates as a DB schema
/// </summary>
public class DepthChartSchema
{
    public List<SportsEntity> Sports { get; set; } = new List<SportsEntity>();
    public List<TeamsEntity> Teams { get; set; } = new List<TeamsEntity>();
    public List<PlayerEntity> Players { get; set; } = new List<PlayerEntity>();
    public List<PositionDepthEntity> PositionDepths { get; set; } = new List<PositionDepthEntity>();
}
