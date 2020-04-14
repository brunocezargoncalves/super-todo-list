using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add();
        void Remove(T entity);
        void Update(T entity);
    }
}
