using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Employees;
using LinkDev.Talabat.Domain.Specifications.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Employees
{
    class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
    {
        public async Task<EmployeeToReturn> GetEmployeeAsync(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications();
            var employee = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
            var employeeToReturn = mapper.Map<EmployeeToReturn>(employee);
            return employeeToReturn;
        }
   
        public  async Task<IEnumerable<EmployeeToReturn>> GetEmployeesAsync()
        {
            var spec = new EmployeeWithDepartmentSpecifications();
            var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
            var employeeToReturn = mapper.Map<IEnumerable<EmployeeToReturn>>(employees);
            return employeeToReturn;
        }
    }
}
