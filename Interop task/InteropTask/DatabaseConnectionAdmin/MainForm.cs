using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class MainForm : Form
    {
        private readonly DatabaseConnection _connection;
        public MainForm()
        {
            _connection = new DatabaseConnection();

            Properties.Settings.Default.LastTaskConnectionString = _connection.GetConnectionString(); 
            Properties.Settings.Default.Save();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            AddUser userFrom = new AddUser();
            userFrom.ShowDialog();

            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        private void RemoveUser_Click(object sender, EventArgs e)
        {
            _connection.RemoveUser(UsersGrid.SelectedRows[0].Cells[0].Value.ToString());
            usersTableAdapter.Fill(lastTaskDataSet.Users);
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }

        private void AddTransaction_Click(object sender, EventArgs e)
        {
            NewTransaction transactionForm = new NewTransaction();
            transactionForm.ShowDialog();

            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        [DllImport("DatabaseConnectionUser", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ShowGUI(IntPtr parentHwnd, [MarshalAs(UnmanagedType.BStr)] string username);

        private void ShowAsSelected_Click(object sender, EventArgs e)
        {
            ShowGUI(Handle, UsersGrid.SelectedRows[0].Cells[0].Value.ToString());
            usersTableAdapter.Fill(lastTaskDataSet.Users);
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            usersTableAdapter.Fill(lastTaskDataSet.Users);
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowAsSelected_Click(sender, e);
        }
    }
}
