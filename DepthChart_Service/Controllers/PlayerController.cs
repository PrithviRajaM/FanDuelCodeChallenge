using AutoMapper;
using DepthChart_Business.Interfaces;
using DepthChart_Models.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace DepthChart_Service.Controllers;

/// <summary>
/// Player Controller to handle player related transactions
/// </summary>
public class PlayerController : BaseController
{
    private IPlayerBusiness _playerBusiness;
    private IMapper _mapper;

    public PlayerController(IPlayerBusiness playerBusiness, IMapper mapper)
    {
        _playerBusiness = playerBusiness;
        _mapper = mapper;
    }

    /// <summary>
    /// This endpoint adds player to a specific position in a team
    /// </summary>
    /// <param name="inputData">Input data contains details of the player, position and team</param>
    /// <returns>Returns the status and all active players in position</returns>
    [HttpPost("AddPlayer")]
    public IActionResult AddPlayer([FromBody] AddPlayerInput inputData)
    {
        return GenerateAPIResponse(_playerBusiness.AddPlayer(inputData));
    }

    /// <summary>
    /// This endpoint removes a player from a specific position in a team
    /// </summary>
    /// <param name="inputData">Input data contains details of the player, position and team</param>
    /// <returns>Returns the status and all active players in position</returns>
    [HttpPost("RemovePlayer")]
    public IActionResult RemovePlayer([FromBody] RemovePlayerInput inputData)
    {
        return GenerateAPIResponse(_playerBusiness.RemovePlayer(inputData));
    }
}
