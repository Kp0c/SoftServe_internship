using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
            adminFrom.Show();
        }
    }
}
