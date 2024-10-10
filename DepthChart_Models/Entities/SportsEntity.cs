namespace DepthChart_Models.Entities;

/// <summary>
/// A simulated table object to handle different Sport
/// </summary>
public class SportsEntity
{
    public Guid SportId { get; set; }
    public string SportName { get; set; }
    public string SportShortName { get; set; }
    public bool IsDeleted { get; set; }
}
