using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Extensions;
using DepthChart_Models.Inputs;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Validations;

// Validation class to validate remove player input
internal static class RemovePlayerInputValidation
{
    internal static BusinessResult<PlayersInPosition> Validate(RemovePlayerInput inputData)
    {
        var vMessages = new List<string>();
        if (inputData == null)
            return new BusinessResult<PlayersInPosition>(ReturnStatus.BadRequest,
                "The Remove player input is mandatory.");

        if (inputData.TeamId == Guid.Empty) vMessages.Add("TeamID has to be a valid Guid.");

        if (inputData.PlayerId == Guid.Empty)
            vMessages.Add("PlayerID has to be a valid Guid.");

        //return if any validation found
        if (vMessages.Any()) return new BusinessResult<PlayersInPosition>(ReturnStatus.BadRequest, vMessages);

        //returning null as not validations found
        return null;
    }
    }
