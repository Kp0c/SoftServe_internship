using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
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
        readonly SqlConnection connection;

        public string GetProperlyConnectionString()
        {
            return connection.ConnectionString;
        }

        public DatabaseConnection()
        {
            RegistryKey databaseData = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("VB and VBA Program Settings").OpenSubKey("LastTask").OpenSubKey("Database");

            string connectionString = "Data Source=" + databaseData.GetValue("Data Source") + ";";
            connectionString += "Initial Catalog=" + databaseData.GetValue("Initial Catalog") + ";";
            connectionString += "Trusted_Connection=" + databaseData.GetValue("Trusted_Connection") + ";";
            connectionString += "User Id=" + databaseData.GetValue("Username") + ";";
            connectionString += "Password=" + databaseData.GetValue("Password") + ";";

            connection = new SqlConnection(connectionString);
        }

        private void ExecuteCommand(string command)
        {
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = command;
            sqlCommand.Connection = connection;
            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void CreateUser(string username, string password, int money)
        {
            ExecuteCommand("INSERT INTO Users VALUES('" + username + "','" + password + "', " + money.ToString() + ");");
         }

        public void RemoveUser(string username)
        {
            ExecuteCommand("EXECUTE remove_user '" + username + "';");
        }

        public void ChangeMoneyOfUser(string username, int money)
        {
            ExecuteCommand("UPDATE Users SET money=" + money.ToString() + " WHERE username='" + username + "';");
        }

        public void SendMoney(string from, string to, int money)
        {
            ExecuteCommand("EXECUTE make_transaction '" + from + "', '" + to + "', " + money.ToString());
        }

    }
}
