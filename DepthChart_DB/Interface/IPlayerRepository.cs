#nullable enable
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;

namespace DepthChart_DB.Interface;

public interface IPlayerRepository
{
    public Guid AddPlayer(Player data);

    public List<PositionDepthEntity> AppendPlayerInPositionDepth(
        Guid teamId, PlayerPositionsEnum position, Guid playerId, PlayerStatusEnum? status);

    public List<PositionDepthEntity> InsertPlayerInPositionDepth(
        Guid teamId, PlayerPositionsEnum position, Guid playerId, int positionDepth, PlayerStatusEnum? status);

    public Dictionary<Guid, PlayerEntity> GetPlayerDictionary(List<Guid> playerIds);

    public List<PositionDepthEntity>? RemovePlayerFromPosition(
        Guid teamId, PlayerPositionsEnum position, Guid playerId);

    public PlayerEntity? GetPlayer(Guid playerId);

}
