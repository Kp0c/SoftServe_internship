using System;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    public static class Validator
    {
        private static IDialogService _dialogService = new MessageBoxService();

        public static void SetDialogService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private static bool ValidateTextFields(TextBox[] fields)
        {
            bool isValid = true;

            foreach (TextBox field in fields)
            {
                if (String.IsNullOrEmpty(field.Text))
                {
                    _dialogService.ShowMessageBox($@"""{field.Name}"" cannot be empty.");
                    isValid = false;
                    break;
                }

                if (field.Text.Contains('\'') || field.Text.Contains(' '))
                {
                    _dialogService.ShowMessageBox($@"""{field.Name}"" cannot contain ' or space symbol");
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
                    _dialogService.ShowMessageBox($@"Wrong {field.Name} field value");
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
        
        public delegate void Act(int money);
        public static bool TryValidateAndAct(TextBox[] textFields, TextBox[] intFields, Act act)
        {
            int sum;
            if (ValidateTextFields(textFields) && ValidateIntFields(intFields, out sum))
            {
                act(sum);
                return true;
            }

            return false;
        }
    }
}
