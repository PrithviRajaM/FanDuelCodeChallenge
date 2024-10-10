# Fan Duel Code Challenge #

Thank you for the opportunity to participate in the coding challenge.

I have followed the provided instructions regarding the purpose and design of NFL Depth Charts and also drew inspiration from ourlads.com.

## User Stories & ACs ##
All the required use cases have been implemented, with a few additional features:

1. **Archive Date Logic:** A system for tracking the history of player position changes over time has been implemented, allowing for the archiving of past data.
2. **Team-Specific Timezones:** The creation and update timestamps for players and position depth entities are stored in UTC and displayed according to the respective team’s local timezone.
3. **Expanded Team Details:** Additional information such as General Manager, Head Coach, Offensive Coordinator, Defensive Coordinator, and Special Teams Coordinator has been included in the team data structure.
4. **Scalable Data Structure:** The architecture is designed for scalability, allowing for multi-dimensional use. For example, by adding a column to the Teams table, the application can support various sports, along with team-specific details like state, country, gender, divisions, etc. This diversification can be achieved using a foreign key to link to the Teams table.
5. **JWT Authentication:** The API has been secured with JWT authentication. Steps for using it, along with credentials, are detailed below.

The development process followed a Test-Driven Development (TDD) approach. Test cases were created first for all the use cases, followed by the implementation of the business logic.

The respective UseCases are mapped with the drafted Test Cases below,
1. **addPlayerToDepthChart (position, player, position_depth)**
  - Adds a player to the depth chart at a given position - ***TestCase: Add_Player_WithPlayerId_WithDepth()***
  - Adding a player without a position_depth would add them to the end of the depth chart at that position - ***TestCase: Add_Player_WithPlayerId_DepthAsNull()***
  - The added player would get priority. Anyone below that player in the depth chart would get moved down a position_depth - ***TestCase: Add_Player_WithPlayerId_WithDepth()***
2. **removePlayerFromDepthChart(position, player)**
  - Removes a player from the depth chart for a given position and returns that player - ***TestCase: Remove_Player_With_Player_In_Depth()***
  - An empty list should be returned if that player is not listed in the depth chart at that position - ***TestCase: Remove_Player_Without_Player_In_Depth()***
3. **getBackups (position, player)**
  - For a given player and position, we want to see all players that are “Backups”, those with a lower position_depth - ***TestCase: Get_Backups_With_Lower_Position_Depth()***
  - An empty list should be returned if the given player has no Backups - ***TestCase: Get_Backups_Without_Lower_Position_Depth()***
  - An empty list should be returned if the given player is not listed in the depth chart at that position - ***TestCase: Get_Backups_Without_Player_In_Position()***
4. **getFullDepthChart()**
  - Print out the full depth chart with every position on the team and every player within the Depth Chart - ***TestCase: Get_Full_Depth_Chart()***

Few addition validation and edge cases are handled and respective test cases are,
1. Add_Player_With_Null_Input
2. Add_Player_With_Empty_Guid
3. Add_Player_With_No_Player_Data
4. Add_Player_With_No_First_Name
5. Add_Player_With_No_Last_Name
6. Add_Player_With_No_PlayerNumber
7. Add_Player_With_Wrong_Status_Code
8. Add_Player_With_Unknown_Position_Code
9. Add_Player_With_Existing_Player
10. Add_Player_With_Unknown_Error_At_Repo
11. Remove_Player_With_Unknown_Position
12. Remove_Player_With_Unknown_Error_At_Repo
13. Get_Players_In_Position_Wrong_Position_Code
14. Get_Backups_For_Unknown_Player
15. Get_Backups_For_Deleted_Player
16. Get_Backups_With_Wrong_Position_Code
17. Get_Full_Depth_Chart_For_Unknown_Team

## Solution Design & Technical Spec: ##

The endpoints are divided into **Player** and **DepthChart** controllers.

Player Controller: 
1. HttpPost: /AddPlayer
2. HttpPost: /RemovePlayer
   
DepthChart Controller:
1. HttpGet: /GetPlayerByPosition
2. HttpGet: /GetBackups
3. HttpGet: /GetFullDepthChart

The Solution diagram for reference:
![](https://github.com/PrithviRajaM/FanDuelCodeChallenge/blob/master/DepthChart_Service/Others/SolutionWFDiagram.png)
Database Entity relationship diagram for reference:
![](https://github.com/PrithviRajaM/FanDuelCodeChallenge/blob/master/DepthChart_Service/Others/DCSchema_ERDiagram.png)
## Swagger execution ##
Execute the application with the below command with in FanDuelCodeChallenge > DepthChart_Service.

    dotner run

The url for the swagger page should be accessible through the below url.

    https://localhost:7157/index.html

On the Swagger page, initiate the basic authentication with 

Username: FanDuel

Password: FanDuel

Then trigger the endpoint **/OAuth/token** with grant_type as **client_credentials**

It should generate an bearer token, then authenticate it in Swagger's Authorize page.

Once authenticated, all the endpoints under DepthChart and Player controller should be accessible.

## Endpoint Inputs ##

### Player Controller: ###
**1. HttpPost: /AddPlayer**

Input:
```json
{
  "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
  "positionCode": "OC",
  "playerId": null,
  "playerDetails": {
    "playerNumber": 78,
    "firstName": "Tristan",
    "lastName": "Wirfs",
    "depthChartkey": "20/1"
  },
  "positionDepth": 1
}
```
Output:
```json
{
  "data": {
    "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
    "positionCode": "OC",
    "players": [
      {
        "playerId": "ecc7426e-ffc9-4e24-a97a-705232fbfe0a",
        "playerNumber": 78,
        "firstName": "Tristan",
        "lastName": "Wirfs",
        "depthChartkey": "20/1",
        "positionDepth": 1
      },
      {
        "playerId": "58e08f87-bed0-4520-ac88-475676f39812",
        "playerNumber": 99,
        "firstName": "Prithvi",
        "lastName": "Raja",
        "depthChartkey": "AA",
        "positionDepth": 2
      },
      {
        "playerId": "b64b12d2-3659-41bd-b410-2419caec7ef1",
        "playerNumber": 82,
        "firstName": "Raja",
        "lastName": "Suresha",
        "depthChartkey": "QW",
        "positionDepth": 3
      }
    ]
  },
  "status": "OK",
  "message": ""
}
```
**2. HttpPost: /RemovePlayer**

Input:
```json
{
  "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
  "positionCode": "OC",
  "playerId": "58e08f87-bed0-4520-ac88-475676f39812"
}
```
Output:
```json
{
  "data": {
    "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
    "positionCode": "OC",
    "players": [
      {
        "playerId": "ecc7426e-ffc9-4e24-a97a-705232fbfe0a",
        "playerNumber": 78,
        "firstName": "Tristan",
        "lastName": "Wirfs",
        "depthChartkey": "20/1",
        "positionDepth": 1
      },
      {
        "playerId": "b64b12d2-3659-41bd-b410-2419caec7ef1",
        "playerNumber": 82,
        "firstName": "Raja",
        "lastName": "Suresha",
        "depthChartkey": "QW",
        "positionDepth": 2
      }
    ]
  },
  "status": "OK",
  "message": ""
}
```
   
### DepthChart Controller: ###
**1. HttpGet: /GetPlayerByPosition**

Input:

teamId : 7f6c6655-8f6e-4d25-91a6-84b89631e1a7

positionCode : OC

Output:
```json
{
  "data": {
    "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
    "positionCode": "OC",
    "players": [
      {
        "playerId": "ecc7426e-ffc9-4e24-a97a-705232fbfe0a",
        "playerNumber": 78,
        "firstName": "Tristan",
        "lastName": "Wirfs",
        "depthChartkey": "20/1",
        "positionDepth": 1
      },
      {
        "playerId": "b64b12d2-3659-41bd-b410-2419caec7ef1",
        "playerNumber": 82,
        "firstName": "Raja",
        "lastName": "Suresha",
        "depthChartkey": "QW",
        "positionDepth": 2
      },
    ]
  },
  "status": "OK",
  "message": ""
}
```
**2. HttpGet: /GetBackups**

Input:

teamId : 7f6c6655-8f6e-4d25-91a6-84b89631e1a7

positionCode : OC

playerId : ecc7426e-ffc9-4e24-a97a-705232fbfe0a

Output:
```json
{
  "data": {
    "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
    "positionCode": "OC",
    "players": [
      {
        "playerId": "b64b12d2-3659-41bd-b410-2419caec7ef1",
        "playerNumber": 82,
        "firstName": "Raja",
        "lastName": "Suresha",
        "depthChartkey": "QW",
        "positionDepth": 2
      }
    ]
  },
  "status": "OK",
  "message": ""
}
```
**3. HttpGet: /GetFullDepthChart**

Input:

teamId : 7f6c6655-8f6e-4d25-91a6-84b89631e1a7

Output:
```json
{
  "data": {
    "dcTitle": "Tampa Bay Buccaneers Depth Chart",
    "teamManagement": {
      "generalManager": "Jason Licht",
      "headCoach": "Todd Bowles",
      "offenseCoordinator": "Liam Coen",
      "defenseCoordinator": "Kacy Rodgers & Larry Foote",
      "specialTeamsCoordinator": "Thomas McGaughey"
    },
    "lastUpdatedOn": "2024-10-10T10:05:51.6545137Z",
    "maxPlayersCountInAPosition": 4,
    "positionTypes": [
      {
        "positionTypeName": "Offense",
        "positions": [
          {
            "positionCode": "OC",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "OC",
              "players": [
                {
                  "playerId": "ecc7426e-ffc9-4e24-a97a-705232fbfe0a",
                  "playerNumber": 78,
                  "firstName": "Tristan",
                  "lastName": "Wirfs",
                  "depthChartkey": "20/1",
                  "positionDepth": 1
                },
                {
                  "playerId": "b64b12d2-3659-41bd-b410-2419caec7ef1",
                  "playerNumber": 82,
                  "firstName": "Raja",
                  "lastName": "Suresha",
                  "depthChartkey": "QW",
                  "positionDepth": 2
                },
                {
                  "playerId": "d224f815-46bb-4aae-83c7-90a5d52e2846",
                  "playerNumber": 45,
                  "firstName": "Mikess",
                  "lastName": "Evansadd",
                  "depthChartkey": "QW",
                  "positionDepth": 3
                },
                {
                  "playerId": "95cd3f47-fb0c-4092-af2f-6e5c699a5810",
                  "playerNumber": 8,
                  "firstName": "Raj",
                  "lastName": "Suresh",
                  "depthChartkey": "QW",
                  "positionDepth": 4
                }
              ]
            },
            "playerCount": 4
          },
          {
            "positionCode": "OG",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "OG",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "OT",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "OT",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "FB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "FB",
              "players": [
                {
                  "playerId": "68963fa7-4966-453e-a0c5-b3995469d712",
                  "playerNumber": 37,
                  "firstName": "Tavierre",
                  "lastName": "Thomas",
                  "depthChartkey": "U/Hou",
                  "positionDepth": 1
                }
              ]
            },
            "playerCount": 1
          },
          {
            "positionCode": "HB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "HB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "RB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "RB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "QB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "QB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "TE",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "TE",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "WR",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "WR",
              "players": []
            },
            "playerCount": 0
          }
        ]
      },
      {
        "positionTypeName": "Defense",
        "positions": [
          {
            "positionCode": "S",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "S",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "CB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "CB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "ILB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "ILB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "NB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "NB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "OLB",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "OLB",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "DL",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "DL",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "DE",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "DE",
              "players": []
            },
            "playerCount": 0
          }
        ]
      },
      {
        "positionTypeName": "SpecialTeams",
        "positions": [
          {
            "positionCode": "KR",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "KR",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "PR",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "PR",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "LS",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "LS",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "PK",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "PK",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "H",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "H",
              "players": []
            },
            "playerCount": 0
          }
        ]
      },
      {
        "positionTypeName": "PracticeSquad",
        "positions": [
          {
            "positionCode": "PS",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "PS",
              "players": []
            },
            "playerCount": 0
          }
        ]
      },
      {
        "positionTypeName": "Reserves",
        "positions": [
          {
            "positionCode": "R",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "R",
              "players": []
            },
            "playerCount": 0
          },
          {
            "positionCode": "IR",
            "players": {
              "teamId": "7f6c6655-8f6e-4d25-91a6-84b89631e1a7",
              "positionCode": "IR",
              "players": []
            },
            "playerCount": 0
          }
        ]
      }
    ],
    "archiveDateTimes": [
      "2024-10-10T06:07:22.0732829",
      "2024-10-10T06:05:50.6534108",
      "2024-10-09T20:34:23.4985677",
      "2024-10-09T20:31:48.1906372",
      "2024-10-09T20:31:27.9999729",
      "2024-10-09T07:34:36.0957689",
      "2024-10-09T07:28:01.3870111",
      "2024-10-09T06:45:51.7837892",
      "2024-10-09T06:01:34.0385669",
      "2024-10-09T03:45:17.2149421",
      "2024-10-09T03:44:26.8237651",
      "2024-10-09T03:30:39.5449808"
    ]
  },
  "status": "OK",
  "message": ""
}
```
