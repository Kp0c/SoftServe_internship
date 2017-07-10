using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CreditCardManager.Tests
{
    [TestClass()]
    public class CreditCardVendorMethodsTests
    {
        [TestMethod]
        public void AmericanExpressTests()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3733 1111 22 22336"));
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 22 22333"));
        }

        [TestMethod]
        public void MaestroTests()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5033 1111 2222 3337"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5633 1111 2222 3331"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5733 1111 2222 3330"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5833 1111 2222 3339"));
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("6933 1111 2222 3336"));
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

        [TestMethod]
        public void InvalidLengthTest()
        {
            Assert.IsFalse(CreditCardVendorMethods.CheckFormat(CreditCardVendor.Unknow, "6556 3145"));
        }
    }
}