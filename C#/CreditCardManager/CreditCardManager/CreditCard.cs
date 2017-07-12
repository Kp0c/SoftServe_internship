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
            return Convert.ToInt32(number.Substring(0, BIN_LENGTH));
        }

        public static CreditCardVendor GetCreditCardVendor(string number)
        {
            string normalizedNumber = CreditCardHelper.TryToNormalizeNumber(number);

            int bin = GetBin(normalizedNumber);

            CreditCardVendor ccv = CreditCardVendorMethods.GetCreditCardVendorFromBin(bin);

            return CreditCardVendorMethods.CheckFormat(ccv, normalizedNumber) ? ccv : CreditCardVendor.Unknow;
        }

        public static bool IsCreditCardNumberValid(string creditCardNumber)
        {
            string normalizedNumber = CreditCardHelper.TryToNormalizeNumber(creditCardNumber);

            int[] numbers = CreditCardHelper.ConvertStringToIntArray(normalizedNumber);
            numbers = CreditCardHelper.LuhnAlgorithmConversion(numbers);

            return numbers.Sum(s => s) % 10 == 0;
        }

        private static string SetCorrectCheckDigit(string creditCardNumber)
        {
            //set last char to "0"
            creditCardNumber = creditCardNumber.Substring(0, creditCardNumber.Length - 1) + "0";

            int[] numbers = CreditCardHelper.LuhnAlgorithmConversion(CreditCardHelper.ConvertStringToIntArray(creditCardNumber));

            int checkDigit = (10 - numbers.Sum(s => s) % 10) % 10;

            return creditCardNumber.Substring(0, creditCardNumber.Length - 1) + checkDigit;
        }

        public static string GenerateNextRandomCreditCardNumber(string creditCardNumber)
        {
            string normalizedNumber = CreditCardHelper.TryToNormalizeNumber(creditCardNumber);

            //generate next credit card number
            Random random = new Random(normalizedNumber.GetHashCode());

            const int MIN_9_DIGITS_NUMBER = 100000000;
            const int MAX_9_DIGITS_NUMBER = 999999999;

            string nextCreditCardNumber = normalizedNumber.Substring(0, BIN_LENGTH) + random.Next(MIN_9_DIGITS_NUMBER, MAX_9_DIGITS_NUMBER) + 0;

            return SetCorrectCheckDigit(nextCreditCardNumber);
        }

        private static bool TryIncrementCardNumber(CreditCardVendor ccv, string creditCardNumber, out string resultCreditCardNumber)
        {
            int[] numbers = CreditCardHelper.ConvertStringToIntArray(creditCardNumber);

            for (int i = numbers.Length - 2; i >= 0; i--)
            {
                numbers[i]++;
                if (numbers[i] >= 10)
                {
                    numbers[i] -= 10;
                }
                else break;
            }

            resultCreditCardNumber = CreditCardHelper.ConvertIntArrayToString(numbers);

            return ccv == GetCreditCardVendor(resultCreditCardNumber);
        }

        private static bool TryIncrementBinsRange(CreditCardVendor ccv, string creditCardNumber, out string resultCreditCardNumber)
        {
            Range[] ranges = new Range[CreditCardVendorMethods.bins[ccv].Length];
            CreditCardVendorMethods.bins[ccv].CopyTo(ranges, 0);

            int bin = GetBin(creditCardNumber);
            Range currentRange = ranges.First(range => range.IsInRange(bin));
            int currentIndex = Array.IndexOf(ranges, currentRange);

            if (currentIndex < ranges.Length - 1)
            {
                //set new bin range with 00...00 card nubmer
                resultCreditCardNumber = ranges[currentIndex + 1].From + new string('0', creditCardNumber.Length - BIN_LENGTH);
                return true;
            }
            else
            {
                resultCreditCardNumber = String.Empty;
                return false;
            }
        }

        private static bool TryIncrementLength(CreditCardVendor ccv, string creditCardNumber, out string resultCreditCardNumber)
        {
            Range[] ranges = new Range[CreditCardVendorMethods.lengths[ccv].Length];
            CreditCardVendorMethods.lengths[ccv].CopyTo(ranges, 0);

            int currentLength = creditCardNumber.Length;
            Range currentRange = ranges.First(range => range.IsInRange(currentLength));
            int currentIndex = Array.IndexOf(ranges, currentRange);

            //set new length with 00...00 card nubmer and minimal bin number
            resultCreditCardNumber = CreditCardVendorMethods.bins[ccv][0].From.ToString();
            if (currentLength != ranges[currentIndex].To)
            {
                resultCreditCardNumber += new string('0', currentLength + 1 - BIN_LENGTH);
                return true;
            }
            else if (currentIndex < ranges.Length - 1)
            {
                resultCreditCardNumber += new string('0', ranges[currentIndex + 1].From - BIN_LENGTH);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GenerateNextCreditCardNumber(string creditCardNumber)
        {
            string normalizedNumber = CreditCardHelper.TryToNormalizeNumber(creditCardNumber);

            CreditCardVendor ccv = GetCreditCardVendor(normalizedNumber);

            if (ccv == CreditCardVendor.Unknow)
            {
                throw new ArgumentException("Cannot generate next credit card number for unknow vendor.");
            }

            string newCreditCardNumber;

            if (!TryIncrementCardNumber(ccv, normalizedNumber, out newCreditCardNumber))
            {
                if (!TryIncrementBinsRange(ccv, normalizedNumber, out newCreditCardNumber))
                {
                    if (!TryIncrementLength(ccv, normalizedNumber, out newCreditCardNumber))
                    {
                        throw new ArgumentException("Cannot generate more credit card numbers for this vendor.");
                    }
                }
            }

            return SetCorrectCheckDigit(newCreditCardNumber);
        }
    }
}
