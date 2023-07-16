using ADODesigner.Core.API;
using ADODesigner.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesigner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ExtensionLoader extensionLoader = new();
            extensionLoader.AddExtension(@"Python\animations\main.py");

            HomeView homeView = new();
            homeView.Show();
        }
    }
}
