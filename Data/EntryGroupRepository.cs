using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais.Data
{
    public class EntryGroupRepository : IRepository<EntryGroup>
    {
        private static int? nextId = null;
        public int NextId
        {
            get
            {
                if(nextId == null)
                {
                    SqlServerCeManager database = new SqlServerCeManager();
                    IDataReader reader = database.ExecuteReader($"select max(id) from EntryGroups");
                    if (reader.Read())
                        nextId = !reader.IsDBNull(0) ? ((int)reader[0]) + 1 : 0;
                }
                return nextId.GetValueOrDefault();
            }
            set => nextId = value;
        }

        public void Delete(EntryGroup entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            database.ExecuteQuerryAsync($"delete from EntryGroups where Id = {entity.Id}");
        }

        public async Task<EntryGroup[]> GetAll()
        {
            List<EntryGroup> list = new List<EntryGroup>();
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"select * from EntryGroups";
            var reader = await database.ExecuteReaderAsync(querry);
            while (reader.Read())
            {
                EntryGroup entryGroup = new EntryGroup
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Type = reader.GetInt32(reader.GetOrdinal("Type"))
                };
                list.Add(entryGroup);
            }
            return list.ToArray();
        }

        public void Save(EntryGroup entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"insert into EntryGroups (Name, Type) values('{entity.Name}', {entity.Type})";
            database.ExecuteQuerryAsync(querry);
        }

        public void Update(EntryGroup entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"update EntryGroups set Name = '{entity.Name}', Type = {entity.Type} " +
                $"where Id = {entity.Id}";
            database.ExecuteQuerryAsync(querry);
        }
    }
}
