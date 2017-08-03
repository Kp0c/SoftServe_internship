using System;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private bool ValidateField(string field, string name)
        {
            if(String.IsNullOrEmpty(field))
            {
                MessageBox.Show(name + " cannot be empty.");
                return false;
            }

            if(field.Contains('\'') || field.Contains(' '))
            {
                MessageBox.Show(name + " cannot contain ' or space symbol");
                return false;
            }

            return true;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            int money = 0;
            if(Int32.TryParse(Money.Text, out money))
            {
                if (ValidateField(Username.Text, "Username") && ValidateField(Password.Text, "Password"))
                {
                    DatabaseConnection connection = new DatabaseConnection();
                    connection.CreateUser(Username.Text, Password.Text, money);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Wrong \"Money\" field value");
                Money.Text = "";
                Money.Select();
               
            }
        }
    }
}
