using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            connection = new DatabaseConnection();

            Properties.Settings.Default.LastTaskConnectionString = connection.GetProperlyConnectionString(); 
            Properties.Settings.Default.Save();

            InitializeComponent();
        }

        DatabaseConnection connection;
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
