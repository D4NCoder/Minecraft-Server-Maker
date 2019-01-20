using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Minecraft_Server_Maker.Models;
using Minecraft_Server_Maker.Scripts;
using System.IO;
namespace Minecraft_Server_Maker
{
    /// <summary>
    /// Interakční logika pro CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page
    {
        private ServerConfig serverConfig;
        private Server newServer;

        string ServerFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Minecraft_servers");

        private List<Server> servers;

        public CreatePage(List<Server> servers)
        {
            InitializeComponent();
            LoadDefaultValues(); // Loading default values into objects
            ChangeContent(); // Second attempt, now succesfully change content of the left side
            this.servers = servers;
        }

        private void LoadDefaultValues()
        {
            /*======Default initializing=======*/
            ServerPort_Textbox.Text = "25565";
            MaxPlayers_Textbox.Text = "20";
        }

        private void ChangeContent()
        {
            // Need to be in try<->catch <-- throws null exception, because Content objects are not initialized yet.
            // Skipping first attempt
            try
            {
                if (Type_Combobox.SelectedIndex == 0)
                {
                    PluginsContent.Visibility = Visibility.Hidden;
                    VanillaContent.Visibility = Visibility.Visible;
                }
                else if (Type_Combobox.SelectedIndex == 1)
                {
                    VanillaContent.Visibility = Visibility.Hidden;
                    PluginsContent.Visibility = Visibility.Visible;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Type_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeContent(); // Changing left Content by Type selection
        }

        private void CreateServerConfig()
        {
            newServer = new Server
            {
                ID = servers.Count + 1,
                MaxPlayers = int.Parse(MaxPlayers_Textbox.Text),
                Name = ServerName_Textbox.Text,
                IPAdress = IPAdress_Textbox.Text,
                Version = Version_Combobox.SelectionBoxItem.ToString(),
                Type = Type_Combobox.SelectionBoxItem.ToString(),

                Config = new ServerConfig
                {
                    WorldsName = WorldName_Textbox.Text,
                    ServerPort = int.Parse(ServerPort_Textbox.Text),
                    Seed = int.Parse(Seed_Textbox.Text),
                    SpawnAnimals = SpawnAnimals_Checkbox.IsChecked.Value,
                    SpawnMonsters = SpawnMonsters_Checkbox.IsChecked.Value,
                    PVP = PVP_Checkbox.IsChecked.Value,
                    Flight = Flight_Checkbox.IsChecked.Value,
                    GameMode = Gamemode_Combobox.SelectedIndex,
                    Difficulty = Difficulty_Combobox.SelectedIndex
                }
            };
        }

        private void MakeCopyOfTheServer()
        {
            // creating rar copy from "Servers" directory
            string rarCopyFileDestiny = System.Windows.Forms.Application.StartupPath + @"\Servers\" + newServer.Type + "_" + newServer.Version;
            // paste rar into the ~Documents/Minecraft_Servers folder
            string rarPastedFileDestiny = ServerFolderPath + "/" + newServer.Type + "_" + newServer.Version;
            // unrar the .rar file
            if (!File.Exists(rarPastedFileDestiny) && !File.Exists(ServerFolderPath + "/" + newServer.Type + "_" + newServer.Version))
            {
                //
                File.Copy(rarCopyFileDestiny + ".zip", rarPastedFileDestiny + ".zip");
                while (!FileHelper.IsFileEditable(rarPastedFileDestiny, ".zip"))
                {
                    continue;
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(rarPastedFileDestiny + ".zip", ServerFolderPath);
                File.Delete(rarPastedFileDestiny + ".zip");
            }
            else
            {
                // File doesn't exists or The Name already exists
            }
            while (!FileHelper.IsDirectoryEditable(rarPastedFileDestiny))
            {
                continue;
            }
            Directory.Move(rarPastedFileDestiny, ServerFolderPath + "/" + newServer.Name); // Name of the server must be null || same as default name

        }

        private void ChangeConfigFile(Server server, string configPath, string newConfig)
        {
            string[] lines = File.ReadAllLines(configPath);

            // replacing lines with lines of correct values
            foreach(string line in lines)
            {
                if (line.Contains("max-players"))
                {
                    string value = line;
                    string newValue = "max-players=" + server.MaxPlayers;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("motd"))
                {
                    string value = line;
                    string newValue = "motd=" + server.Name;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("server-ip"))
                {
                    string value = line;
                    string newValue = "server-ip=" + server.IPAdress;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("level-name"))
                {
                    string value = line;
                    string newValue = "level-name=" + server.Config.WorldsName;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("server-port"))
                {
                    string value = line;
                    string newValue = "server-port=" + server.Config.ServerPort;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("level-seed"))
                {
                    string value = line;
                    string newValue = "level-seed=" + server.Config.Seed;
                    line.Replace(value, newValue);
                }
                else if (line.Contains("spawn-animals"))
                {
                    string value = line;
                    string newValue = "spawn-animals=" + server.Config.SpawnAnimals.ToString().ToLower();
                    line.Replace(value, newValue);
                }
                else if (line.Contains("spawn-monsters"))
                {
                    string value = line;
                    string newValue = "spawn-monsters=" + server.Config.SpawnMonsters.ToString().ToLower();
                    line.Replace(value, newValue);
                }
                else if (line.Contains("pvp"))
                {
                    string value = line;
                    string newValue = "pvp=" + server.Config.PVP.ToString().ToLower();
                    line.Replace(value, newValue);
                }
                else if (line.Contains("Flight"))
                {
                    string value = line;
                    string newValue = "Flight=" + server.Config.SpawnAnimals.ToString().ToLower();
                    line.Replace(value, newValue);
                }
                else if (line.Contains("gamemode"))
                {
                    string value = line;
                    string newValue = "gamemode=" + server.Config.GameMode.ToString();
                    line.Replace(value, newValue);
                }
                else if (line.Contains("difficulty"))
                {
                    string value = line;
                    string newValue = "difficulty=" + server.Config.Difficulty.ToString();
                    line.Replace(value, newValue);
                }
            }

            File.WriteAllLines(newConfig, lines);
        }

        private void CreateServer_Button1_Click(object sender, RoutedEventArgs e)
        {
            CreateServerConfig();
            MakeCopyOfTheServer();

            string configPath = ServerFolderPath + "/" + newServer.Name + "/server.properties";
            string newConfig = ServerFolderPath + "/" + newServer.Name + "/server2.properties";
            ChangeConfigFile(newServer, configPath, newConfig);
        }
    }
}
