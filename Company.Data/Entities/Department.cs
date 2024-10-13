using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string code { get; set; }
        public ICollection<Employees> Employees { get; set; } = new List<Employees>();
    }
}
