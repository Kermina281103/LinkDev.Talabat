using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Employees
{
    public class EmployeeToReturn
    {
        public int Id { get; set; }
        public required string  Name { get; set; }
        public decimal  Salary { get; set; }
        public string Department { get; set; } = default!;

    }
}
