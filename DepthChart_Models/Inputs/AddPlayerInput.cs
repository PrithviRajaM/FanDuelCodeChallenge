#nullable enable
using DepthChart_Models.DTOs;

namespace DepthChart_Models.Inputs;

/// <summary>
/// Input object used to relay input parameters for add player action
/// </summary>
public class AddPlayerInput
{
    public required Guid TeamId { get; set; }
    public required string PositionCode { get; set; }
    public Guid? PlayerId { get; set; }
    public Player? PlayerDetails { get; set; }
    public int? PositionDepth { get; set; }
    public string StatusCode { get; set; } = "Active";
}
