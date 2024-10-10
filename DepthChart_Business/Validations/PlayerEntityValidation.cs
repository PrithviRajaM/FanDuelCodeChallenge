#nullable enable
using DepthChart_Models.Entities;

namespace DepthChart_Business.Validations;

// Validation class to validate player entity collected from DB to find its backups
internal static class PlayerEntityValidation
{
    internal static List<string> Validate(PlayerEntity? player)
    {
        var vMessages = new List<string>();

        if (player == null)
        {
            vMessages.Add("No such player found for the PlayerId.");
        }
        else
        {
            if (player.IsDeleted)
                vMessages.Add($"The player was already deleted, on {player.UpdatedOn.ToString("dd-MMM-yyyy hh:mm")}");
        }

        return vMessages;
    }
}
