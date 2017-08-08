using System.Windows.Forms;

namespace DatabaseConnectionAdmin
{
    class MessageBoxService : IDialogService
    {
        public void ShowMessageBox(string text)
        {
            MessageBox.Show(text);
        }
    }
}
