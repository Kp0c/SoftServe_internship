using System;

namespace CreditCardManager
{
    public class Range
    {
        public Range(int from, int to)
        {
            if (from > to) throw new ArgumentException(String.Format("Invalid range: {0} - {1}", from, to));
            this.from = from;
            this.to = to;
        }

        public bool IsInRange(int number)
        {
            return number >= from && number <= to;
        }

        public int from { get; }
        public int to { get; }
    }
}
