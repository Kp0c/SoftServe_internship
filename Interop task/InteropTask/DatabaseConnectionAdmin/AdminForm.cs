using System.Runtime.InteropServices;

namespace DatabaseConnectionAdmin
{
    [ComVisible(true)]
    [Guid("581440C4-A6CE-4370-9BC0-A5056230BA17")]
    [ProgId("DatabaseConnectionAdmin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class AdminForm : IAdminForm
    {
        public void Show()
        {
            MainForm adminFrom = new MainForm();
            adminFrom.ShowDialog();
        }
    }
}
