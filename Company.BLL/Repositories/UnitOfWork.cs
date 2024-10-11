using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDBContext _dBContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(CompanyDBContext dBContext)
        {
            _dBContext = dBContext;
            EmployeeRepository = new EmployeeRepository(dBContext);
            DepartmentRepository = new DepartmentRepository(dBContext);
        }

        public int Complete()
        {
            return _dBContext.SaveChanges();
        }

        public void Dispose()
        {
            _dBContext.Dispose();
        }
    }
}
