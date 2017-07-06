using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CreditCardManager
{
    public class CreditCard
    {
        private const int BIN_LENGTH = 6;

        /// <summary>
        /// Bin - bank identification number. It's the leading six digits of the card number
        /// </summary>
        private static uint GetBin(string number)
        {
            return Convert.ToUInt32(number.Replace(" ", string.Empty).Substring(0, BIN_LENGTH));
        }
        
        public static CreditCardVendor GetCreditCardVendor(string number)
        {
            if (number == null) throw new ArgumentNullException("card number cannot be null");

            uint bin = GetBin(number);

            if (CreditCardVendorMethods.isAmericanExpress(bin)) return CreditCardVendor.AmericanExpress;
            if (CreditCardVendorMethods.isMaestro(bin)) return CreditCardVendor.Maestro;
            if (CreditCardVendorMethods.isMasterCard(bin)) return CreditCardVendor.MasterCard;
            if (CreditCardVendorMethods.isVisa(bin)) return CreditCardVendor.VISA;
            if (CreditCardVendorMethods.isJcb(bin)) return CreditCardVendor.JCB;

            return CreditCardVendor.Unknow;
        }

        private static int[] LuhnAlgorithmConversion(int[] numbers)
        {
            int startPosition = (numbers.Length - 1) % 2 == 0 ? numbers.Length - 1 : numbers.Length - 2;
            for (int i = startPosition; i >= 0; i -= 2)
            {
                numbers[i] *= 2;
                //add digits of result from the previous operation
                if (numbers[i] >= 10)   
                    numbers[i] -= 9;
            }

            return numbers;
        }

        /// <summary>
        /// Convert number string to digits array
        /// </summary>
        private static int[] ConvertStringToIntArray(string number)
        {
            return number.Replace(" ", string.Empty).Select(c => (int)char.GetNumericValue(c)).ToArray();
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

        public static string GenerateNextCreditCardNumber(string creditCardNumber)
        {
            if (creditCardNumber == null) throw new ArgumentNullException("card number cannot be null");
            
            creditCardNumber = creditCardNumber.Replace(" ", string.Empty);
        
            //generate next credit card number
            Random random = new Random(creditCardNumber.GetHashCode());

            const int MIN_9_DIGITS_NUMBER = 100000000;
            const int MAX_9_DIGITS_NUMBER = 999999999;

            string nextCreditCardNumber = creditCardNumber.Substring(0, BIN_LENGTH) + random.Next(MIN_9_DIGITS_NUMBER, MAX_9_DIGITS_NUMBER).ToString() + 0;

            //set check digit
            int[] numbers = LuhnAlgorithmConversion(ConvertStringToIntArray(nextCreditCardNumber));
            int checkDigit = (10 - (numbers.Sum(s => s) % 10));
            nextCreditCardNumber = nextCreditCardNumber.Substring(0, nextCreditCardNumber.Length - 1) + checkDigit.ToString();

            return nextCreditCardNumber;
        }
    }
}
