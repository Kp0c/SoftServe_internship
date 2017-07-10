using System;

namespace CreditCardManager
{
    public class Range
    {
        public Range(int from, int to)
        {
            if (from > to)
            {
                throw new ArgumentException(string.Format("Invalid range: {0} - {1}", from, to));
            }
            this.From = from;
            this.To = to;
        }

        public bool IsInRange(int number)
        {
            return number >= From && number <= To;
        }

        public int From { get; }
        public int To { get; }
    }
}
