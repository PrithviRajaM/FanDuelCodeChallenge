using DepthChart_Models.DTOs;
using DepthChart_Models.Inputs;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Interfaces;

public interface IPlayerBusiness
{    
    public BusinessResult<PlayersInPosition> AddPlayer(AddPlayerInput inputData);
    public BusinessResult<PlayersInPosition> RemovePlayer(RemovePlayerInput inputData);    
}
