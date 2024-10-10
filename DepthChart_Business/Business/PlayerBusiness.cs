using AutoMapper;
using DepthChart_Business.Interfaces;
using DepthChart_Business.Validations;
using DepthChart_DB.Interface;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;
using DepthChart_Models.Extensions;
using DepthChart_Models.Inputs;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Business;

public class PlayerBusiness : CoreBusiness, IPlayerBusiness
{
    private IPlayerRepository _playerRepository;
    private IDepthChartRepository _depthChartRepository;
    private IMapper _mapper;
    public PlayerBusiness(
        IPlayerRepository playerRepository,
        IDepthChartRepository depthChartRepository, 
        IMapper mapper
    )
    {
        _playerRepository = playerRepository;
        _depthChartRepository = depthChartRepository;
        _mapper = mapper;
    }

    // Purpose: Landing business methid for addition of new player in a specific position
    public BusinessResult<PlayersInPosition> AddPlayer(AddPlayerInput inputData)
    {
        //Validating AddPlayerInput parameter
        var inputValidationResult = AddPlayerInputValidation.Validate(inputData);
        //Returning input validation messgaes if any
        if (inputValidationResult != null)
            return inputValidationResult;

        //Check whether the player exists, if not create one
        if (inputData.PlayerId == null)
            inputData.PlayerId = _playerRepository.AddPlayer(inputData.PlayerDetails);

        //insert into respective position depth and archive existing position depths
        var responseData = AddPlayerInPosition(inputData);

        return responseData;
    }

    // Purpose: Landing business methid for removal of a player in a specific position
    public BusinessResult<PlayersInPosition> RemovePlayer(RemovePlayerInput inputData)
    {
        //Validating RemovePlayerInput parameter
        var inputValidationResult = RemovePlayerInputValidation.Validate(inputData);
        //Returning input validation messgaes if any
        if (inputValidationResult != null)
            return inputValidationResult;
        
        // Check and remove the requested player and return updated position depth stack
        return RemovePlayerInPosition(inputData);
    }

    //Purpose: a private method to perform addtion of player to position logic
    private BusinessResult<PlayersInPosition> AddPlayerInPosition(AddPlayerInput inputData)
    {
        // Validation of provided position code
        var position = EnumExtention.GetPlayerPositionByCode(inputData.PositionCode);

        if (position == null) 
            return GetNotFoundBusinessResult<PlayersInPosition>(
                $"No matching position found for the provided Position Code '{inputData.PositionCode}'."
            );

        // Collect all current players under the specific position
        var currentPlayers = _depthChartRepository.GetCurrentPlayersInPosition(
                                                    inputData.TeamId,
                                                    (PlayerPositionsEnum) position);

        var playersInPosition = new List<PositionDepthEntity>();

        // Case when position depth is not defined and no existing players in the position
        // then append the new player to the position
        if (inputData.PositionDepth == null || !currentPlayers.Any())
        {
            // Return if the player to add already exist in the list
            if (currentPlayers.Any(x => x.PlayerId == inputData.PlayerId))
                return GetValidationBusinessResult<PlayersInPosition>(
                    $"The provided player already exist in the requested position at Position " +
                    $"{currentPlayers.First(x => x.PlayerId == inputData.PlayerId).PositionDepth}."
                );

            // Action to append the player to position depth's end
            playersInPosition = _playerRepository.AppendPlayerInPositionDepth(
                                inputData.TeamId, 
                                (PlayerPositionsEnum) position, 
                                (Guid) inputData.PlayerId,
                                EnumExtention.GetPlayerStatusEnum(inputData.StatusCode));

            // Case when the updated position depth list is empty
            // or the added player is not in the end of the list
            // then respond with an error message
            if (playersInPosition == null
                || !playersInPosition.Any()
                || playersInPosition.OrderBy(x => x.PositionDepth).LastOrDefault()?.PlayerId != inputData.PlayerId)

                return GetErrorBusinessResult<PlayersInPosition>(
                    "An error occured while appending player to end of the position depth."
                );
        }
        else
        {
            // The case when a player needs to be inserted among existing players in the position
            playersInPosition = _playerRepository.InsertPlayerInPositionDepth(
                                inputData.TeamId,
                                (PlayerPositionsEnum)position,
                                (Guid)inputData.PlayerId,
                                (int) inputData.PositionDepth,
                                EnumExtention.GetPlayerStatusEnum(inputData.StatusCode));
        }

        // Respond with the updated position depth stack with player details
        return BuildPlayersInPositionResponse(
            _playerRepository, _mapper, inputData.TeamId, inputData.PositionCode, playersInPosition);
    }

    //Purpose: a private method to perform removal of player to position logic
    private BusinessResult<PlayersInPosition> RemovePlayerInPosition(RemovePlayerInput inputData)
    {
        // Validation of provided position code
        var position = EnumExtention.GetPlayerPositionByCode(inputData.PositionCode);

        if (position == null)
            return GetNotFoundBusinessResult<PlayersInPosition>(
                $"No matching position found for the provided Position Code '{inputData.PositionCode}'."
            );

        // Collect all current players under the specific position
        var currentPlayers = _depthChartRepository.GetCurrentPlayersInPosition(
                                        inputData.TeamId,
                                        (PlayerPositionsEnum)position);

        // Case when the position depth stack is not empty
        if (currentPlayers.Any())
        {
            // And the required is available in the stack
            if (currentPlayers.Any(x => x.PlayerId == inputData.PlayerId))
            {
                // Action removal of the player and update the position depth index
                // for rest of the player below the removal
                var playersInPosition = _playerRepository.RemovePlayerFromPosition(
                    inputData.TeamId,
                    (PlayerPositionsEnum)position,
                    inputData.PlayerId
                );

                // playersInPosition receives null when the removal action has an unseen scenario
                if (playersInPosition == null)
                    return GetErrorBusinessResult<PlayersInPosition>(
                    $"An error has occured while removing the player."
                );

                return BuildPlayersInPositionResponse(
                    _playerRepository, _mapper, inputData.TeamId, inputData.PositionCode, playersInPosition);
            }
            else
            {
                // Case when the requested player is not found in the position depth stack
                return GetNotFoundBusinessResult<PlayersInPosition>(
                    new PlayersInPosition()
                    {
                        Players = new List<PlayerDetail>(),
                        TeamId = inputData.TeamId,
                        PositionCode = inputData.PositionCode
                    },
                    $"The provided players is not found in the provided Position Code '{inputData.PositionCode}'."
                );
            }
        }
        else
        {
            // case when no players are found in the position depth stack
            return GetNotFoundBusinessResult<PlayersInPosition>(
                $"No players found in the provided position, Position Code '{inputData.PositionCode}'."
            );
        }
    }
}
