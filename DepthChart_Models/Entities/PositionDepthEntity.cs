using DepthChart_Models.Constants;

namespace DepthChart_Models.Entities;

/// <summary>
/// A simulated table object to handle position depth details
/// </summary>
public class PositionDepthEntity
{
    public Guid PositionDepthId { get; set; }
    public Guid TeamId { get; set; }
    public int Year { get; set; }
    public int PositionEnumId { get; set; }
    public Guid PlayerId { get; set; }
    public int PositionDepth { get; set; }
    public int StatusEnumId { get; set; } = (int) PlayerStatusEnum.Active;
    public bool IsArchived { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static PositionDepthEntity AddNewPlayer(Guid teamId, int positionEnumId, Guid playerId, int statusEnumId, int positionDepth)
    {
        var currentUTC = DateTime.UtcNow;
        return new PositionDepthEntity
        {
            PositionDepthId = Guid.NewGuid(),
            TeamId = teamId,
            Year = DateTime.UtcNow.Year,
            PositionEnumId = positionEnumId,
            PlayerId = playerId,
            PositionDepth = positionDepth,
            StatusEnumId = statusEnumId,
            CreatedOn = currentUTC,
            UpdatedOn = currentUTC
        };
    }
}
