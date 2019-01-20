using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Maker.Models
{
    public class ServerConfig
    {

        public string WorldsName { get; set; }
        public int ServerPort { get; set; }
        public int Seed { get; set; }

        public bool SpawnAnimals { get; set; }
        public bool SpawnMonsters { get; set; }
        public bool PVP { get; set; }
        public bool Flight { get; set; }

        public int Difficulty { get; set; }
        public int GameMode { get; set; }
    }
}
