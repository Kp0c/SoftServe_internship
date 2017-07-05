using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardManager
{
    public enum CreditCardVendor
    {
        AmericanExpress,
        Maestro,
        MasterCard,
        VISA,
        JCB,
        Unknow,
    }

    public class CreditCard
    {
        private static uint GetBin(string number)
        {
            return Convert.ToUInt32(number.Replace(" ", string.Empty).Substring(0, 6));
        }

        #region different vendors checks
        private static bool isAmericanExpress(uint bin)
        {
            uint firstTwoNumbers = bin / 10000;
            if (firstTwoNumbers == 34 || firstTwoNumbers == 37)
                return true;
            else
                return false;
        }

        private static bool isMaestro(uint bin)
        {
            uint firstTwoNumbers = bin / 10000;
            if (firstTwoNumbers / 10 == 6
                || firstTwoNumbers == 50
                || (firstTwoNumbers >= 56 && firstTwoNumbers <= 58))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool isMasterCard(uint bin)
        {
            uint firstFourNumbers = bin / 100;
            uint firstTwoNumbers = bin / 10000;
            if((firstFourNumbers >= 2221 && firstFourNumbers <= 2720)
                || (firstTwoNumbers >= 51 && firstTwoNumbers <= 55))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool isVisa(uint bin)
        {
            if (bin / 100000 == 4)
                return true;
            else
                return false;
        }

        private static bool isJcb(uint bin)
        {
            uint firstFourNumbers = bin / 100;
            if (firstFourNumbers >= 3528 && firstFourNumbers <= 3589)
                return true;
            else
                return false;
        }
        #endregion

        private static bool IsCorrectFormat(string number)
        {
            //is have groups
            if (number.Contains(" "))
            {
                //is valid count of groups
                if (number.Count(c => c == ' ') != 3)
                    return false;

                //is valid groups 
                if (number.Split(' ').Where(str => str.Length == 4).Count() != 4)
                {
                    return false;
                }
            }
            //is valid count of numbers
            if (number.Replace(" ", string.Empty).Count() != 16) return false;

            return true;
        }

        public static CreditCardVendor GetCreditCardVendor(string number)
        {
            if (number == null) throw new ArgumentNullException("card number cannot be null");
            if(!IsCorrectFormat(number)) throw new ArgumentException("Invalid format");

            uint bin = GetBin(number);

            if (isAmericanExpress(bin)) return CreditCardVendor.AmericanExpress;
            if (isMaestro(bin)) return CreditCardVendor.Maestro;
            if (isMasterCard(bin)) return CreditCardVendor.MasterCard;
            if (isVisa(bin)) return CreditCardVendor.VISA;
            if (isJcb(bin)) return CreditCardVendor.JCB;

            return CreditCardVendor.Unknow;
        }

        public static bool IsCreditCardNumberValid(string number)
        {
            return true;
        }
    }
}
