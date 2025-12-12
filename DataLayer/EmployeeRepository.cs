using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class EmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Employees";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Employee e = new Employee();
                    e.Id = sqlDataReader.GetInt32(0);
                    e.Name = sqlDataReader.GetString(1);
                    e.Surname = sqlDataReader.GetString(2);
                    e.Role = sqlDataReader.GetString(3);
                    e.Password = sqlDataReader.GetString(4);
                    employeeList.Add(e);
                }
            }
            return employeeList;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Employees WHERE Id = @EmployeeId";
                sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = sqlDataReader.GetInt32(0);
                    employee.Name = sqlDataReader.GetString(1);
                    employee.Surname = sqlDataReader.GetString(2);
                    employee.Role = sqlDataReader.GetString(3);
                    employee.Password = sqlDataReader.GetString(4);
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
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = string.Format("DELETE FROM Employees WHERE Id='{0}'", employee.Id);
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
