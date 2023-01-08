using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpo.Logger;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMJT_Desktop_Application.ViewModels;
using Serilog;
using Serilog.Enrichers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace PMJT_Desktop_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider ServiceProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string[] clArgs = Environment.GetCommandLineArgs().Skip(1).ToArray();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            // Through smuggle almost all kind of configurations
            builder.SetBasePath(AppContext.BaseDirectory)
                // add main json settings
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                // add optional json settings "stolen" from maybe working web api
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                // add environment variables
                .AddEnvironmentVariables()
                // add command line arguments
                .AddCommandLine(clArgs);
            IConfigurationRoot conf = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(conf)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    Environment.GetEnvironmentVariable("TMP") + $"/ConcurrencyEmulator.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}"
            )
                .CreateLogger();
            ServiceProvider = new ServiceCollection()
                .AddSingleton(typeof(MainViewModel), ViewModelSource.GetPOCOType(typeof(MainViewModel)))
                .AddSingleton(typeof(MainWindow))
                .AddSingleton<IConfiguration>(conf)
                .BuildServiceProvider();
         
            Log.Debug("OnStartup ______________________________________________");

            MainWindow mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        object Resolve(Type type, object key, string name) => type == null ? null : ServiceProvider.GetService(type);

        /// <summary>
        /// On app exit event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            ApplicationThemeHelper.SaveApplicationThemeName();
            Log.CloseAndFlush();
            base.OnExit(e);
        }
    }
}

