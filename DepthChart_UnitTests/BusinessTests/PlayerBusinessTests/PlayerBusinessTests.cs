using DepthChart_Business.Business;
using DepthChart_Models.Constants;
using Xunit;

namespace DepthChart_UnitTests.BusinessTests.PlayerBusinessTests;

/// <summary>
/// Test cases for player realted business 
/// </summary>
public class PlayerBusinessTests : CoreBusinessTestMocks
{
    [Fact]
    public void Add_Player_WithPlayerId_DepthAsNull()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(GetAddPlayerInput("WithPlayerId_DepthAsNull"));

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count >= 1);
        Assert.True(response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
    }

    [Fact]
    public void Add_Player_WithPlayerId_WithDepth()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.AddPlayer(GetAddPlayerInput("WithPlayerId_WithDepth"));

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count >= 1);
        Assert.True(response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
        Assert.True(response.Data.Players.Where(x => x.PositionDepth == 1).Any());
    }

    [Fact]
    public void Add_Player_WithPlayerDetail_WithDepth()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.AddPlayer(GetAddPlayerInput("WithPlayerDetail_WithDepth"));

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count >= 1);
        Assert.True(response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
        Assert.True(response.Data.Players.Where(x => x.PositionDepth == 1).Any());
    }

    [Fact]
    public void Remove_Player_With_Player_In_Depth()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithOutPlayer1"), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.RemovePlayer(GetRemovePlayerInput(""));

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count == 2);
        Assert.False(response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
    }

    [Fact]
    public void Remove_Player_Without_Player_In_Depth()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithOutPlayer1"), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.RemovePlayer(GetRemovePlayerInput("UnknownPlayer"));

        Assert.True(response.Status == ReturnStatus.NotFound);
        Assert.True(response.Data.Players.Count == 0);
    }
}
