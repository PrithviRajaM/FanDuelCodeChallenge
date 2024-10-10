namespace DepthChart_Models.Outputs;

/// <summary>
/// A collection of objects to consolidate data to populate an entire Depth Chart
/// </summary>
public class FullDepthChart
{
    public string DCTitle { get; set; }
    public TeamManagement TeamManagement { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
    public int? MaxPlayersCountInAPosition { get; set; } = 0;
    public List<PositionTypeSet> PositionTypes { get; set; }
    public List<DateTime> ArchiveDateTimes {  get; set; }
}

public class PositionTypeSet
{
    public string PositionTypeName { get; set; }
    public List<PositionSet> Positions { get; set; }
}

public class PositionSet
{
    public string PositionCode { get; set; }
    public PlayersInPosition Players { get; set; }
    public int PlayerCount { get; set; }
}

public class TeamManagement
{
    public string GeneralManager { get; set; }
    public string HeadCoach { get; set; }
    public string OffenseCoordinator { get; set; }
    public string DefenseCoordinator { get; set; }
    public string SpecialTeamsCoordinator { get; set; }
}
