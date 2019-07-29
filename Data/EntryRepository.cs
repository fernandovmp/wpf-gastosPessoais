using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais.Data
{
    public class EntryRepository : IRepository<Entry>
    {

        private static int? nextId = null;

        public void Delete(Entry entity)
        {
            DatabaseManager.Delete<Entry>($"Id = {entity.Id}");
        }

        public Entry[] GetAll()
        {
            return DatabaseManager.ReadAll<Entry>().ToArray();
        }

        public int NextId
        {
            get
            {
                if (nextId == null)
                {
                    nextId = DatabaseManager.NextId(new Entry());
                }
                return nextId.GetValueOrDefault();
            }
            set
            {
                nextId = value;
            }
        }

        public void Save(Entry entity)
        {
            DatabaseManager.Save(entity);
        }

        public void Update(Entry entity)
        {
            DatabaseManager.Update(entity, $"Id = {entity.Id}");
        }
    }
}
