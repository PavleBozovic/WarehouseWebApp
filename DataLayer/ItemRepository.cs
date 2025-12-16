using DataLayer.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace DataLayer
{
    public class ItemRepository : IItemRepository
    {
        public List<Item> GetAllItems()
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id, Name, Manufacturer, Quantity, Price, Category, Description FROM Inventory", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Item item = new Item
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Manufacturer = (string)sqlDataReader["Manufacturer"],
                        Quantity = (int)sqlDataReader["Quantity"],
                        Price = (decimal)sqlDataReader["Price"],
                        Category = (string)sqlDataReader["Category"],
                        Description = sqlDataReader["Description"] == DBNull.Value ? null : (string)sqlDataReader["Description"]
                    };
                    itemList.Add(item);
                }
            }
            return itemList;
        }

        public Item GetItemById(int itemId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "SELECT Id, Name, Manufacturer, Quantity, Price, Category, Description FROM Inventory WHERE Id = @ItemId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ItemId", itemId);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    Item item = new Item
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Manufacturer = (string)sqlDataReader["Manufacturer"],
                        Quantity = (int)sqlDataReader["Quantity"],
                        Price = (decimal)sqlDataReader["Price"],
                        Category = (string)sqlDataReader["Category"],
                        Description = sqlDataReader["Description"] == DBNull.Value ? null : (string)sqlDataReader["Description"]
                    };
                    return item;
                }
                else
                {
                    return null;
                }
            }
        }

        public int InsertItem(Item item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "INSERT INTO Inventory (Name, Manufacturer, Quantity, Price, Category, Description) " +
                               "VALUES (@Name, @Manufacturer, @Quantity, @Price, @Category, @Description)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Name", item.Name);
                sqlCommand.Parameters.AddWithValue("@Manufacturer", item.Manufacturer);
                sqlCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                sqlCommand.Parameters.AddWithValue("@Price", item.Price);
                sqlCommand.Parameters.AddWithValue("@Category", item.Category);
                sqlCommand.Parameters.AddWithValue("@Description", item.Description ?? (object)DBNull.Value);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int UpdateItem(Item item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "UPDATE Inventory SET Name = @Name, Manufacturer = @Manufacturer, Quantity = @Quantity, " +
                               "Price = @Price, Category = @Category, Description = @Description " +
                               "WHERE Id = @Id";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Name", item.Name);
                sqlCommand.Parameters.AddWithValue("@Manufacturer", item.Manufacturer);
                sqlCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                sqlCommand.Parameters.AddWithValue("@Price", item.Price);
                sqlCommand.Parameters.AddWithValue("@Category", item.Category);
                sqlCommand.Parameters.AddWithValue("@Description", item.Description ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Id", item.Id);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int DeleteItem(Item item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "DELETE FROM Inventory WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", item.Id);

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}