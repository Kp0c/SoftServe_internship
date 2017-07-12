using System.Linq;

namespace Citizens
{
    static class CitizenRegistryHelper
    {
        public static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", string.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
        }

        public static int CalculateControlNumber(string vat)
        {
            int[] numbers = CitizenRegistryHelper.ConvertStringToIntArray(vat);

            return (-numbers[0] + 5 * numbers[1] + 7 * numbers[2] + 9 * numbers[3] + 4 * numbers[4]
                + 6 * numbers[5] + 10 * numbers[6] + 5 * numbers[7] + 7 * numbers[8]) % 11 % 10;
        }
    }
}
