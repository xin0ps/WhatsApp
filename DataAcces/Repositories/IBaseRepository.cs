using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories
{
    public interface IBaseRepo<T>
    {

        T? GetById(int id);
        string Add(T entity);
        string AddRange(ICollection<T> entities);
        ICollection<T>? GetAll();
        string Remove(T entity);
        string Update(T entity);
        void SaveChanges();
    }
}
