using CreditCardManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CreditCardManager.Tests
{
    [TestClass]
    public class CreditCardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentGetCreditCardVendorTests()
        {
            CreditCard.GetCreditCardVendor(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentIsCreditCardNumberValidTests()
        {
            CreditCard.IsCreditCardNumberValid(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentGenerateNextCreditCardNumberTests()
        {
            CreditCard.GenerateNextCreditCardNumber(null);
        }

        [TestMethod]
        public void FormatTests()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 2222 3333"));
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433111122223333"));
        }

        [TestMethod]
        public void ValidCardsTests()
        {
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("5555555555554444"));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("3530111333300000"));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("4012 8888 8888 1881"));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("4111 1111 1111 1111"));
        }

        [TestMethod]
        public void GenerateNextCreditCardNumber()
        {
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("5555555555554444")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("3530111333300000")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("4012 8888 8888 1881")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("4111 1111 1111 1111")));
        }
    }
}
