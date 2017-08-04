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
