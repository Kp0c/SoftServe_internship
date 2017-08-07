using System;
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

        private void Apply_Click(object sender, EventArgs e)
        {
            Validator.TryValidateAndAct(new[] {From, To}, new[] {Sum}, sum =>
            {
                DatabaseConnection connection = new DatabaseConnection();
                connection.SendMoney(From.Text, To.Text, sum);
                Close();
            });
        }
    }
}
