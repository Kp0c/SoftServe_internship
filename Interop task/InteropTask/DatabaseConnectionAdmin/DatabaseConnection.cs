using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DatabaseSetup;
using Microsoft.Win32;
using DatabaseSetup = DatabaseSetup.DatabaseSetup;

namespace DatabaseConnectionAdmin
{
    [ComVisible(true)]
    [Guid("581440C4-A6CE-4370-9BC0-A5078940BA17")]
    [ProgId("DatabaseConnectionAdmin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class DatabaseConnection : IDatabaseConnection
    {
        readonly SqlConnection connection;
        private readonly DbCommand commandExecutor;

        public string GetConnectionString()
        {
            return connection.ConnectionString;
        }

        private static RegistryKey GetDatabaseData()
        {
            return Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("VB and VBA Program Settings")?.OpenSubKey("LastTask")?.OpenSubKey("Database");
        }

        public DatabaseConnection(DbCommand commandExecutor = null)
        {
            RegistryKey databaseData = GetDatabaseData();
            if (databaseData == null)
            {
                var databaseSetup = new global::DatabaseSetup.DatabaseSetup();
                databaseSetup.ShowDialog();

                databaseData = GetDatabaseData();
            }

            string connectionString = "Data Source=" + databaseData.GetValue("Data Source") + ";";
            connectionString += "Initial Catalog=" + databaseData.GetValue("Initial Catalog") + ";";
            connectionString += "Trusted_Connection=" + databaseData.GetValue("Trusted_Connection") + ";";
            connectionString += "User Id=" + databaseData.GetValue("Username") + ";";
            connectionString += "Password=" + databaseData.GetValue("Password") + ";";

            connection = new SqlConnection(connectionString);

            if (commandExecutor == null)
            {
                this.commandExecutor = new SqlCommand();
            }
            else
            {
                this.commandExecutor = commandExecutor;
            }

            this.commandExecutor.Connection = connection;
        }

        private void ExecuteCommand(string command)
        {
            connection.Open();
            commandExecutor.CommandText = command;
            commandExecutor.ExecuteNonQuery();

            connection.Close();
        }

        public void CreateUser(string username, string password, int money)
        {
            ExecuteCommand("INSERT INTO Users VALUES('" + username + "', '" + password + "', " + money + ");");
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
            ExecuteCommand("EXECUTE make_transaction '" + from + "', '" + to + "', " + money + ";");
        }
    }
}
