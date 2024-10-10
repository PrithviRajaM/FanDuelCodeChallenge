namespace DepthChart_Models.Inputs;

/// <summary>
/// Input object used to relay input parameters for remove player action
/// </summary>
public class RemovePlayerInput
{
    public required Guid TeamId { get; set; }
    public required string PositionCode { get; set; }
    public Guid PlayerId { get; set; }
}