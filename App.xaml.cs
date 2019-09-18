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
        private const string DBCONNECTION = "Data Source=./db.sdf;Password=DB10pass";

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await CreateDbFile();
            await StartDatabase();
        }

        private async Task StartDatabase()
        {
            SqlServerCeManager.OpenConnection(DBCONNECTION);
            SqlServerCeManager database = new SqlServerCeManager();
            await CreateEntriesTable(database);
            await CreateGoalsTable(database);
            await CreateEntryGroupsTable(database);
        }

        private async Task CreateEntriesTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Id int identity");
            fields.Add("Name nvarchar (50)");
            fields.Add("Value decimal (9, 2)");
            fields.Add("EntryGroup nvarchar (50)");
            fields.Add("Editable bit");
            fields.Add("Type int");
            primaryKeys.Add("Id");
            await database.TryCreateTable("Entries", fields, primaryKeys);
        }

        private async Task CreateGoalsTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Id int identity");
            fields.Add("Name nvarchar (50)");
            fields.Add("Value decimal (9, 2)");
            fields.Add("SavedValue decimal (9, 2)");
            fields.Add("Completed bit");
            primaryKeys.Add("Id");
            await database.TryCreateTable("Goals", fields, primaryKeys);
        }

        private async Task CreateEntryGroupsTable(SqlServerCeManager database)
        {
            var fields = new List<string>();
            var primaryKeys = new List<string>();
            fields.Add("Id int identity");
            fields.Add("Name nvarchar (50)");
            fields.Add("Type int");
            primaryKeys.Add("Id");
            if (await database.TryCreateTable("EntryGroups", fields, primaryKeys))
            {
                var repository = new EntryGroupRepository();
                foreach (var item in EntryGroup.DefaultGroups())
                {
                    repository.Save(item);
                }
            }
        }

        private async Task CreateDbFile()
        {
            if (File.Exists("./db.sdf")) return;
            SqlCeEngine engine = new SqlCeEngine(DBCONNECTION);
            engine.CreateDatabase();
        }
    }
}
