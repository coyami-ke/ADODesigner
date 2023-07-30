using ADODesigner.Core.API;
using ADODesigner.Localization;
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
        ExtensionLoader? ExtensionLoader { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            //Localization.Resources.Culture = new System.Globalization.CultureInfo("ko");
            //ExtensionLoader = new();
            //ExtensionLoader.AddExtension(@"python\animations\main.py");

            //HomeView homeView = new();
            //homeView.Show();

            EditorAPI.Editor = new();
            EditorAPI.Editor.Show();
        }
    }
}
