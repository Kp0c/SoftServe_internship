using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class MainForm : Form
    {
        private readonly DatabaseConnection connection;
        public MainForm()
        {
            connection = new DatabaseConnection();

            Properties.Settings.Default.LastTaskConnectionString = connection.GetConnectionString(); 
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
            connection.RemoveUser(UsersGrid.SelectedRows[0].Cells[0].Value.ToString());
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        private void AddTransaction_Click(object sender, EventArgs e)
        {
            NewTransaction transactionForm = new NewTransaction();
            transactionForm.ShowDialog();

            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        [DllImport("DatabaseConnectionUser", CallingConvention = CallingConvention.Cdecl)]
        static extern void ShowGUI(IntPtr parentHwnd, [MarshalAs(UnmanagedType.BStr)] string username);

        private void ShowAsSelected_Click(object sender, EventArgs e)
        {
            ShowGUI(Handle, UsersGrid.SelectedRows[0].Cells[0].Value.ToString());
            usersTableAdapter.Fill(lastTaskDataSet.Users);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            usersTableAdapter.Fill(lastTaskDataSet.Users);
            transactionsTableAdapter.Fill(lastTaskDataSet.Transactions);
        }
    }
}
