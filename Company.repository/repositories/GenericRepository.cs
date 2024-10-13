using Company.Data.context;
using Company.Data.Entities;
using Company.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.repository.repositories
{
    public class GenericRepository<T> : IGenaricReposatory<T> where T : BaseEntity

    {
        private readonly CompanyDbContext _context;
       public  GenericRepository(CompanyDbContext context)
        {
            this._context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

        }

        public void Delete(T entity)
        {
            _context.Set<T>().Update(entity);

            //_context.Remove(emp);
            //_context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
   

            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int? id)
        {
            return _context.Set<T>().AsNoTracking<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
             _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
