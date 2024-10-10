namespace DepthChart_Models.DTOs;

/// <summary>
/// Data transfer object to relay basic details of a player
/// </summary>
public class Player
{
    public int PlayerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DepthChartkey { get; set; }
}
