﻿using System.Runtime.InteropServices;

namespace DatabaseConnectionAdmin
{
    [ComVisible(true)]
    [Guid("9986CE26-577D-497A-9B08-ED6BAE3B9DF4")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IAdminForm
    {
       void Show();
    }
}
