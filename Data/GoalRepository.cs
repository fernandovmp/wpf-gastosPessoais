using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Models;
using Database;

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
                    nextId = DatabaseManager.NextId(new Goal());
                }
                return nextId.GetValueOrDefault();
            }
            set => nextId = value;
        }

        public void Delete(Goal entity)
        {
            DatabaseManager.Delete<Goal>($"Id = {entity.Id}");
        }

        public Goal[] GetAll()
        {
            return DatabaseManager.ReadAll<Goal>().ToArray();
        }

        public void Save(Goal entity)
        {
            DatabaseManager.Save(entity);
        }

        public void Update(Goal entity)
        {
            DatabaseManager.Update(entity, $"Id = {entity.Id}");
        }
    }
}
