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
using wpf_gastosPessoais.Data;

namespace wpf_gastosPessoais
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            SetInitialAppConfig();
            CreateDbFile();
            await StartDatabase();
        }

        private async Task StartDatabase()
        {
            SqlServerCeManager.OpenConnection(ConfigurationManager.AppSettings.Get("SQLConnection"));
            SqlServerCeManager database = new SqlServerCeManager();
            await CreateEntriesTable(database);
            await CreateGoalsTable(database);
            await CreateEntryGroupsTable(database);
        }

        private async Task CreateEntriesTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Name nvarchar (50)");
            fields.Add("Value decimal (9, 2)");
            fields.Add("EntryGroup nvarchar (50)");
            fields.Add("Editable bit");
            fields.Add("Type int");
            primaryKeys.Add("Id int identity");
            await database.TryCreateTable("Entries", fields, primaryKeys);
        }

        private async Task CreateGoalsTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Name nvarchar (50)");
            fields.Add("Value decimal (9, 2)");
            fields.Add("SavedValue decimal (9, 2)");
            fields.Add("Completed bit");
            primaryKeys.Add("Id int identity");
            await database.TryCreateTable("Goals", fields, primaryKeys);
        }

        private async Task CreateEntryGroupsTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Name nvarchar (50)");
            fields.Add("Type int");
            primaryKeys.Add("Id int identity");
            if (await database.TryCreateTable("Goals", fields, primaryKeys))
            {
                var repository = new EntryGroupRepository();
                foreach (var item in EntryGroup.DefaultGroups())
                {
                    repository.Save(item);
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
