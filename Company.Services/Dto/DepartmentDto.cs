using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Dto
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string code { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

    }
}
