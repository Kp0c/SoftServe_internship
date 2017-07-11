using System.Linq;

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
