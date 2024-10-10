using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Extensions;
using DepthChart_Models.Inputs;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Validations;

// Validation class to validate add player input
internal static class AddPlayerInputValidation
{
    internal static BusinessResult<PlayersInPosition> Validate(AddPlayerInput inputData)
    {
        var vMessages = new List<string>();
        if (inputData == null)
            return new BusinessResult<PlayersInPosition>(ReturnStatus.BadRequest,
                "The Add player input is mandatory.");

        if (inputData.TeamId == Guid.Empty) vMessages.Add("TeamID has to be a valid Guid.");

        if (inputData.PlayerId == null && inputData.PlayerDetails == null) 
            vMessages.Add("Either one of the input PlayerId or PlayerDetails is mandatory.");

        if (inputData.PlayerDetails != null)
        {
            if (string.IsNullOrEmpty(inputData.PlayerDetails.FirstName)
                || inputData.PlayerDetails.FirstName.Length < 4)
                vMessages.Add("The FirstName has to be atleast 3 characters long");

            if (string.IsNullOrEmpty(inputData.PlayerDetails.LastName)
                || inputData.PlayerDetails.LastName.Length < 4)
                vMessages.Add("The LastName has to be atleast 3 characters long");

            if (inputData.PlayerDetails.PlayerNumber == 0
                || inputData.PlayerDetails.PlayerNumber.ToString().Length > 2)
                vMessages.Add("The PlayerNumber has to be whole number between 0 and 100.");
        }

        if (EnumExtention.GetPlayerStatusEnum(inputData.StatusCode) == null)
            vMessages.Add("The StatusCode has to be one among ['Active', 'Rookie', 'InjuredOrInactive', 'Injured', 'Inactive', 'None']");

        //return if any validation found
        if (vMessages.Any()) return new BusinessResult<PlayersInPosition>(ReturnStatus.BadRequest, vMessages);

        //returning null as not validations found
        return null;

    }
}
