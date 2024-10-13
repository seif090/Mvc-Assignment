using Company.Data.context;
using Company.Data.Entities;
using Company.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.repository.repositories
{
    public class DepartmentRepository : GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {
        }
    }
}
