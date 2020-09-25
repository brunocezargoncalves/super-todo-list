using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Remove(Guid id);
        void Update(T entity);
    }
}
