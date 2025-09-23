
using LinkDev.Talabat.Shared.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Employees
{
   public  interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturn>> GetEmployeesAsync();
        Task<EmployeeToReturn> GetEmployeeAsync(int id);

    }
}
