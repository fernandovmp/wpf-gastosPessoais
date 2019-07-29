using Database;
using System;
using System.Collections.Generic;
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
                    nextId = DatabaseManager.NextId(new EntryGroup());
                }
                return nextId.GetValueOrDefault();
            }
            set => nextId = value;
        }

        public void Delete(EntryGroup entity)
        {
            DatabaseManager.Delete<EntryGroup>($"Id = {entity.Id}");
        }

        public EntryGroup[] GetAll()
        {
            return DatabaseManager.ReadAll<EntryGroup>().ToArray();
        }

        public void Save(EntryGroup entity)
        {
            DatabaseManager.Save(entity);
        }

        public void Update(EntryGroup entity)
        {
            DatabaseManager.Update(entity, $"Id = {entity.Id}");
        }
    }
}
