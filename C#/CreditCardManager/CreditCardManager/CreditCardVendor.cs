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

    public class CreditCardVendorMethods
    {
        public static bool isMaestro(uint bin)
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

        public static bool isMasterCard(uint bin)
        {
            uint firstFourNumbers = bin / 100;
            uint firstTwoNumbers = firstFourNumbers / 100;
            if ((firstFourNumbers >= 2221 && firstFourNumbers <= 2720)
                || (firstTwoNumbers >= 51 && firstTwoNumbers <= 55))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isAmericanExpress(uint bin)
        {
            uint firstTwoNumbers = bin / 10000;
            if (firstTwoNumbers == 34 || firstTwoNumbers == 37)
                return true;
            else
                return false;
        }

        public static bool isVisa(uint bin)
        {
            if (bin / 100000 == 4)
                return true;
            else
                return false;
        }

        public static bool isJcb(uint bin)
        {
            uint firstFourNumbers = bin / 100;
            if (firstFourNumbers >= 3528 && firstFourNumbers <= 3589)
                return true;
            else
                return false;
        }
    }
}
