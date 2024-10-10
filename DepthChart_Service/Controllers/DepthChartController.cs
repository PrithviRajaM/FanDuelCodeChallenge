using AutoMapper;
using DepthChart_Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DepthChart_Service.Controllers;

public class DepthChartController : BaseController
{
    private IDepthChartBusiness _depthChartBusiness;
    private IMapper _mapper;

    public DepthChartController(
        IDepthChartBusiness depthChartBusiness,
        IMapper mapper
    )
    {
        _depthChartBusiness = depthChartBusiness;
        _mapper = mapper;
    }

    /// <summary>
    /// This endpoint returns all the active players in a specific position in a team
    /// </summary>
    /// <param name="teamId">Unique id of the Team (Guid)</param>
    /// <param name="positionCode">Short code of the Position</param>
    /// <returns>Returns the status and all active players in position</returns>
    [HttpGet("GetPlayerByPosition")]
    public IActionResult GetPlayersByPosition(Guid teamId, string positionCode)
    {
        return GenerateAPIResponse(_depthChartBusiness.GetPlayersInPosition(teamId, positionCode));
    }

    /// <summary>
    /// This endpoint returns all the active players in the lower position depth 
    /// after a specific player in a position
    /// </summary>
    /// <param name="teamId">Unique id of the Team (Guid)</param>
    /// <param name="positionCode">Short code of the Position</param>
    /// <param name="playerId">Unique player Id (Guid)</param>
    /// <returns>Returns the status and backup players in a position</returns>
    [HttpGet("GetBackups")]
    public IActionResult GetBackups(Guid teamId, string positionCode, Guid playerId)
    {
        return GenerateAPIResponse(_depthChartBusiness.GetBackups(teamId, positionCode, playerId));
    }

    /// <summary>
    /// This endpoint returns all the players in a team categorised under position group, positions
    /// and team details
    /// </summary>
    /// <param name="teamId">Unique id of the Team (Guid)</param>
    /// <returns>Return all the info required to populate the Depth chart</returns>
    [HttpGet("GetFullDepthChart")]
    public IActionResult GetFullDepthChart(Guid teamId)
    {
        return GenerateAPIResponse(_depthChartBusiness.GetFullDepthChart(teamId));
    }

}
