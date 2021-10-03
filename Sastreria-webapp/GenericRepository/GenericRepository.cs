using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SastreriaWebApp.Data;

namespace SastreriaWebApp.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private SastreriaWebAppContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository(SastreriaWebAppContext context)
        {            
            table = context.Set<T>();
            _context = context;
        }
        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {

            return _context.Set<T>().AsQueryable();
            //throw new NotImplementedException();
        }

        public Task<T> GetById(object id)
        {

            return _context.Set<T>().FindAsync(id).AsTask();
 
        }

        public T Insert(T obj)
        {
            var pyvar =_context.Set<T>().Add(obj);
            return pyvar.Entity;
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Update(T obj)
        {

            _context.Set<T>().Update(obj);

            //throw new NotImplementedException();
        }
    }
}
