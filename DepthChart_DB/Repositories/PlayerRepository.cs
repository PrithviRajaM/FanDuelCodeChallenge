using DepthChart_DB.Interface;
using DepthChart_Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthChart_DB.Repositories;

public class PlayerRepository : CoreRepository, IPlayerRepository
{
    public PlayerRepository()
    {
        GetSchema();
    }

    public List<PlayerEntity> GetPlayers()
    {
        return dbSchema.Players;
    }
}
