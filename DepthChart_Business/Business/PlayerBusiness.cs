using DepthChart_Business.Interfaces;
using DepthChart_Models.Extensions;

namespace DepthChart_Business.Business;

public class PlayerBusiness : IPlayerBusiness
{
    public PlayerBusiness() { }

    public void GetPlayers()
    {
        PlayerPositionsExtention.GetPlayerPositionByCode("OB");
    }
}
