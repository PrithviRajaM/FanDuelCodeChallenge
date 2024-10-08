using DepthChart_Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DepthChart_Service.Controllers;

public class DepthChartController : Controller
{
    private IPlayerBusiness _playerBusiness;

    public DepthChartController(IPlayerBusiness playerBusiness)
    {
        _playerBusiness = playerBusiness;
    }

    [HttpGet("GetChart")]
    public IActionResult Get()
    {
        _playerBusiness.GetPlayers();
        return Ok();
    }

}
