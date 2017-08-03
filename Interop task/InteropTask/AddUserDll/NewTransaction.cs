using System;
using System.Linq;
using System.Windows.Forms;

namespace NewTransactionDll
{
    public class NewTransaction
    {
        public NewTransaction(string username)
        {
            DatabaseConnectionAdmin.NewTransaction transactionForm = new DatabaseConnectionAdmin.NewTransaction(username);
            transactionForm.ShowDialog();
        }
    }
}
