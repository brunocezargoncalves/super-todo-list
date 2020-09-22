using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Remove(int id);
        void Edit(T entity);
    }
}
