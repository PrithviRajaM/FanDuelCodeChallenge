#nullable enable
using DepthChart_DB.Interface;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;

namespace DepthChart_DB.Repositories;

public class PlayerRepository : CoreRepository, IPlayerRepository
{
    public PlayerRepository()
    {
        GetSchema();
    }

    public List<PlayerEntity> GetPlayers()
    {
        return dbSchema.Players;
    }

    // Adding a new player, if exists
    public Guid AddPlayer(Player data)
    {
        var existingPlayer = dbSchema
                                .Players
                                .FirstOrDefault(x => !x.IsDeleted
                                    && x.FirstName == data.FirstName
                                    && x.LastName == data.LastName
                                    && x.PlayerNumber == data.PlayerNumber
                                    && x.DepthChartkey == data.DepthChartkey);

        // Check if exists
        if (existingPlayer != null) return existingPlayer.PlayerId;

        //Add new player
        var newPlayer = PlayerEntity.AddNewPlayer
                                        (
                                            data.PlayerNumber,
                                            data.FirstName,
                                            data.LastName,
                                            data.DepthChartkey
                                        );

        dbSchema.Players.Add(newPlayer);
        SaveChanges();

        return newPlayer.PlayerId;
    }

    //Append the new player to the end of the position depth stack
    public List<PositionDepthEntity> AppendPlayerInPositionDepth(Guid teamId, PlayerPositionsEnum position, Guid playerId, PlayerStatusEnum? status)
    {
        var positions = GetCurrentPlayersInPosition(teamId, position);
        var lastPlayerPosition = positions.OrderBy(x => x.PositionDepthId).LastOrDefault();

        dbSchema.PositionDepths.Add(
            PositionDepthEntity.AddNewPlayer(
                teamId, 
                (int)position, 
                playerId,
                status != null ? (int) status : (int) PlayerStatusEnum.Active,
                lastPlayerPosition == null ? 1 : (lastPlayerPosition.PositionDepth + 1)
            ));

        SaveChanges();
        
        return GetCurrentPlayersInPosition(teamId, position);
    }

    // Place a new player in a specific position and increment rest of the player's depth index
    public List<PositionDepthEntity> InsertPlayerInPositionDepth(
        Guid teamId, PlayerPositionsEnum position, Guid playerId, int positionDepth, PlayerStatusEnum? status)
    {
        var positions = GetCurrentPlayersInPosition(teamId, position);
        var currentUTC = DateTime.UtcNow;

        //Increment player position depth, for players after current player
        var laterPlayers = positions
            .Where(x => x.PositionDepth >= positionDepth)
            .ToList();

        // Archiving existing players
        foreach (var laterPlayer in laterPlayers) 
        {
            laterPlayer.IsArchived = true;
            laterPlayer.UpdatedOn = currentUTC.AddSeconds(-1);
        }

        // Adding new player
        dbSchema.PositionDepths.Add(
            PositionDepthEntity.AddNewPlayer(
                teamId,
                (int)position,
                playerId,
                status != null ? (int)status : (int)PlayerStatusEnum.Active,
                positionDepth
            ));

        // Adding player positioned lower than the new player
        dbSchema.PositionDepths.AddRange(
                laterPlayers
                    .Where(x => x.PlayerId != playerId)
                    .Select(x => PositionDepthEntity.AddNewPlayer(
                        x.TeamId,
                        x.PositionEnumId,
                        x.PlayerId,
                        x.StatusEnumId,
                        x.PositionDepth + 1
                    ))
            );

        SaveChanges();

        return CleanUpPositionDepth(teamId, position);
    }

    // A dictionary collection of players details
    // User mostly for response object build
    public Dictionary<Guid, PlayerEntity> GetPlayerDictionary(List<Guid> playerIds) 
        => dbSchema
            .Players
            .Where(x => playerIds.Contains(x.PlayerId))
            .ToDictionary(x => x.PlayerId);

    // A method to clean messed up position index and used after removal of a player
    public List<PositionDepthEntity> CleanUpPositionDepth(Guid teamId, PlayerPositionsEnum position)
    {
        var positions = GetCurrentPlayersInPosition(teamId, position);

        var iterator = 1;
        foreach (var spot in positions.OrderBy(x => x.PositionDepth))
        {
            spot.PositionDepth = iterator; iterator++;
        }

        SaveChanges();

        return positions;
    }

    // Remove (Archive) a player and reiterate the position depth index
    public List<PositionDepthEntity>? RemovePlayerFromPosition(Guid teamId, PlayerPositionsEnum position, Guid playerId)
    {
        try
        {
            var playerToRemove = dbSchema
                            .PositionDepths
                            .FirstOrDefault(x => x.TeamId == teamId
                                && x.PositionEnumId == (int)position
                                && x.Year == DateTime.UtcNow.Year
                                && x.PlayerId == playerId
                                && !x.IsDeleted
                                && !x.IsArchived);

            if (playerToRemove == null) throw new Exception();

            playerToRemove.IsArchived = true;
            playerToRemove.IsDeleted = true;
            playerToRemove.UpdatedOn = DateTime.UtcNow;

            SaveChanges();

            return CleanUpPositionDepth(teamId, position);
        }catch (Exception)
        {
            return null;
        }
    }

    // Relay a specific player details
    public PlayerEntity? GetPlayer(Guid playerId)
        => dbSchema.Players.FirstOrDefault(x => x.PlayerId == playerId);

}
