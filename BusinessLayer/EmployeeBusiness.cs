using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class EmployeeBusiness
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeBusiness()
        {
            this.employeeRepository = new EmployeeRepository();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return employeeRepository.GetEmployeeById(employeeId);
        }

        public List<Employee> GetAllEmployees()
        {
            return this.employeeRepository.GetAllEmployees();
        }

        public bool InsertEmployee(Employee employee)
        {
            if (this.employeeRepository.InsertEmployee(employee) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            if (this.employeeRepository.UpdateEmployee(employee) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteEmployee(Employee employee)
        {
            if (this.employeeRepository.DeleteEmployee(employee) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
