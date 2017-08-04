using System;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public partial class NewTransaction : Form
    {
        public NewTransaction(string username = "admin")
        {
            InitializeComponent();

            if (username != "admin")
            {
                From.Text = username;
                From.ReadOnly = true;
                From.HideSelection = true;
                To.Select();
            }
        }

        private static bool ValidateField(string field, string name)
        {
            if (String.IsNullOrEmpty(field))
            {
                MessageBox.Show(name + @" cannot be empty.");
                return false;
            }

            if (field.Contains('\'') || field.Contains(' '))
            {
                MessageBox.Show(name + @" cannot contain ' or space symbol");
                return false;
            }

            return true;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int sum;
            if (Int32.TryParse(Sum.Text, out sum))
            {
                if (ValidateField(From.Text, "Username") && ValidateField(To.Text, "Password"))
                {
                    DatabaseConnection connection = new DatabaseConnection();
                    connection.SendMoney(From.Text, To.Text, sum);
                    Close();
                }
            }
            else
            {
                MessageBox.Show(@"Wrong ""Money"" field value");
                Sum.Text = "";
                Sum.Select();

            }
        }
    }
}
