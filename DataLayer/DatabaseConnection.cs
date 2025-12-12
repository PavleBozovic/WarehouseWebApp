using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DatabaseConnection
    {
        public static string ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=WarehouseDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        public string Path;
        public string Query;
        SqlDataAdapter dataAdapter;

        public DataSet GetRequiredData(string query)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            dataAdapter = new SqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            connection.Close();
            return dataSet;
        }

        public DataSet Data
        {
            get { return GetRequiredData(Query); }
        }

        public void RefreshDatabase(DataSet database)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            commandBuilder.DataAdapter.Update(database.Tables[0]);
        }
    }
}