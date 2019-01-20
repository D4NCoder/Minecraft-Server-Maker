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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using Minecraft_Server_Maker.Models;
using System.IO;
namespace Minecraft_Server_Maker
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        string constring = "datasource=localhost;port=3306;username=root;password=hackerspc123";
        private List<Server> Servers;


        public MainPage()
        {
            InitializeComponent();
            CreateMinecraftServerFolder();
            LoadServerInfoFromDatabase();
            About_Footer.Visibility = Visibility.Hidden;
            ABOUT_Tab.IsSelected = true;
            Servers_Footer.Visibility = Visibility.Hidden; // Remove this, for now it is annoying
        }

        private void CreateMinecraftServerFolder()
        {
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "/Minecraft_servers")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Minecraft_servers"));
        }

        private void PageSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ABOUT_Tab.IsSelected)
            {
                Servers_Footer.Visibility = Visibility.Hidden;
                About_Footer.Visibility = Visibility.Visible;
            }
            else if (SERVER_Tab.IsSelected)
            {
                About_Footer.Visibility = Visibility.Hidden;
                Servers_Footer.Visibility = Visibility.Visible;
                LoadToListView();
            }
        }

        private void LoadServerInfoFromDatabase()
        {
            //Servers = new List<Server>();

            //MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand("SELECT * FROM servery.servery ;", conDataBase);

            //conDataBase.Open();
            //MySqlDataReader reader = cmdDataBase.ExecuteReader();

            //while (reader.Read())
            //{
            //    Servers.Add(new Server
            //    {
            //        ID = int.Parse(reader["ID"].ToString()),
            //        Name = reader["Name"].ToString(),
            //        IPAdress = reader["IP Adress"].ToString(),
            //        MaxPlayers = int.Parse(reader["MaxPlayers"].ToString()),
            //        Version = reader["Version"].ToString()
            //    });
            //}

            //conDataBase.Close();
        }

        private void LoadToListView()
        {
            ServerList.ItemsSource = Servers;
        }

        private void CreateServer_Button1_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).BasicFrame.Content = new CreatePage(this.Servers);
        }

        private void CreateServer_Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).BasicFrame.Content = new CreatePage(this.Servers);
            
        }
    }
}
