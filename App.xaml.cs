using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlServerCe;
using System.IO;
using wpf_gastosPessoais.Models;
using Database;

namespace wpf_gastosPessoais
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SetInitialAppConfig();
            CreateDbFile();
            DatabaseManager.TryCreateTable(new Entry());
            DatabaseManager.TryCreateTable(new Goal());
            if(DatabaseManager.TryCreateTable(new EntryGroup()))
            {
                foreach (var item in EntryGroup.DefaultGroups())
                {
                    DatabaseManager.Save(item);
                }
            }
        }

        private void SetInitialAppConfig()
        {
            var currentDomain = AppDomain.CurrentDomain;
            string basePath = currentDomain.BaseDirectory;
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings["SQLConnection"].Value = settings["SQLConnection"].Value.Replace("|dbFilePath|", basePath);
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void CreateDbFile()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/db.sdf")) return;
            SqlCeEngine engine = new SqlCeEngine(ConfigurationManager.AppSettings.Get("SQLConnection"));
            engine.CreateDatabase();
        }
    }
}
