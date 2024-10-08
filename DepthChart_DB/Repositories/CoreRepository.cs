using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepthChart_Models.Entities;

namespace DepthChart_DB.Repositories
{
    public class CoreRepository
    {
        private string dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DepthChartDB.txt");
        private protected DepthChartSchema dbSchema = new DepthChartSchema();

        private protected void GetSchema() {
            string dbValueString = "";
            try
            {
                dbValueString = File.ReadAllText(dbFilePath);
            }
            catch (Exception) { }

            dbSchema = string.IsNullOrEmpty(dbValueString) ? new DepthChartSchema()
                : JsonConvert.DeserializeObject<DepthChartSchema>(dbValueString);
        }

        private protected void SaveChanges()
        {
            var dbValueString = JsonConvert.SerializeObject(dbSchema);
            File.WriteAllText(dbFilePath, dbValueString);
        }
    }
}
