using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart_Models.Enums
{
    public enum PlayerPositions
    {
        [EnumMember(Value = "OC")]
        [Description("Offensive Coordinator")]
        OffensiveCoordinator = 1,

        [EnumMember(Value = "OG")]
        [Description("Offensive Guard")]
        OffensiveGuard = 2,

        [EnumMember(Value = "OT")]
        [Description("Offensive Tackle")]
        OffensiveTackle = 3,

        [EnumMember(Value = "FB")]
        [Description("Full Back")]
        FullBack = 4,

        [EnumMember(Value = "HB")]
        [Description("Half Back")]
        HalfBack = 5,

        [EnumMember(Value = "RB")]
        [Description("Running Back")]
        RunningBack = 6,

        [EnumMember(Value = "QB")]
        [Description("Quarter Back")]
        QuarterBack = 7,

        [EnumMember(Value = "TE")]
        [Description("Tight End")]
        TightEnd = 8,

        [EnumMember(Value = "WR")]
        [Description("Wide Receiver")]
        WideReceiver = 9,

        [EnumMember(Value = "S")]
        [Description("Safety")]
        Safety = 10,

        [EnumMember(Value = "CB")]
        [Description("Corner Back")]
        CornerBack = 11,

        [EnumMember(Value = "ILB")]
        [Description("Inside Linebacker")]
        InsideLinebacker = 12,

        [EnumMember(Value = "NB")]
        [Description("Nickel Back")]
        NickelBack = 13,

        [EnumMember(Value = "OLB")]
        [Description("Outside Linebacker")]
        OutsideLinebacker = 14,

        [EnumMember(Value = "DL")]
        [Description("Defensive Tackle")]
        DefensiveTackle = 15,

        [EnumMember(Value = "DE")]
        [Description("Defensive End")]
        DefensiveEnd = 16,

        [EnumMember(Value = "KR")]
        [Description("Kick Returner")]
        KickReturner = 17,

        [EnumMember(Value = "PR")]
        [Description("Punt Returner")]
        PuntReturner = 18,

        [EnumMember(Value = "LS")]
        [Description("Long Snapper")]
        LongSnapper = 19,

        [EnumMember(Value = "PK")]
        [Description("Place Kicker")]
        PlaceKicker = 20,

        [EnumMember(Value = "H")]
        [Description("Holder")]
        Holder = 21,

        [EnumMember(Value = "PS")]
        [Description("Practice Squad")]
        PracticeSquad = 22,

        [EnumMember(Value = "R")]
        [Description("Reserves")]
        Reserves = 23,

        [EnumMember(Value = "IR")]
        [Description("Injured Reserve")]
        InjuredReserve = 24
    }
}
