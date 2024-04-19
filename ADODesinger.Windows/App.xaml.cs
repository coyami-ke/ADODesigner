using Jace.Execution;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesinger.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public LoggerConfiguration? MainLogger { get; set; }
        public static Logger? Logger { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory("logs");
            this.MainLogger = new LoggerConfiguration();
            var log = this.MainLogger.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Logger = log;
            Logger.Information($"ADODesigner ::: Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger?.Error(e.Exception.Message + " StackTrace: " + e.Exception.StackTrace);
            Environment.Exit(1);
        }
    }
}
