using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais.Data
{
    public class EntryRepository : IRepository<Entry>
    {

        private static int? nextId = null;

        public int NextId
        {
            get
            {
                if (nextId == null)
                {
                    SqlServerCeManager database = new SqlServerCeManager();
                    IDataReader reader = database.ExecuteReader($"select max(id) from Entries");
                    if (reader.Read())
                        nextId = !reader.IsDBNull(0) ? ((int)reader[0]) + 1 : 0;
                }
                return nextId.GetValueOrDefault();
            }
            set
            {
                nextId = value;
            }
        }

        public void Delete(Entry entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            database.ExecuteQuerryAsync($"delete from Entries where Id = {entity.Id}");
        }

        public async Task<Entry[]> GetAll()
        {
            List<Entry> list = new List<Entry>();
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"select * from Entries";
            var reader = await database.ExecuteReaderAsync(querry);
            while (reader.Read())
            {
                Entry entry = new Entry
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Value = reader.GetDecimal(reader.GetOrdinal("Value")),
                    Group = reader.GetString(reader.GetOrdinal("EntryGroup")),
                    Editable = reader.GetBoolean(reader.GetOrdinal("Editable")),
                    EntryType = (EntryType)reader.GetInt32(reader.GetOrdinal("Type"))
                };
                list.Add(entry);
            }
            return list.ToArray();
        }

        public void Save(Entry entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"insert into Entries (Name, Value, EntryGroup, Editable, Type) " +
                $"values('{entity.Name}', {entity.Value.ToString("F2", CultureInfo.InvariantCulture)}, " +
                $"'{entity.Group}', '{entity.Editable}', {(int)entity.EntryType})";
            database.ExecuteQuerryAsync(querry);
        }

        public void Update(Entry entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"update Entries set Name = '{entity.Name}', " +
                $"Value = {entity.Value.ToString("F2", CultureInfo.InvariantCulture)}, " +
                $"EntryGroup = '{entity.Group}', Editable = '{entity.Editable}', " +
                $"Type = {(int)entity.EntryType} where Id = {entity.Id}";
            database.ExecuteQuerryAsync(querry);
        }
    }
}
