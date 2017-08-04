using System.Runtime.InteropServices;

namespace DatabaseConnectionAdmin
{
    [ComVisible(true)]
    [Guid("9986CE26-577D-497A-9B08-ED6BAE3B9DF3")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IDatabaseConnection
    {
        void CreateUser(string username, string password, int money);
        void RemoveUser(string username);
        void ChangeMoneyOfUser(string username, int money);
        void SendMoney(string from, string to, int money);
    }
}
