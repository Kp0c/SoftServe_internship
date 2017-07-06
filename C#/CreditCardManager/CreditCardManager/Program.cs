using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CreditCard.GenerateNextCreditCardNumber("4012888888881881"));
        }
    }
}
