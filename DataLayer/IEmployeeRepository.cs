using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        int InsertEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
        int DeleteEmployee(Employee employee);
    }
}
