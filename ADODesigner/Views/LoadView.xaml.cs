using ADODesigner.Core;
using ADODesigner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ADODesigner.Views
{
    /// <summary>
    /// Interaction logic for LoadView.xaml
    /// </summary>
    public partial class LoadView : Window
    {
        public static ConfigurationModel? Config { get; set; }
        public LoadView()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(5000);

            string pathToConfig = Path.Combine(ConfigFiles.CONFIG_FOLDER, ConfigFiles.CONFIG);
            Directory.CreateDirectory(ConfigFiles.CONFIG_FOLDER);
            if (!File.Exists(pathToConfig)) File.Create(pathToConfig).Dispose();

            Path.Combine("dir", "file.json");
            
            JsonTextReader configReader = new(new StreamReader(pathToConfig));
            JsonSerializer configSerializer = new();
            configSerializer.Deserialize<ConfigurationModel>(configReader);

            HomeView homeView = new();
            homeView.Show();
            this.Close();
        }
    }
}
