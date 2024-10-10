namespace DepthChart_Models.Entities;

/// <summary>
/// A simulated table object to handle player details
/// </summary>
public class PlayerEntity
{
    public Guid PlayerId { get; set; }
    public int PlayerNumber { get; set; }
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string DepthChartkey { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static PlayerEntity AddNewPlayer(int playerNumber, string firstName, string lastName, string depthChartkey)
    {
        var currentUTC = DateTime.UtcNow;
        return new PlayerEntity
        {
            PlayerId = Guid.NewGuid(),
            PlayerNumber = playerNumber,
            FirstName = firstName,
            LastName = lastName,
            DepthChartkey = depthChartkey,
            IsDeleted = false,
            CreatedOn = currentUTC,
            UpdatedOn = currentUTC
        };
    }
}
