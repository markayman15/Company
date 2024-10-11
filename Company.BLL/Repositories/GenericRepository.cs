using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly CompanyDBContext _dBContext;

        public GenericRepository(CompanyDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void Add(T item)
        {
            _dBContext.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            _dBContext.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dBContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
            }
            return _dBContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dBContext.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _dBContext.Set<T>().Update(item);
        }
    }
}
