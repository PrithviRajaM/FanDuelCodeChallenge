using DepthChart_Business.Business;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Inputs;
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

    #region EdgeCaseTests
    [Fact]
    public void Add_Player_With_Null_Input()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(null);

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "The Add player input is mandatory.");
    }

    [Fact]
    public void Add_Player_With_Empty_Guid()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = Guid.Empty,
            PositionCode = _positionCode,
            PlayerId = _playerId_1,
            PlayerDetails = null,
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "TeamID has to be a valid Guid.");
    }

    [Fact]
    public void Add_Player_With_No_Player_Data()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = null,
            PlayerDetails = null,
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "Either one of the input PlayerId or PlayerDetails is mandatory.");
    }

    [Fact]
    public void Add_Player_With_No_First_Name()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = null,
            PlayerDetails = new Player()
            {
                FirstName = "K",
                LastName = "Miss",
                DepthChartkey = "U/ES",
                PlayerNumber = 1
            },
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "The FirstName has to be atleast 3 characters long");
    }

    [Fact]
    public void Add_Player_With_No_Last_Name()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = null,
            PlayerDetails = new Player()
            {
                FirstName = "Kate",
                LastName = "M",
                DepthChartkey = "U/ES",
                PlayerNumber = 1
            },
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "The LastName has to be atleast 3 characters long");
    }

    [Fact]
    public void Add_Player_With_No_PlayerNumber()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = null,
            PlayerDetails = new Player()
            {
                FirstName = "Kate",
                LastName = "Miss",
                DepthChartkey = "U/ES",
                PlayerNumber = 0
            },
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "The PlayerNumber has to be whole number between 0 and 100.");
    }

    [Fact]
    public void Add_Player_With_Wrong_Status_Code()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = null,
            PlayerDetails = new Player()
            {
                FirstName = "Kate",
                LastName = "Miss",
                DepthChartkey = "U/ES",
                PlayerNumber = 1
            },
            PositionDepth = null,
            StatusCode = "Status"
        });

        Assert.True(response.Status == ReturnStatus.BadRequest);
        Assert.True(response.Message == "The StatusCode has to be one among ['Active', 'Rookie', 'InjuredOrInactive', 'Injured', 'Inactive', 'None']");
    }

    [Fact]
    public void Add_Player_With_Unknown_Position_Code()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = "AA",
            PlayerId = _playerId_1,
            PlayerDetails = null,
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.NotFound);
        Assert.True(response.Message == "No matching position found for the provided Position Code 'AA'.");
    }

    [Fact]
    public void Add_Player_With_Existing_Player()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth"), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = _playerId_2,
            PlayerDetails = null,
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.ValidationError);
        Assert.True(response.Message == "The provided player already exist in the requested position at Position 2.");
    }

    [Fact]
    public void Add_Player_With_Unknown_Error_At_Repo()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithDepth", true), GetDepthChartRepo(false), GetMapper());
        var response = playerbusiness.AddPlayer(new AddPlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = _playerId_1,
            PlayerDetails = null,
            PositionDepth = null,
            StatusCode = _statusCode
        });

        Assert.True(response.Status == ReturnStatus.Error);
        Assert.True(response.Message == "An error occured while appending player to end of the position depth.");
    }

    [Fact]
    public void Remove_Player_With_Unknown_Position()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithOutPlayer1"), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.RemovePlayer(new RemovePlayerInput()
        {
            TeamId = _teamId,
            PositionCode = "AA",
            PlayerId = _playerId_1
        });

        Assert.True(response.Status == ReturnStatus.NotFound);
        Assert.True(response.Message == "No matching position found for the provided Position Code 'AA'.");
    }

    [Fact]
    public void Remove_Player_With_Unknown_Error_At_Repo()
    {
        var playerbusiness = new PlayerBusiness(GetPlayerRepo("WithOutPlayer1", removeError: true), GetDepthChartRepo(true), GetMapper());
        var response = playerbusiness.RemovePlayer(new RemovePlayerInput()
        {
            TeamId = _teamId,
            PositionCode = _positionCode,
            PlayerId = _playerId_1
        });

        Assert.True(response.Status == ReturnStatus.Error);
        Assert.True(response.Message == "An error has occured while removing the player.");
    }

    #endregion
}
