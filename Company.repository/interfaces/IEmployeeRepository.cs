using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.repository.interfaces
{
    public interface IEmployeeRepository:IGenaricReposatory<Employees>
    {
        IEnumerable<Employees> GetEmployeeByName(string employeeName);
    }
}
