using AutoMapper;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;

namespace DepthChart_Models.Outputs;

/// <summary>
/// An output object to relay player under a position as an endpoint response
/// </summary>
public class PlayersInPosition
{
    public Guid TeamId { get; set; }
    public string PositionCode { get; set; }
    public List<PlayerDetail> Players { get; set; }
}
