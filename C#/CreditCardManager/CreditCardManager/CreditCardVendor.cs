﻿using System.Collections.Generic;
using System.Linq;

namespace CreditCardManager
{
    public enum CreditCardVendor
    {
        AmericanExpress = 1,
        Maestro,
        MasterCard,
        VISA,
        JCB,
        Unknow = 0, //set unknow as default
    }

    public class CreditCardVendorMethods
    {
        public static Dictionary<CreditCardVendor, Range[]> bins = new Dictionary<CreditCardVendor, Range[]>
        {
             [CreditCardVendor.AmericanExpress] = new Range[]{ new Range(340000,349999), new Range(370000,379999) },
             [CreditCardVendor.Maestro] = new Range[] {new Range(500000,509999), new Range(560000,589999), new Range(600000,699999)},
             [CreditCardVendor.MasterCard] = new Range[] {new Range(222100,272099), new Range(510000,559999)},
             [CreditCardVendor.VISA] = new Range[] {new Range(400000,499999)},
             [CreditCardVendor.JCB] = new Range[] {new Range(352800,358999)},
             //don't add unknow, because it's can intersects with current or future bins ranges
        };

        public static Dictionary<CreditCardVendor, Range[]> lengths = new Dictionary<CreditCardVendor, Range[]>
        {
            [CreditCardVendor.AmericanExpress] = new Range[] { new Range(15, 15) },
            [CreditCardVendor.Maestro] = new Range[] { new Range(12, 19) },
            [CreditCardVendor.MasterCard] = new Range[] { new Range(16, 16) },
            [CreditCardVendor.VISA] = new Range[] { new Range(13, 13), new Range(16, 16), new Range(19, 19) },
            [CreditCardVendor.JCB] = new Range[] { new Range(16, 16) },
            [CreditCardVendor.Unknow] = new Range[] { new Range(12, 19) }, //maximum possible range
        };

        public static bool CheckFormat(CreditCardVendor ccv, string creditCardNumber)
        {
            int numberCount = creditCardNumber.Replace(" ", string.Empty).Count();

            return lengths[ccv].Any(rangeArray => rangeArray.IsInRange(numberCount));
        }

        public static CreditCardVendor GetCreditCardVendorFromBin(int bin)
        {
             return bins.FirstOrDefault(pair =>
            {
                bool isInRange = false;
                foreach (Range range in pair.Value)
                {
                    if (range.IsInRange(bin)) isInRange = true;
                }
                return isInRange;
            }).Key;
        }
    }
}
