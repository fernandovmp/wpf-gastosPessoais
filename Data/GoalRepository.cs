using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Models;
using System.Globalization;
using System.Data;

namespace wpf_gastosPessoais.Data
{
    public class GoalRepository : IRepository<Goal>
    {
        private static int? nextId = null;
        public int NextId
        {
            get
            {
                if(nextId == null)
                {
                    SqlServerCeManager database = new SqlServerCeManager();
                    IDataReader reader = database.ExecuteReader($"select max(id) from Goals");
                    if (reader.Read())
                        nextId = !reader.IsDBNull(0) ? ((int)reader[0]) + 1 : 0;

                }
                return nextId.GetValueOrDefault();
            }
            set => nextId = value;
        }

        public void Delete(Goal entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            database.ExecuteQuerryAsync($"delete from Goals where Id = {entity.Id} ");
        }

        public async Task<Goal[]> GetAll()
        {
            List<Goal> list = new List<Goal>();
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"select * from Goals";
            var reader = await database.ExecuteReaderAsync(querry);
            while (reader.Read())
            {
                Goal goal = new Goal
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Value = reader.GetDecimal(reader.GetOrdinal("Value")),
                    SavedValue = reader.GetDecimal(reader.GetOrdinal("SavedValue")),
                    Completed = reader.GetBoolean(reader.GetOrdinal("Completed"))
                };
                list.Add(goal);
            }
            return list.ToArray();
        }

        public void Save(Goal entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"insert into Goals (Name, Value, SavedValue, Completed) " +
                $"values('{entity.Name}', {entity.Value.ToString("F2", CultureInfo.InvariantCulture)}, " +
                $"{entity.SavedValue.ToString("F2", CultureInfo.InvariantCulture)}, '{entity.Completed}')";
            database.ExecuteQuerryAsync(querry);
        }

        public void Update(Goal entity)
        {
            SqlServerCeManager database = new SqlServerCeManager();
            string querry = $"update Goals " +
                $"set Name = '{entity.Name}', Value = {entity.Value.ToString("F2", CultureInfo.InvariantCulture)}, " +
                $"SavedValue = {entity.SavedValue.ToString("F2", CultureInfo.InvariantCulture)}, " +
                $"Completed = '{entity.Completed}' where Id = {entity.Id}";
            database.ExecuteQuerryAsync(querry);
        }
    }
}
