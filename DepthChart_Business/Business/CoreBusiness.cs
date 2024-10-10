using AutoMapper;
using DepthChart_DB.Interface;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Business;

public class CoreBusiness
{
    //Purpose: This method gathers player details for the specific list of players from Position depths
    //Usage: Mostly for endpoint responses
    //Scope: Consumed with in the business classes
    internal BusinessResult<PlayersInPosition> BuildPlayersInPositionResponse(
        IPlayerRepository _playerRepository,
        IMapper _mapper,
        Guid teamId,
        string positionCode,
        List<PositionDepthEntity> players
    )
    {
        var playerIds = players.Select(x => x.PlayerId).ToList();
        if (playerIds.Any())
        {
            //Collect details of the required players
            var playerEntities = _playerRepository.GetPlayerDictionary(playerIds);

            return new BusinessResult<PlayersInPosition>(
                new PlayersInPosition
                {
                    TeamId = teamId,
                    PositionCode = positionCode,
                    Players = SetDetails(_mapper, players, playerEntities)
                });
        }

        //Empty player list response
        return new BusinessResult<PlayersInPosition>(
            new PlayersInPosition
            {
                TeamId = teamId,
                PositionCode = positionCode,
                Players = new List<PlayerDetail>()
            });
    }

    //Purpose: This method maps the player details into response objects
    //Usage: To bundle player details
    //Scope: Consumed in BuildPlayersInPositionResponse(...)
    private List<PlayerDetail> SetDetails(
        IMapper _mapper,
        List<PositionDepthEntity> players, 
        Dictionary<Guid, PlayerEntity> playerEntities
    )
    {
        var playerDetails = players
                                .Select(x => _mapper.Map<PlayerDetail>(x))
                                .ToList();

        foreach (var player in playerDetails)
        {
            PlayerDetail.SetDetails(player, playerEntities[player.PlayerId]);
        }
        return playerDetails;
    }


    #region ExceptionResponseMethods
    // Response object in case of an error / exception
    public static BusinessResult<T> GetErrorBusinessResult<T>(string message)
    {
        return new BusinessResult<T>(
            status: ReturnStatus.Error,
            message: message
         );
    }

    // Response object in case of no data found
    public static BusinessResult<T> GetNotFoundBusinessResult<T>(string message)
    {
        return new BusinessResult<T>()
        {
            Status = ReturnStatus.NotFound,
            Message = message,
            BusinessResponseCode = "NotFound"
        };
    }

    // Response object in case of no data found, but has to return enpty array
    public static BusinessResult<T> GetNotFoundBusinessResult<T>(T data, string message)
    {
        return new BusinessResult<T>()
        {
            Data = data,
            Status = ReturnStatus.NotFound,
            Message = message,
            BusinessResponseCode = "NotFound"
        };
    }

    // Response object to relay validation error messages
    public static BusinessResult<T> GetValidationBusinessResult<T>(string message)
    {
        return new BusinessResult<T>()
        {
            Status = ReturnStatus.ValidationError,
            Message = message,
            BusinessResponseCode = "ValidationError"
        };
    }
    #endregion
}
