using System;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    static class Validator
    {
        public delegate void Act(int money);

        private static bool ValidateField(TextBox field)
        {
            if (String.IsNullOrEmpty(field.Text))
            {
                MessageBox.Show($@"""{field.Name}"" cannot be empty.");
                return false;
            }

            if (field.Text.Contains('\'') || field.Text.Contains(' '))
            {
                MessageBox.Show($@"""{field.Name}"" cannot contain ' or space symbol");
                return false;
            }

            return true;
        }

        //TODO: rename fields
        public static void TryValidateAndAct(TextBox text1, TextBox text2, TextBox int1, Act act)
        {
            int sum;
            if (Int32.TryParse(int1.Text, out sum))
            {
                if (Validator.ValidateField(text1) && Validator.ValidateField(text2))
                {
                    act(sum);
                }
            }
            else
            {
                MessageBox.Show($@"Wrong {int1.Name} field value");
                int1.Text = "";
                int1.Select();
            }
        }
    }
}
