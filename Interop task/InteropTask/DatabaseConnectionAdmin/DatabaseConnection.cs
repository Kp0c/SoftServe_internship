using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DatabaseConnectionAdmin
{
    [ComVisible(true)]
    [Guid("581440C4-A6CE-4370-9BC0-A5078940BA17")]
    [ProgId("DatabaseConnectionAdmin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class DatabaseConnection : IDatabaseConnection
    {
        readonly SqlConnection _connection;

        public string GetConnectionString()
        {
            return _connection.ConnectionString;
        }

        public DatabaseConnection()
        {
            RegistryKey databaseData = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("VB and VBA Program Settings")?.OpenSubKey("LastTask")?.OpenSubKey("Database");

            Debug.Assert(databaseData != null, "databaseData cannot be null");

            string connectionString = "Data Source=" + databaseData.GetValue("Data Source") + ";";
            connectionString += "Initial Catalog=" + databaseData.GetValue("Initial Catalog") + ";";
            connectionString += "Trusted_Connection=" + databaseData.GetValue("Trusted_Connection") + ";";
            connectionString += "User Id=" + databaseData.GetValue("Username") + ";";
            connectionString += "Password=" + databaseData.GetValue("Password") + ";";

            _connection = new SqlConnection(connectionString);
        }

        private void ExecuteCommand(string command)
        {
            _connection.Open();
            SqlCommand sqlCommand = new SqlCommand(command, _connection);
            sqlCommand.ExecuteNonQuery();
            _connection.Close();
        }

        public void CreateUser(string username, string password, int money)
        {
            ExecuteCommand("INSERT INTO Users VALUES('" + username + "','" + password + "', " + money + ");");
         }

        public void RemoveUser(string username)
        {
            ExecuteCommand("EXECUTE remove_user '" + username + "';");
        }

        public void ChangeMoneyOfUser(string username, int money)
        {
            ExecuteCommand("UPDATE Users SET money=" + money + " WHERE username='" + username + "';");
        }

        public void SendMoney(string from, string to, int money)
        {
            ExecuteCommand("EXECUTE make_transaction '" + from + "', '" + to + "', " + money);
        }
    }
}
