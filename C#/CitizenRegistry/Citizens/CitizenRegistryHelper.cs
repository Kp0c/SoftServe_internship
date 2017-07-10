using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizens
{
    class CitizenRegistryHelper
    {
        public static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", string.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
        }
    }
}
