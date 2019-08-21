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

        public string CreateTable(string name, IEnumerable<string> fields, IEnumerable<string> primaryKeys)
        {
            string primaryKey = "";
            if (primaryKeys.Count() > 0)
            {
                primaryKey = $"primary key({string.Join(",", primaryKeys)})";
            }
            return $"create table {name} ({string.Join(",", fields)}, {primaryKey})";
        }

        public async void ExecuteQuerry(string querry)
        {
            SqlCeCommand command = new SqlCeCommand(querry, dbConnection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> ExecuteReader(string querry)
        {
            SqlCeCommand command = new SqlCeCommand(querry, dbConnection);
            DbDataReader reader = await command.ExecuteReaderAsync();
            return reader;
        }

    }
}
