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
    public class EmployeeRepository : GenericRepository<Employees>, IEmployeeRepository
    {
        private readonly CompanyDbContext _dbContext;
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public IEnumerable<Employees> GetEmployeeByName(string employeeName)
        {
            return _dbContext.Employees.Where(x => x.Name.Trim().ToLower().Contains(employeeName.Trim().ToLower())).ToList();    
        }
    }
}
