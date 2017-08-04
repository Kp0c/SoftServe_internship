using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            _connection = new DatabaseConnection();

            Properties.Settings.Default.LastTaskConnectionString = _connection.GetProperlyConnectionString(); 
            Properties.Settings.Default.Save();

            InitializeComponent();
        }

        readonly DatabaseConnection _connection;
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
        }

        private void AddTransaction_Click(object sender, EventArgs e)
        {
            NewTransaction transactionForm = new NewTransaction();
            transactionForm.ShowDialog();

            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }

        [DllImport("DatabaseConnectionUser", CallingConvention = CallingConvention.Cdecl)]
        static extern void ShowGUI(IntPtr parentHwnd, [MarshalAs(UnmanagedType.BStr)] string username);

        private void ShowAsSelected_Click(object sender, EventArgs e)
        {
            ShowGUI(Handle, UsersGrid.SelectedRows[0].Cells[0].Value.ToString());
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            usersTableAdapter.Fill(lastTaskDataSet.Users);
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }
    }
}
