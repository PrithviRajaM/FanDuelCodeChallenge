using AutoMapper;
using DepthChart_DB.Interface;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;
using DepthChart_Models.Inputs;
using DepthChart_Models.MappingProfiles;
using DepthChart_Models.Outputs;
using Moq;

namespace DepthChart_UnitTests.BusinessTests;

/// <summary>
/// Class provides mock definition and test data for PlayerBusinessTests and DepthChartBusinessTests
/// </summary>
public class CoreBusinessTestMocks
{
    public static IPlayerRepository GetPlayerRepo(string depthOption, bool? appendError = false, bool? removeError = false)
    {
        var playerRepo = new Mock<IPlayerRepository>();

        playerRepo.Setup(s => s.AddPlayer(It.IsAny<Player>()))
            .Returns(_playerId_1);

#pragma warning disable CS8604 // Possible null reference argument.
        playerRepo.Setup(s => s.AppendPlayerInPositionDepth(
                                    It.IsAny<Guid>(),
                                    It.IsAny<PlayerPositionsEnum>(),
                                    It.Is<Guid>(y => y == _playerId_1),
                                    It.IsAny<PlayerStatusEnum?>()
                                ))
            .Returns((bool)appendError ? null : new List<PositionDepthEntity>() {
                new PositionDepthEntity() { PlayerId = _playerId_1, TeamId = _teamId, PositionDepth = 1 }
            });
#pragma warning restore CS8604 // Possible null reference argument.

        var positionDepth = new List<PositionDepthEntity>();
        if (depthOption == "WithDepth" && depthOption != "WithOutPlayer1") 
            positionDepth.Add(new PositionDepthEntity() { PlayerId = _playerId_1, TeamId = _teamId, PositionDepth = 1 });
        positionDepth.Add(new PositionDepthEntity() { PlayerId = _playerId_2, TeamId = _teamId, PositionDepth = 2 });
        positionDepth.Add(new PositionDepthEntity() { PlayerId = _playerId_3, TeamId = _teamId, PositionDepth = 3 });
        if (depthOption == "DepthAsNull" && depthOption != "WithOutPlayer1")
            positionDepth.Add(new PositionDepthEntity() { PlayerId = _playerId_1, TeamId = _teamId, PositionDepth = 1 });

        playerRepo.Setup(s => s.InsertPlayerInPositionDepth(
                                    It.IsAny<Guid>(),
                                    It.IsAny<PlayerPositionsEnum>(),
                                    It.IsAny<Guid>(),
                                    It.IsAny<int>(),
                                    It.IsAny<PlayerStatusEnum?>()
                                ))
            .Returns(positionDepth);

        playerRepo.Setup(s => s.GetPlayerDictionary(It.IsAny<List<Guid>>()))
            .Returns(new Dictionary<Guid, PlayerEntity>() { 
                { _playerId_1, _playerEntity }, 
                { _playerId_2, _playerEntity_2 }, 
                { _playerId_3, _playerEntity_3 } 
            });

        playerRepo.Setup(s => s.RemovePlayerFromPosition(
                                    It.IsAny<Guid>(),
                                    It.IsAny<PlayerPositionsEnum>(),
                                    It.IsAny<Guid>()
                                ))
            .Returns((bool)removeError ? null : positionDepth);

        playerRepo.Setup(s => s.GetPlayer(It.IsAny<Guid>()))
            .Returns((Guid playerId) =>
            {
                switch (playerId)
                {
                    case var value when value == _playerId_1: return _playerEntity;
                    case var value when value == _unknownPlayerId: return null;
                    case var value when value == _deletedPlayerId: return _deletedPlayerEntity;
                    default: return _playerEntity;
                }
            });

        return playerRepo.Object;
    }
    public static IDepthChartRepository GetDepthChartRepo(bool WithPlayer1)
    {
        var depthChartRepo = new Mock<IDepthChartRepository>();
        depthChartRepo.Setup(s => s.GetCurrentPlayersInPosition(
                                        It.IsAny<Guid>(),
                                        It.IsAny<PlayerPositionsEnum>()
                                    ))
            .Returns(!WithPlayer1 ? 
                new List<PositionDepthEntity>() { 
                    _positionDepthEntity_2, _positionDepthEntity_3 }
                : new List<PositionDepthEntity>() {
                    _positionDepthEntity_1, _positionDepthEntity_2, _positionDepthEntity_3 });

        depthChartRepo.Setup(s => s.GetTeamDetails(It.IsAny<Guid>()))
            .Returns((Guid teamId) =>
            {
                switch (teamId)
                {
                    case var value when value == _teamId: return _teamEntity;
                    case var value when value == _unknownTeamId: return null;
                    default: return _teamEntity;
                }
            });

        depthChartRepo.Setup(s => s.GetAllPlayersByTeam(It.IsAny<Guid>()))
            .Returns(new List<PositionDepthEntity>());

        depthChartRepo.Setup(s => s.GetArchiveDateTimesByTeam(It.IsAny<Guid>()))
            .Returns(new List<DateTime>());

        return depthChartRepo.Object;
    }

    public static IMapper GetMapper() 
        => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new PlayerMapProfile())));


    public static Guid _teamId = Guid.ParseExact("7f6c6655-8f6e-4d25-91a6-84b89631e1a7", "D");
    public static Guid _unknownTeamId = Guid.ParseExact("7f6c6655-8f6e-4d25-91a6-84b89631e1a1", "D");
    public static string _positionCode = "OG";
    public static string _wrongPositionCode = "AA";
    public static Guid _playerId_1 = Guid.ParseExact("c9010721-21d6-4c8c-ba26-8de7b1b2cf1f", "D");
    public static Guid _playerId_2 = Guid.ParseExact("c9010721-21d6-4c8c-ba26-8de7b1b2cf12", "D");
    public static Guid _playerId_3 = Guid.ParseExact("c9010721-21d6-4c8c-ba26-8de7b1b2cf13", "D");
    public static Guid _unknownPlayerId = Guid.ParseExact("c9010721-21d6-4c8c-ba26-8de7b1b2cf14", "D");
    public static Guid _deletedPlayerId = Guid.ParseExact("c9010721-21d6-4c8c-ba26-8de7b1b2cf15", "D");
    public static Player _player = new Player()
    { FirstName = "Mick", LastName = "Ratatouille", DepthChartkey = "U/AN", PlayerNumber = 65 };

    public static PlayerDetail _playerDetail = new PlayerDetail()
    { FirstName = "Mick", LastName = "Ratatouille", DepthChartkey = "U/AN", PlayerNumber = 65, 
        PlayerId = _playerId_1, PositionDepth = 1 };

    public static string _statusCode = "Active";
    public static PlayerEntity _playerEntity = new PlayerEntity()
    {
        PlayerId = _playerId_1,
        FirstName = _playerDetail.FirstName,
        LastName = _playerDetail.LastName,
        DepthChartkey = _playerDetail.DepthChartkey,
        IsDeleted = false,
        PlayerNumber = _playerDetail.PlayerNumber,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };
    public static PlayerEntity _playerEntity_2 = new PlayerEntity()
    {
        PlayerId = _playerId_2,
        FirstName = _playerDetail.FirstName,
        LastName = _playerDetail.LastName,
        DepthChartkey = _playerDetail.DepthChartkey,
        IsDeleted = false,
        PlayerNumber = _playerDetail.PlayerNumber,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };
    public static PlayerEntity _playerEntity_3 = new PlayerEntity()
    {
        PlayerId = _playerId_3,
        FirstName = _playerDetail.FirstName,
        LastName = _playerDetail.LastName,
        DepthChartkey = _playerDetail.DepthChartkey,
        IsDeleted = false,
        PlayerNumber = _playerDetail.PlayerNumber,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };
    public static PlayerEntity _deletedPlayerEntity = new PlayerEntity()
    {
        PlayerId = _playerId_1,
        FirstName = _playerDetail.FirstName,
        LastName = _playerDetail.LastName,
        DepthChartkey = _playerDetail.DepthChartkey,
        IsDeleted = true,
        PlayerNumber = _playerDetail.PlayerNumber,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };

    public static PositionDepthEntity _positionDepthEntity_1 = new PositionDepthEntity()
    {
        PositionDepthId = Guid.NewGuid(),
        TeamId = _teamId,
        Year = 2024,
        PositionEnumId = 1,
        PlayerId = _playerId_1,
        PositionDepth = 1,
        StatusEnumId = 1,
        IsArchived = false,
        IsDeleted = false,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };

    public static PositionDepthEntity _positionDepthEntity_2 = new PositionDepthEntity()
    {
        PositionDepthId = Guid.NewGuid(),
        TeamId = _teamId,
        Year = 2024,
        PositionEnumId = 1,
        PlayerId = _playerId_2,
        PositionDepth = 2,
        StatusEnumId = 1,
        IsArchived = false,
        IsDeleted = false,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };

    public static PositionDepthEntity _positionDepthEntity_3 = new PositionDepthEntity()
    {
        PositionDepthId = Guid.NewGuid(),
        TeamId = _teamId,
        Year = 2024,
        PositionEnumId = 1,
        PlayerId = _playerId_3,
        PositionDepth = 3,
        StatusEnumId = 1,
        IsArchived = false,
        IsDeleted = false,
        CreatedOn = DateTime.UtcNow,
        UpdatedOn = DateTime.UtcNow
    };

    public static TeamsEntity _teamEntity = new TeamsEntity()
    {
        TeamId = _teamId,
        TeamName = "Team name",
        TeamLocationTimeZone = "Eastern Standard Time",
        SportId = Guid.NewGuid(),
        GeneralManager = "General Manager",
        IsDeleted = false
    };

    public static AddPlayerInput GetAddPlayerInput(string inputType)
    { 
        switch (inputType) {
            case "WithPlayerId_DepthAsNull":
                return new AddPlayerInput()
                {
                    TeamId = _teamId,
                    PositionCode = _positionCode,
                    PlayerId = _playerId_1,
                    PlayerDetails = null,
                    PositionDepth = null,
                    StatusCode = _statusCode
                };
            case "WithPlayerId_WithDepth":
                return new AddPlayerInput()
                {
                    TeamId = _teamId,
                    PositionCode = _positionCode,
                    PlayerId = _playerId_1,
                    PlayerDetails = null,
                    PositionDepth = 1,
                    StatusCode = _statusCode
                };
            case "WithPlayerDetail_WithDepth":
                return new AddPlayerInput()
                {
                    TeamId = _teamId,
                    PositionCode = _positionCode,
                    PlayerId = null,
                    PlayerDetails = _player,
                    PositionDepth = 1,
                    StatusCode = _statusCode
                };
            default: return new AddPlayerInput()
            {
                TeamId = _teamId,
                PositionCode = _positionCode,
                PlayerId = _playerId_1,
                PlayerDetails = _player,
                PositionDepth = null,
                StatusCode = _statusCode
            };
        }
    }

    public static RemovePlayerInput GetRemovePlayerInput(string inputType)
    {
        return new RemovePlayerInput() { 
            TeamId = _teamId, 
            PositionCode = _positionCode, 
            PlayerId = inputType == "UnknownPlayer" ? Guid.NewGuid() : _playerId_1 
        };
    }
}
