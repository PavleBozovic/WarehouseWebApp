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
                    Employee e = new Employee
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Surname = (string)sqlDataReader["Surname"],
                        Role = (string)sqlDataReader["Role"],
                        Password = (string)sqlDataReader["Password"]
                    };
                    employeeList.Add(e);
                }
            }
            return employeeList;
        }

        public Employee GetEmployeeById(int Id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "SELECT Id, Name, Surname, Role, Password FROM Employees WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", Id);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Surname = (string)sqlDataReader["Surname"],
                        Role = (string)sqlDataReader["Role"],
                        Password = (string)sqlDataReader["Password"]
                    };
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

        public Employee GetEmployeeByIdAndPassword(int Id, string password)
        {
            Employee employee = null;
            string connectionString = DatabaseConnection.ConnectionString;

            string sql = "SELECT Id, Name, Surname, Role FROM Employees WHERE Id = @Id AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Surname = reader.GetString(2),
                            Role = reader.GetString(3),
                        };
                    }
                }
            }
            return employee;
        }
    }
}