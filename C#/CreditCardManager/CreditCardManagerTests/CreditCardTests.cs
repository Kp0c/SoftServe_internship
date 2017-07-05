using CreditCardManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CreditCardManagerTests
{
    [TestClass]
    public class CreditCardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFormatManyNumbersTests()
        {
            CreditCard.GetCreditCardVendor("1234 5678 9101 11213");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFormatNotValidSpacesTests()
        {
            CreditCard.GetCreditCardVendor("1111 2222 33334444");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFormatNotValidGroupsTests()
        {
            CreditCard.GetCreditCardVendor("1111 2222 33334 444");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentTests()
        {
            CreditCard.GetCreditCardVendor(null);
        }

        [TestMethod]
        public void FormatTests()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433111122223333"));
        }

        [TestMethod]
        public void AmericanExpressTests()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3733 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 2222 3333"));
        }

        [TestMethod]
        public void MaestroTests()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5033 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5633 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5733 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5833 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("6933 1111 2222 3333"));
        }

        [TestMethod]
        public void MasterCardTests()
        {
            for (int i = 2221; i < 2720; i++)
            {
                Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor(i.ToString() + " 1111 2222 3333"));
            }

            for (int i = 51; i < 55; i++)
            {
                Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor(i.ToString() + "33 1111 2222 3333"));
            }
        }

        [TestMethod]
        public void VisaTests()
        {
            Assert.AreEqual(CreditCardVendor.VISA, CreditCard.GetCreditCardVendor("4433 1111 2222 3333"));
        }

        [TestMethod]
        public void JcbTests()
        {
            for (int i = 3528; i < 3589; i++)
            {
                Assert.AreEqual(CreditCardVendor.JCB, CreditCard.GetCreditCardVendor(i.ToString() + " 1111 2222 3333"));
            }
        }

        [TestMethod]
        public void UnknowTests()
        {
            Assert.AreEqual(CreditCardVendor.Unknow, CreditCard.GetCreditCardVendor("1111 2222 3333 4444"));
        }
    }
}
