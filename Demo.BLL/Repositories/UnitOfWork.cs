using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            DepartmentRepository = new DepartmentRepository(appDbContext);
            EmployeeRepository = new EmployeeRepository(appDbContext);
            _appDbContext = appDbContext;
        }

        public int Complete()
              => _appDbContext.SaveChanges();
        public void Dispose()
        {
            _appDbContext.Dispose();
        }

    }
}
