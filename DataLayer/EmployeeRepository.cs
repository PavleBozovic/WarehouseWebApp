using DataLayer.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace DataLayer
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id, Name, Surname, Role, Password FROM Employees", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Employee e = new Employee();
                    e.Id = (int)sqlDataReader["Id"];
                    e.Name = (string)sqlDataReader["Name"];
                    e.Surname = (string)sqlDataReader["Surname"];
                    e.Role = (string)sqlDataReader["Role"];
                    e.Password = (string)sqlDataReader["Password"];
                    employeeList.Add(e);
                }
            }
            return employeeList;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "SELECT Id, Name, Surname, Role, Password FROM Employees WHERE Id = @EmployeeId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = (int)sqlDataReader["Id"];
                    employee.Name = (string)sqlDataReader["Name"];
                    employee.Surname = (string)sqlDataReader["Surname"];
                    employee.Role = (string)sqlDataReader["Role"];
                    employee.Password = (string)sqlDataReader["Password"];
                    return employee;
                }
                else
                {
                    return null;
                }
            }
        }

        public int InsertEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "INSERT INTO Employees (Name, Surname, Role, Password) " +
                               "VALUES (@Name, @Surname, @Role, @Password)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                sqlCommand.Parameters.AddWithValue("@Role", employee.Role);
                sqlCommand.Parameters.AddWithValue("@Password", employee.Password);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "UPDATE Employees SET Name = @Name, Surname = @Surname, Role = @Role, Password = @Password " +
                               "WHERE Id = @Id";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                sqlCommand.Parameters.AddWithValue("@Role", employee.Role);
                sqlCommand.Parameters.AddWithValue("@Password", employee.Password);
                sqlCommand.Parameters.AddWithValue("@Id", employee.Id);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int DeleteEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", employee.Id);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}