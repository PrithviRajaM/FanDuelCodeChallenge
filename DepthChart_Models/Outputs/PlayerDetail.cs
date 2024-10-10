using DepthChart_Models.Entities;

namespace DepthChart_Models.Outputs;

/// <summary>
/// An output object to relay player details as an endpoint response
/// </summary>
public class PlayerDetail
{
    public Guid PlayerId { get; set; }
    public int PlayerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DepthChartkey { get; set; }
    public int PositionDepth { get; set; }

    public static void SetDetails(PlayerDetail detail, PlayerEntity entity)
    {
        detail.PlayerNumber = entity.PlayerNumber;
        detail.FirstName = entity.FirstName;
        detail.LastName = entity.LastName;
        detail.DepthChartkey = entity.DepthChartkey;
    }
}
