using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthChart_Models.Entities;
using DepthChart_Models.Enums;

namespace DepthChart_DB.Repositories;

// This class simulated fundamental DB actions
// The data is stored in a file, it is retrived and updated based on requested business logic
public class CoreRepository
{
    // the DB file located directly under DepthChart_Service project folder
    private string dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DepthChartDB.txt");
    private protected DepthChartSchema dbSchema = new DepthChartSchema();

    // This method acts as an initialiser for all DB actions
    // It is triggered for repository requests 
    private protected void GetSchema() 
    {
        string dbValueString = "";
        try
        {
            dbValueString = File.ReadAllText(dbFilePath);
        }
        catch (Exception) { }

        dbSchema = string.IsNullOrEmpty(dbValueString) ? new DepthChartSchema()
            : JsonConvert.DeserializeObject<DepthChartSchema>(dbValueString);
    }

    // A core method to update the changes to the file
    private protected void SaveChanges()
    {
        var dbValueString = JsonConvert.SerializeObject(dbSchema);
        File.WriteAllText(dbFilePath, dbValueString);
    }

    // A common business-repo method used between player and depthChart repos
    // It retrived active position depth entity for a specific position in a team
    public List<PositionDepthEntity> GetCurrentPlayersInPosition(Guid teamId, PlayerPositionsEnum position)
    {
        return dbSchema
                .PositionDepths
                .Where(x => !x.IsDeleted
                    && !x.IsArchived
                    && x.TeamId == teamId
                    && x.PositionEnumId == (int)position
                    && x.Year == DateTime.UtcNow.Year)
                .ToList();
    }

}
