using DepthChart_Models.Constants;
using DepthChart_Models.Enums;

namespace DepthChart_Models.Extensions;

public static class PlayerPositionsExtention
{
    public static List<PlayerPositions> GetPlayerPositionsByType(PlayerPositionType type)
    {
        switch (type) {
            case PlayerPositionType.Offense:
                return
                [
                    PlayerPositions.OffensiveCoordinator,
                    PlayerPositions.OffensiveGuard,
                    PlayerPositions.OffensiveTackle,
                    PlayerPositions.FullBack,
                    PlayerPositions.HalfBack,
                    PlayerPositions.RunningBack,
                    PlayerPositions.QuarterBack,
                    PlayerPositions.TightEnd,
                    PlayerPositions.WideReceiver
                ];
            case PlayerPositionType.Defense:
                return
                [
                    PlayerPositions.Safety,
                    PlayerPositions.CornerBack,
                    PlayerPositions.InsideLinebacker,
                    PlayerPositions.NickelBack,
                    PlayerPositions.OutsideLinebacker,
                    PlayerPositions.DefensiveTackle,
                    PlayerPositions.DefensiveEnd
                ];
            case PlayerPositionType.SpecialTeams:
                return
                [
                    PlayerPositions.KickReturner,
                    PlayerPositions.PuntReturner,
                    PlayerPositions.LongSnapper,
                    PlayerPositions.PlaceKicker,
                    PlayerPositions.Holder
                ];
            case PlayerPositionType.PracticeSquad:
                return
                [
                    PlayerPositions.PracticeSquad
                ];
            case PlayerPositionType.Reserves:
                return
                [
                    PlayerPositions.Reserves,
                    PlayerPositions.InjuredReserve
                ];
            default: return [];
        }
    }

    public static PlayerPositions? GetPlayerPositionByCode (string code)
    {
        var asd = Enum.GetValues(typeof(PlayerPositions));

        return PlayerPositions.CornerBack;
    }
}