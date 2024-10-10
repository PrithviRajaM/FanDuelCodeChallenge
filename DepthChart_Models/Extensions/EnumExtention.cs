using DepthChart_Models.Constants;
using DepthChart_Models.Enums;

namespace DepthChart_Models.Extensions;

/// <summary>
/// And Enum utility method to convert enum from its value to name, short name or groups
/// </summary>
public static class EnumExtention
{
    public static List<PlayerPositionsEnum> GetPositionsEnumByType(PlayerPositionTypeEnum type)
    {
        switch (type) {
            case PlayerPositionTypeEnum.Offense:
                return
                [
                    PlayerPositionsEnum.OffensiveCoordinator,
                    PlayerPositionsEnum.OffensiveGuard,
                    PlayerPositionsEnum.OffensiveTackle,
                    PlayerPositionsEnum.FullBack,
                    PlayerPositionsEnum.HalfBack,
                    PlayerPositionsEnum.RunningBack,
                    PlayerPositionsEnum.QuarterBack,
                    PlayerPositionsEnum.TightEnd,
                    PlayerPositionsEnum.WideReceiver
                ];
            case PlayerPositionTypeEnum.Defense:
                return
                [
                    PlayerPositionsEnum.Safety,
                    PlayerPositionsEnum.CornerBack,
                    PlayerPositionsEnum.InsideLinebacker,
                    PlayerPositionsEnum.NickelBack,
                    PlayerPositionsEnum.OutsideLinebacker,
                    PlayerPositionsEnum.DefensiveTackle,
                    PlayerPositionsEnum.DefensiveEnd
                ];
            case PlayerPositionTypeEnum.SpecialTeams:
                return
                [
                    PlayerPositionsEnum.KickReturner,
                    PlayerPositionsEnum.PuntReturner,
                    PlayerPositionsEnum.LongSnapper,
                    PlayerPositionsEnum.PlaceKicker,
                    PlayerPositionsEnum.Holder
                ];
            case PlayerPositionTypeEnum.PracticeSquad:
                return
                [
                    PlayerPositionsEnum.PracticeSquad
                ];
            case PlayerPositionTypeEnum.Reserves:
                return
                [
                    PlayerPositionsEnum.Reserves,
                    PlayerPositionsEnum.InjuredReserve
                ];
            default: return [];
        }
    }
    public static string GetPlayerPositionCodeByEnum(PlayerPositionsEnum position)
    {
        switch (position)
        {
            case PlayerPositionsEnum.OffensiveCoordinator: return "OC";
            case PlayerPositionsEnum.OffensiveGuard: return "OG";
            case PlayerPositionsEnum.OffensiveTackle: return "OT";
            case PlayerPositionsEnum.FullBack: return "FB";
            case PlayerPositionsEnum.HalfBack: return "HB";
            case PlayerPositionsEnum.RunningBack: return "RB";
            case PlayerPositionsEnum.QuarterBack: return "QB";
            case PlayerPositionsEnum.TightEnd: return "TE";
            case PlayerPositionsEnum.WideReceiver: return "WR";
            case PlayerPositionsEnum.Safety: return "S";
            case PlayerPositionsEnum.CornerBack: return "CB";
            case PlayerPositionsEnum.InsideLinebacker: return "ILB";
            case PlayerPositionsEnum.NickelBack: return "NB";
            case PlayerPositionsEnum.OutsideLinebacker: return "OLB";
            case PlayerPositionsEnum.DefensiveTackle: return "DL";
            case PlayerPositionsEnum.DefensiveEnd: return "DE";
            case PlayerPositionsEnum.KickReturner: return "KR";
            case PlayerPositionsEnum.PuntReturner: return "PR";
            case PlayerPositionsEnum.LongSnapper: return "LS";
            case PlayerPositionsEnum.PlaceKicker: return "PK";
            case PlayerPositionsEnum.Holder: return "H";
            case PlayerPositionsEnum.PracticeSquad: return "PS";
            case PlayerPositionsEnum.Reserves: return "R";
            case PlayerPositionsEnum.InjuredReserve: return "IR";
            default: return "";
        }
    }

    public static PlayerPositionsEnum? GetPlayerPositionByCode (string code)
    {
        switch (code)
        {
            case "OC": return PlayerPositionsEnum.OffensiveCoordinator;
            case "OG": return PlayerPositionsEnum.OffensiveGuard;
            case "OT": return PlayerPositionsEnum.OffensiveTackle;
            case "FB": return PlayerPositionsEnum.FullBack;
            case "HB": return PlayerPositionsEnum.HalfBack;
            case "RB": return PlayerPositionsEnum.RunningBack;
            case "QB": return PlayerPositionsEnum.QuarterBack;
            case "TE": return PlayerPositionsEnum.TightEnd;
            case "WR": return PlayerPositionsEnum.WideReceiver;
            case "S": return PlayerPositionsEnum.Safety;
            case "CB": return PlayerPositionsEnum.CornerBack;
            case "ILB": return PlayerPositionsEnum.InsideLinebacker;
            case "NB": return PlayerPositionsEnum.NickelBack;
            case "OLB": return PlayerPositionsEnum.OutsideLinebacker;
            case "DL": return PlayerPositionsEnum.DefensiveTackle;
            case "DE": return PlayerPositionsEnum.DefensiveEnd;
            case "KR": return PlayerPositionsEnum.KickReturner;
            case "PR": return PlayerPositionsEnum.PuntReturner;
            case "LS": return PlayerPositionsEnum.LongSnapper;
            case "PK": return PlayerPositionsEnum.PlaceKicker;
            case "H": return PlayerPositionsEnum.Holder;
            case "PS": return PlayerPositionsEnum.PracticeSquad;
            case "R": return PlayerPositionsEnum.Reserves;
            case "IR": return PlayerPositionsEnum.InjuredReserve;
            default: return null;
        }
    }

    public static PlayerStatusEnum? GetPlayerStatusEnum(string statusCode)
    {
        switch (statusCode)
        {
            case "Active": return PlayerStatusEnum.Active;
            case "Rookie": return PlayerStatusEnum.Rookie;
            case "InjuredOrInactive": return PlayerStatusEnum.InjuredOrInactive;
            case "Injured": return PlayerStatusEnum.InjuredOrInactive;
            case "Inactive": return PlayerStatusEnum.InjuredOrInactive;
            case "None": return PlayerStatusEnum.None;
            default: return null;
        }
    }
}