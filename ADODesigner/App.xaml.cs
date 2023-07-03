using ADODesigner.Converters;
using ADODesigner.Core;
using ADODesigner.Models;
using ADODesigner.Views;
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
        protected override void OnExit(ExitEventArgs e)
        {

            File.Delete("config.json");

            File.Create("config.json").Dispose();

            FileStream configStream = new("config.json", FileMode.Append);
            StreamWriter configWriter = new(configStream);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(configWriter, LoadView.Config);

            configWriter.Dispose();
            configStream.Dispose();
            base.OnExit(e);
        }
    }
}
