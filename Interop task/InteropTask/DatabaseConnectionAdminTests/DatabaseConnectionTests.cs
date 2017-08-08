using DatabaseConnectionAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnectionAdminTests
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        private MockDbCommand mockDbCommand;

        private DatabaseConnection dbConnection;
        [TestInitialize]
        public void Setup()
        {
            mockDbCommand = new MockDbCommand();
            dbConnection = new DatabaseConnection(mockDbCommand);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            dbConnection.CreateUser("username", "password", 1000);

            Assert.AreEqual(@"INSERT INTO Users VALUES('username', 'password', 1000);", mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            dbConnection.RemoveUser("username");
            Assert.AreEqual(@"EXECUTE remove_user 'username';", mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void ChangeMoneyOfUserTest()
        {
            dbConnection.ChangeMoneyOfUser("username", 1000);
            Assert.AreEqual(@"UPDATE Users SET money=1000 WHERE username='username';", mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void SendMoneyTest()
        {
            dbConnection.SendMoney("1", "2", 500);
            Assert.AreEqual(@"EXECUTE make_transaction '1', '2', 500;", mockDbCommand.ExecutedCommandText);
        }
    }
}