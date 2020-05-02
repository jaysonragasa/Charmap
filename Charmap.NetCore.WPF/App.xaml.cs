using Charmap.Shared.Interfaces;
using Charmap.Shared.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows;

namespace Charmap.NetCore.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Lazy<Logger> lazyLogger => new Lazy<Logger>(() =>
        {
            return new Logger();
        });

        public static ILogger Logger = lazyLogger.Value;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Logger.Start();

#if DEBUG
            Logger.Log("Running in Debug");
#else
            Logger.Log("Running in Release");
#endif

            AppCenter.Start("150b1c43-3c92-4467-b092-96ffe2de0aa3",
                   typeof(Analytics), typeof(Crashes));

            
        }
    }
}
