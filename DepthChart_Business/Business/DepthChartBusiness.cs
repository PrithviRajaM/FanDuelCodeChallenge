using AutoMapper;
using DepthChart_Business.Interfaces;
using DepthChart_Business.Validations;
using DepthChart_DB.Interface;
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;
using DepthChart_Models.Extensions;
using DepthChart_Models.Outputs;

namespace DepthChart_Business.Business;

public class DepthChartBusiness : CoreBusiness, IDepthChartBusiness
{
    private IPlayerRepository _playerRepository;
    private IDepthChartRepository _depthChartRepository;
    private IMapper _mapper;

    public DepthChartBusiness(
        IPlayerRepository playerRepository, 
        IDepthChartRepository depthChartRepository, 
        IMapper mapper
    )
    {
        _playerRepository = playerRepository;
        _depthChartRepository = depthChartRepository;
        _mapper = mapper;
    }

    // Purpose: Collect player details under a specific position
    public BusinessResult<PlayersInPosition> GetPlayersInPosition(Guid teamId, string positionCode)
    {
        // Check to validate the provided position code
        var position = EnumExtention.GetPlayerPositionByCode(positionCode);

        if (position == null)
            return GetNotFoundBusinessResult<PlayersInPosition>(
                $"No matching position found for the provided Position Code '{positionCode}'."
            );

        // Collect active players under the specific position
        var players = _depthChartRepository.GetCurrentPlayersInPosition(
                                                teamId,
                                                (PlayerPositionsEnum)position);

        return BuildPlayersInPositionResponse(_playerRepository, _mapper, teamId, positionCode, players);
    }

    //Purpose: Collect backup players listed after a specific player in the position depth hierarchy 
    public BusinessResult<PlayersInPosition> GetBackups(Guid teamId, string positionCode, Guid playerId)
    {
        // check if such player exists
        var player = _playerRepository.GetPlayer(playerId);

        var playerValidationMessage = PlayerEntityValidation.Validate(player);
        if (playerValidationMessage.Any())
            return GetErrorBusinessResult<PlayersInPosition>(string.Join("; ", playerValidationMessage));

        // Check to validate the provided position code
        var position = EnumExtention.GetPlayerPositionByCode(positionCode);

        if (position == null)
            return GetNotFoundBusinessResult<PlayersInPosition>(
                $"No matching position found for the provided Position Code '{positionCode}'."
            );

        // Collect active players under the specific position
        var playersInPosition = _depthChartRepository.GetCurrentPlayersInPosition(
                                    teamId,
                                    (PlayerPositionsEnum)position);

        if (!playersInPosition.Any(x => x.PlayerId == playerId))
            return GetNotFoundBusinessResult<PlayersInPosition>(
                new PlayersInPosition()
                {
                    Players = new List<PlayerDetail>(),
                    TeamId = teamId,
                    PositionCode = positionCode
                },
                $"No matching position found for the provided Position Code '{positionCode}'."
            );

        // Determine position depth of the requested player
        var currentPlayerPosition = playersInPosition.First(x => x.PlayerId == playerId);

        // Listing backup players
        var backupPlayer = playersInPosition
                            .Where(x => x.PositionDepth > currentPlayerPosition.PositionDepth)
                            .ToList();

        if (!backupPlayer.Any())
            return GetNotFoundBusinessResult<PlayersInPosition>(
                new PlayersInPosition()
                {
                    Players = new List<PlayerDetail>(),
                    TeamId = teamId,
                    PositionCode = positionCode
                },
                $"No backup players found for the player."
            );

        return BuildPlayersInPositionResponse(_playerRepository, _mapper, teamId, positionCode, backupPlayer);
    }

    // Purpose: Collect all players and team details to populate Depth Chart
    public BusinessResult<FullDepthChart> GetFullDepthChart(Guid teamId)
    {
        // Collection of team specific details
        var teamDetails = _depthChartRepository.GetTeamDetails(teamId);
        if (teamDetails == null)
            return GetNotFoundBusinessResult<FullDepthChart>(
                $"No team found for the provided TeamId."
            );

        // Collection of all players under the specific team
        var allPlayersOfTeam = _depthChartRepository.GetAllPlayersByTeam(teamId);

        var playerIds = allPlayersOfTeam.Select(x => x.PlayerId).ToList();
        var playerEntities = _playerRepository.GetPlayerDictionary(playerIds);

        // Chart object consolidate all the collected data
        var chart = new FullDepthChart();

        chart.DCTitle = $"{teamDetails.TeamName} Depth Chart";
        
        chart.TeamManagement = new TeamManagement()
        {
            GeneralManager = teamDetails.GeneralManager,
            HeadCoach = teamDetails.HeadCoach,
            OffenseCoordinator = teamDetails.OffenseCoordinator,
            DefenseCoordinator = teamDetails.DefenseCoordinator,
            SpecialTeamsCoordinator = teamDetails.SpecialTeamsCoordinator
        };
        
        chart.LastUpdatedOn = allPlayersOfTeam
                                .OrderByDescending(x => x.UpdatedOn)
                                .FirstOrDefault()?
                                .UpdatedOn;
        
        chart.MaxPlayersCountInAPosition = allPlayersOfTeam
                                            .OrderByDescending(x => x.PositionDepth)
                                            .FirstOrDefault()?
                                            .PositionDepth;

        // List of all the players in respective positions group under position types
        chart.PositionTypes = new List<PositionTypeSet>()
        {
            BuildPositionTypeSet(teamId, PlayerPositionTypeEnum.Offense, allPlayersOfTeam),
            BuildPositionTypeSet(teamId, PlayerPositionTypeEnum.Defense, allPlayersOfTeam),
            BuildPositionTypeSet(teamId, PlayerPositionTypeEnum.SpecialTeams, allPlayersOfTeam),
            BuildPositionTypeSet(teamId, PlayerPositionTypeEnum.PracticeSquad, allPlayersOfTeam),
            BuildPositionTypeSet(teamId, PlayerPositionTypeEnum.Reserves, allPlayersOfTeam),
        };

        // Time zone config to convert UTC to team specific time zone date time
        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(teamDetails.TeamLocationTimeZone);

        // List of past dates on which player's position might have changed
        // The archive holds histoy when a player's position is changed
        // or when a player is removed from a position
        // These dates can be used to retrive past histoy on player changes (Yet to be implemented)
        chart.ArchiveDateTimes = _depthChartRepository.GetArchiveDateTimesByTeam(teamId)
                                    .Select(x => TimeZoneInfo.ConvertTimeFromUtc(x, tzi))
                                    .ToList();

        return new BusinessResult<FullDepthChart>(chart);
    }

    // Purpose: Local method to consolidate positions with players under it with in its type
    private PositionTypeSet BuildPositionTypeSet(
        Guid teamId,
        PlayerPositionTypeEnum positionType,
        List<PositionDepthEntity> allPositionDepths
    )
    {
        var positionDepths = new List<PositionSet>();

        // Iterate through each position unde its type
        // Note: Position with out any players can be avoided, but have included for now
        foreach (var position in EnumExtention.GetPositionsEnumByType(positionType))
        {
            var playersInPosition = allPositionDepths
                                        .Where(x => x.PositionEnumId == (int)position)
                                        .OrderBy(x => x.PositionDepth)
                                        .ToList();

            // Listing of built position sets
            positionDepths.Add(new PositionSet()
            {
                PositionCode = EnumExtention.GetPlayerPositionCodeByEnum(position),
                PlayerCount = playersInPosition.Count,
                Players = BuildPlayersInPositionResponse(
                            _playerRepository,
                            _mapper,
                            teamId,
                            EnumExtention.GetPlayerPositionCodeByEnum(position),
                            playersInPosition).Data
            });
        }

        return new PositionTypeSet()
        {
            Positions = positionDepths,
            PositionTypeName = positionType.ToString()
        };
    }
}
