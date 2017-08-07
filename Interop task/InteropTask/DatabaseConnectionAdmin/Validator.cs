using System;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    static class Validator
    {

        private static bool ValidateTextFields(TextBox[] fields)
        {
            bool isValid = true;

            foreach (TextBox field in fields)
            {
                if (String.IsNullOrEmpty(field.Text))
                {
                    MessageBox.Show($@"""{field.Name}"" cannot be empty.");
                    isValid = false;
                    break;
                }

                if (field.Text.Contains('\'') || field.Text.Contains(' '))
                {
                    MessageBox.Show($@"""{field.Name}"" cannot contain ' or space symbol");
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        private static bool ValidateIntFields(TextBox[] fields, out int sum)
        {
            bool isValid = true;

            sum = 0;
            foreach (TextBox field in fields)
            {
                int singleValue;
                if (Int32.TryParse(field.Text, out singleValue))
                {
                    sum += singleValue;
                }
                else
                {
                    MessageBox.Show($@"Wrong {field.Name} field value");
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
        
        public delegate void Act(int money);
        public static void TryValidateAndAct(TextBox[] textFields, TextBox[] intFields, Act act)
        {
            int sum;
            if (ValidateTextFields(textFields) && ValidateIntFields(intFields, out sum))
            {
                act(sum);
            }
        }
    }
}
