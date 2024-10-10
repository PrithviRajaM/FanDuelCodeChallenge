using DepthChart_Business.Business;
using DepthChart_Models.Constants;
using Xunit;

namespace DepthChart_UnitTests.BusinessTests.DepthChartBusinessTests;

/// <summary>
/// Test cases for depth chart business 
/// </summary>
public class DepthChartBusinessTests : CoreBusinessTestMocks
{
    [Fact]
    public void Get_Players_In_Position()
    {
        var depthChartbusiness = new DepthChartBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = depthChartbusiness.GetPlayersInPosition(_teamId, _positionCode);

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count >= 1);
        Assert.True(response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
    }

    [Fact]
    public void Get_Backups_With_Lower_Position_Depth()
    {
        var depthChartbusiness = new DepthChartBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = depthChartbusiness.GetBackups(_teamId, _positionCode, _playerId_1);

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.True(response.Data.Players.Count == 2);
        Assert.True(!response.Data.Players.Where(x => x.PlayerId == _playerId_1).Any());
    }

    [Fact]
    public void Get_Backups_Without_Lower_Position_Depth()
    {
        var depthChartbusiness = new DepthChartBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = depthChartbusiness.GetBackups(_teamId, _positionCode, _playerId_3);

        Assert.True(response.Status == ReturnStatus.NotFound);
        Assert.True(response.Data.Players.Count == 0);
    }

    [Fact]
    public void Get_Backups_Without_Player_In_Position()
    {
        var depthChartbusiness = new DepthChartBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = depthChartbusiness.GetBackups(_teamId, _positionCode, Guid.NewGuid());

        Assert.True(response.Status == ReturnStatus.NotFound);
        Assert.True(response.Data.Players.Count == 0);
    }

    [Fact]
    public void Get_Full_Depth_Chart()
    {
        var depthChartbusiness = new DepthChartBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(true), GetMapper());
        var response = depthChartbusiness.GetFullDepthChart(_teamId);

        Assert.True(response.Status == ReturnStatus.OK);
        Assert.False(response.Data == null);
        Assert.True(response.Data.PositionTypes.Count == 5);
        Assert.True(response.Data.TeamManagement.GeneralManager == "General Manager");
    }
}
