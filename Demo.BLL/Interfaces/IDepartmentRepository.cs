using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAll();
        public Department Get(int id);
        public int Add(Department department);
        public int Update(Department department);
        public int Delete(Department department);
    }
}
