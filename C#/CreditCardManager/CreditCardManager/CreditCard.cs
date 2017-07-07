using System;
using System.Linq;

namespace CreditCardManager
{
    public class CreditCard
    {
        private const int BIN_LENGTH = 6;

        /// <summary>
        /// Bin - bank identification number. It's the leading six digits of the card number
        /// </summary>
        private static int GetBin(string number)
        {
            return Convert.ToInt32(number.Replace(" ", string.Empty).Substring(0, BIN_LENGTH));
        }

        public static CreditCardVendor GetCreditCardVendor(string number)
        {
            if (number == null) throw new ArgumentNullException("card number cannot be null");

            int bin = GetBin(number);

            CreditCardVendor ccv = CreditCardVendorMethods.GetCreditCardVendorFromBin(bin);

            return CreditCardVendorMethods.CheckFormat(ccv, number) ? ccv : CreditCardVendor.Unknow;
        }

        private static int[] LuhnAlgorithmConversion(int[] numbers)
        {
            int startPosition = numbers.Length - 2;
            for (int i = startPosition; i >= 0; i -= 2)
            {
                numbers[i] *= 2;
                //add digits of result from the previous operation
                if (numbers[i] >= 10)
                    numbers[i] -= 9;
            }

            return numbers;
        }

        private static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", string.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
        }

        private static string ConvertIntArrayToString(int[] number)
        {
            return new string(number.Select(c => Convert.ToChar(c + '0')).ToArray());
        }

        public static bool IsCreditCardNumberValid(string creditCardNumber)
        {
            if (creditCardNumber == null) throw new ArgumentNullException("card number cannot be null");

            int[] numbers = ConvertStringToIntArray(creditCardNumber);

            numbers = LuhnAlgorithmConversion(numbers);

            if (numbers.Sum(s => s) % 10 == 0)
                return true;
            else
                return false;
        }

        private static string SetCorrectCheckDigit(string creditCardNumber)
        {
            //set last char to "0"
            creditCardNumber = creditCardNumber.Substring(0, creditCardNumber.Length - 1) + "0";

            int[] numbers = LuhnAlgorithmConversion(ConvertStringToIntArray(creditCardNumber));
            int checkDigit = 10 - numbers.Sum(s => s) % 10;
            checkDigit %= 10;
            creditCardNumber = creditCardNumber.Substring(0, creditCardNumber.Length - 1) + checkDigit;
            return creditCardNumber;
        }

        public static string GenerateNextRandomCreditCardNumber(string creditCardNumber)
        {
            if (creditCardNumber == null) throw new ArgumentNullException("card number cannot be null");

            creditCardNumber = creditCardNumber.Replace(" ", string.Empty);

            //generate next credit card number
            Random random = new Random(creditCardNumber.GetHashCode());

            const int MIN_9_DIGITS_NUMBER = 100000000;
            const int MAX_9_DIGITS_NUMBER = 999999999;

            string nextCreditCardNumber = creditCardNumber.Substring(0, BIN_LENGTH) + random.Next(MIN_9_DIGITS_NUMBER, MAX_9_DIGITS_NUMBER) + 0;

            return SetCorrectCheckDigit(nextCreditCardNumber);
        }

        private static string IncrementCardNumber(string creditCardNumber)
        {
            int[] numbers = ConvertStringToIntArray(creditCardNumber);

            for (int i = numbers.Length - 2; i >= 0; i--)
            {
                numbers[i]++;
                if (numbers[i] >= 10) numbers[i] -= 10;
                else break;
            }

            return ConvertIntArrayToString(numbers);
        }

        private static string TryIncrementBinsRange(CreditCardVendor ccv, string creditCardNumber)
        {
            Range[] ranges = new Range[CreditCardVendorMethods.bins[ccv].Length];
            CreditCardVendorMethods.bins[ccv].CopyTo(ranges, 0);

            Range currentRange = ranges.First(range => range.IsInRange(GetBin(creditCardNumber)));
            int currentIndex = Array.IndexOf(ranges, currentRange);

            if (currentIndex < ranges.Length - 1)
            {
                //set new bin range with 00...00 card nubmer
                return ranges[currentIndex + 1].from + new string('0', creditCardNumber.Length - BIN_LENGTH);
            }

            return creditCardNumber;
        }

        private static string TryIncrementLength(CreditCardVendor ccv, string creditCardNumber)
        {
            Range[] ranges = new Range[CreditCardVendorMethods.lengths[ccv].Length];
            CreditCardVendorMethods.lengths[ccv].CopyTo(ranges, 0);

            Range currentRange = ranges.First(range => range.IsInRange(creditCardNumber.Length));
            int currentIndex = Array.IndexOf(ranges, currentRange);

            //set new length with 00...00 card nubmer and minimal bin number
            string newCreditCardNumber = CreditCardVendorMethods.bins[ccv][0].from.ToString();
            if (creditCardNumber.Length != ranges[currentIndex].to)
            {
                return newCreditCardNumber + new string('0', creditCardNumber.Length + 1 - BIN_LENGTH);
            }
            else if (currentIndex < ranges.Length - 1)
            {
                return newCreditCardNumber + new string('0', ranges[currentIndex + 1].from - BIN_LENGTH);
            }

            return creditCardNumber;
        }

        public static string GenerateNextCreditCardNumber(string creditCardNumber)
        {
            if (creditCardNumber == null) throw new ArgumentNullException("card number cannot be null");

            creditCardNumber = creditCardNumber.Replace(" ", string.Empty);

            CreditCardVendor ccv = CreditCardVendorMethods.GetCreditCardVendorFromBin(GetBin(creditCardNumber));

            string newCreditCardNumber;

            newCreditCardNumber = IncrementCardNumber(creditCardNumber);
            if (ccv == CreditCardVendorMethods.GetCreditCardVendorFromBin(GetBin(newCreditCardNumber)))
            {
                return SetCorrectCheckDigit(newCreditCardNumber);
            }

            newCreditCardNumber = TryIncrementBinsRange(ccv, creditCardNumber);
            if (newCreditCardNumber != creditCardNumber && ccv == CreditCardVendorMethods.GetCreditCardVendorFromBin(GetBin(newCreditCardNumber)))
            {
                return SetCorrectCheckDigit(newCreditCardNumber);
            }

            newCreditCardNumber = TryIncrementLength(ccv, creditCardNumber);
            if (newCreditCardNumber != creditCardNumber && ccv == CreditCardVendorMethods.GetCreditCardVendorFromBin(GetBin(newCreditCardNumber)))
            {
                return SetCorrectCheckDigit(newCreditCardNumber);
            }

            throw new ArgumentException("Cannot generate more credit card numbers for this vendor");
        }
    }
}
