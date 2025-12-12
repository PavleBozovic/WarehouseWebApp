using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ItemRepository : IItemRepository
    {
        public List<Item> GetAllItems()
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Inventory";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Item item = new Item();
                    item.Id = sqlDataReader.GetInt32(0);
                    item.Name = sqlDataReader.GetString(1);
                    item.Manufacturer = sqlDataReader.GetString(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
                    item.Description = sqlDataReader.GetString(4);
                    itemList.Add(item);
                }
            }
            return itemList;
        }
        public Item GetItemById(int itemId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Inventory WHERE Id = @ItemId";
                sqlCommand.Parameters.AddWithValue("@ItemId", itemId);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    Item item = new Item();
                    item.Id = sqlDataReader.GetInt32(0);
                    item.Name = sqlDataReader.GetString(1);
                    item.Manufacturer = sqlDataReader.GetString(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
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
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = string.Format("DELETE FROM Inventory WHERE Id='{0}'", item.Id);
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
