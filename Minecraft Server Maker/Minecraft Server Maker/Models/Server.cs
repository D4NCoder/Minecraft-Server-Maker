using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Maker.Models
{
    public class Server
    {
        public int ID { get; set; }
        public int MaxPlayers { get; set; }
        public string Name { get; set; }
        public string IPAdress { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }

        public ServerConfig Config { get; set; }
    }
}
