using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CreditCardManager
{
    static class CreditCardHelper
    {
        private static void ValidateNumber(string number)
        {
            string notNumbersPattern = "([^0-9])";

            if (Regex.IsMatch(number, notNumbersPattern))
            {
                throw new ArgumentException("Invalid card number.");
            }
        }

        public static string TryToNormalizeNumber(string number)
        {
            if (String.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException("Card cannot be null or empty.");
            }

            number = number.Replace(" ", String.Empty);

            ValidateNumber(number);

            return number;
        }

        public static int[] LuhnAlgorithmConversion(int[] numbers)
        {
            int startPosition = numbers.Length - 2;
            for (int i = startPosition; i >= 0; i -= 2)
            {
                numbers[i] *= 2;
                //add digits of result from the previous operation
                if (numbers[i] >= 10)
                {
                    numbers[i] -= 9;
                }
            }

            return numbers;
        }

        public static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", String.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
        }

        public static string ConvertIntArrayToString(int[] number)
        {
            return new string(number.Select(c => Convert.ToChar(c + '0')).ToArray());
        }
    }
}
