using DatabaseConnectionAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnectionAdminTests
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        private MockDbCommand _mockDbCommand;

        private DatabaseConnection _dbConnection;
        [TestInitialize]
        public void Setup()
        {
            _mockDbCommand = new MockDbCommand();
            _dbConnection = new DatabaseConnection(_mockDbCommand);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            _dbConnection.CreateUser("username", "password", 1000);

            Assert.AreEqual(@"INSERT INTO Users VALUES('username', 'password', 1000);", _mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            _dbConnection.RemoveUser("username");
            Assert.AreEqual(@"EXECUTE remove_user 'username';", _mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void ChangeMoneyOfUserTest()
        {
            _dbConnection.ChangeMoneyOfUser("username", 1000);
            Assert.AreEqual(@"UPDATE Users SET money=1000 WHERE username='username';", _mockDbCommand.ExecutedCommandText);
        }

        [TestMethod]
        public void SendMoneyTest()
        {
            _dbConnection.SendMoney("1", "2", 500);
            Assert.AreEqual(@"EXECUTE make_transaction '1', '2', 500;", _mockDbCommand.ExecutedCommandText);
        }
    }
}