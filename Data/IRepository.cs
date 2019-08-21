using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_gastosPessoais.Data
{
    public interface IRepository<T>
    {
        Task<T[]> GetAll();
        void Save(T entity);
        void Delete(T entity);
        void Update(T entity);
        int NextId { get; set; }
    }
}
