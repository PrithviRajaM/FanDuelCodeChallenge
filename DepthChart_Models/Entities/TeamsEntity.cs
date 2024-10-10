namespace DepthChart_Models.Entities;

/// <summary>
/// A simulated table object to handle different Teams and its details
/// </summary>
public class TeamsEntity
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; }
    public string TeamDescription { get; set; }
    public string TeamShortName { get; set; }
    public string TeamLocationTimeZone { get; set; }
    public Guid SportId { get; set; }
    public string GeneralManager { get; set; }
    public string HeadCoach { get; set; }
    public string OffenseCoordinator { get; set; }
    public string DefenseCoordinator { get; set; }
    public string SpecialTeamsCoordinator { get; set; }
    public bool IsDeleted { get; set; }

}
