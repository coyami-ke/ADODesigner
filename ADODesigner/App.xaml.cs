using ADODesigner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
#nullable disable
namespace ADODesigner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ConfigurationModel Config { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!File.Exists("config.json"))
            {
                File.Create("config.json").Dispose();

                Config = new();
                Config.Projects = new ObservableCollection<Project>();

                return;
            }

            FileStream configStream = new("config.json", FileMode.Open);
            StreamReader configReader = new(configStream);
            JsonSerializer serializer = new JsonSerializer();
            Config = (ConfigurationModel)serializer.Deserialize(configReader, typeof(ConfigurationModel));

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            File.Delete("config.json");

            File.Create("config.json").Dispose();

            FileStream configStream = new("config.json", FileMode.Append);
            StreamWriter configWriter = new(configStream);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(configWriter, Config);

            configWriter.Dispose();
            configStream.Dispose();
            base.OnExit(e);
        }
    }
}
