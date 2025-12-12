using DataLayer;
using DataLayer.Models;
using System.Collections.Generic;
using System;

namespace BusinessLayer
{
    public class EmployeeBusiness
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeBusiness(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        public List<Employee> GetAllEmployees()
        {
            return this._employeeRepository.GetAllEmployees();
        }

        public bool InsertEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Role))
            {
                return false;
            }
            if (this._employeeRepository.InsertEmployee(employee) > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            if (this._employeeRepository.UpdateEmployee(employee) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteEmployee(Employee employee)
        {
            if (this._employeeRepository.DeleteEmployee(employee) > 0)
            {
                return true;
            }
            return false;
        }
    }
}