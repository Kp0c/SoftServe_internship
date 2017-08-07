using System;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Validator.TryValidateAndAct(new[] {Username, Password}, new[] {Money}, money =>
            {
                DatabaseConnection connection = new DatabaseConnection();
                connection.CreateUser(Username.Text, Password.Text, money);
                Close();
            });
        }
    }
}
