using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;

namespace wpf_gastosPessoais.Data
{
    public class SqlServerCeManager
    {
        private static SqlCeConnection dbConnection;

        public static void OpenConnection(string connection)
        {
            dbConnection = new SqlCeConnection(connection);
            dbConnection.Open();
        }

        public async Task<bool> TryCreateTable(string tableName, IEnumerable<string> fields, 
            IEnumerable<string> primaryKeys)
        {
            if (await TableExists(tableName)) return false;
            string querry = CreateTable(tableName, fields, primaryKeys);
            ExecuteQuerryAsync(querry);
            return true;
        }

        private async Task<bool> TableExists(string tableName)
        {
            string querry = $"select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = '{tableName}'";
            IDataReader reader = await ExecuteReaderAsync(querry);
            if (reader != null) return true;
            return false;
        }

        public string CreateTable(string name, IEnumerable<string> fields, IEnumerable<string> primaryKeys)
        {
            string primaryKey = "";
            if (primaryKeys.Count() > 0)
            {
                primaryKey = $"primary key({string.Join(",", primaryKeys)})";
            }
            return $"create table {name} ({string.Join(",", fields)}, {primaryKey})";
        }

        public async void ExecuteQuerryAsync(string querry)
        {
            SqlCeCommand command = new SqlCeCommand(querry, dbConnection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> ExecuteReaderAsync(string querry)
        {
            SqlCeCommand command = new SqlCeCommand(querry, dbConnection);
            DbDataReader reader = await command.ExecuteReaderAsync();
            return reader;
        }

        public IDataReader ExecuteReader(string querry)
        {
            SqlCeCommand command = new SqlCeCommand(querry, dbConnection);
            DbDataReader reader = command.ExecuteReader();
            return reader;
        }

    }
}
