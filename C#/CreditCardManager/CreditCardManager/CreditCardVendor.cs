using System.Collections.Generic;
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
        public static readonly Dictionary<CreditCardVendor, Range[]> bins = new Dictionary<CreditCardVendor, Range[]>
        {
            [CreditCardVendor.AmericanExpress] = new[] { new Range(340000, 349999), new Range(370000, 379999) },
            [CreditCardVendor.Maestro] = new[] { new Range(500000, 509999), new Range(560000, 589999), new Range(600000, 699999) },
            [CreditCardVendor.MasterCard] = new[] { new Range(222100, 272099), new Range(510000, 559999) },
            [CreditCardVendor.VISA] = new[] { new Range(400000, 499999) },
            [CreditCardVendor.JCB] = new[] { new Range(352800, 358999) },
            //don't add unknow, because it's can intersects with current or future bins ranges
        };

        public static readonly Dictionary<CreditCardVendor, Range[]> lengths = new Dictionary<CreditCardVendor, Range[]>
        {
            [CreditCardVendor.AmericanExpress] = new[] { new Range(15, 15) },
            [CreditCardVendor.Maestro] = new[] { new Range(12, 19) },
            [CreditCardVendor.MasterCard] = new[] { new Range(16, 16) },
            [CreditCardVendor.VISA] = new[] { new Range(13, 13), new Range(16, 16), new Range(19, 19) },
            [CreditCardVendor.JCB] = new[] { new Range(16, 16) },
        };

        public static bool CheckFormat(CreditCardVendor ccv, string creditCardNumber)
        {
            if (ccv == CreditCardVendor.Unknow)
            {
                return false;
            }
            else
            {
                int numberCount = creditCardNumber.Count();
                return lengths[ccv].Any(rangeArray => rangeArray.IsInRange(numberCount));
            }
        }

        public static CreditCardVendor GetCreditCardVendorFromBin(int bin)
        {
             return bins.FirstOrDefault(pair =>
            {
                foreach (Range range in pair.Value)
                {
                    if (range.IsInRange(bin))
                    {
                        return true;
                    }
                }
                return false;
            }).Key;
        }
    }
}
