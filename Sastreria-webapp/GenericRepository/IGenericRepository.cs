using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SastreriaWebApp.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(object id);
        T Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
