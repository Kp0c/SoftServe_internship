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
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentIsCreditCardNumberValidTests()
        {
            CreditCard.IsCreditCardNumberValid(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentGenerateNextCreditCardNumberTests()
        {
            CreditCard.GenerateNextRandomCreditCardNumber(null);
            Assert.Fail();
        }

        [TestMethod]
        public void FormatTests()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 2222 333"));
            Assert.AreEqual(CreditCardVendor.VISA, CreditCard.GetCreditCardVendor("4567123478693"));

            Assert.AreEqual(CreditCardVendor.Unknow, CreditCard.GetCreditCardVendor("35301113333000001"));
        }

        [TestMethod]
        public void ValidCardsTests()
        {
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("5555555555554444"));
            Assert.IsFalse(CreditCard.IsCreditCardNumberValid("3530111333300001"));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("4012 8888 8888 1881"));
            Assert.IsFalse(CreditCard.IsCreditCardNumberValid("4111 1111 1111 1110"));
        }

        [TestMethod]
        public void GenerateNextRandomCreditCardNumberTests()
        {
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextRandomCreditCardNumber("5555555555554444")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextRandomCreditCardNumber("3530111333300000")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextRandomCreditCardNumber("4012 8888 8888 1881")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextRandomCreditCardNumber("4111 1111 1111 1111")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateNextCreditCardNumberCannotGenerateTest()
        {
            CreditCard.GenerateNextCreditCardNumber("4999999999999999993");
            Assert.Fail();
        }

        [TestMethod]
        public void GenerateNextCreditCardNumberTest()
        {
           Assert.AreEqual("4999999999999999993", CreditCard.GenerateNextCreditCardNumber("4999999999999999985"));
        }

        [TestMethod]
        public void GenerateNextCreditCardNumberGotoAnotherRangeOfBinsTest()
        {
            Assert.AreEqual("5100000000000008", CreditCard.GenerateNextCreditCardNumber("2720999999999996"));
        }

        [TestMethod]
        public void GenerateNextCreditCardNumberGotoAnotherRangeOfLengthsTest()
        {
            Assert.AreEqual("5000000000005", CreditCard.GenerateNextCreditCardNumber("6999 9999 9997"));
            Assert.AreEqual("4000000000000000006", CreditCard.GenerateNextCreditCardNumber("4999 9999 9999 9996"));
        }

        [TestMethod]
        public void GetBinTests()
        {
            PrivateType pt = new PrivateType(typeof(CreditCard));

            Assert.AreEqual(123456, pt.InvokeStatic("GetBin", "1234 56"));
            Assert.AreEqual(123456, pt.InvokeStatic("GetBin", "123456"));
        }

        [TestMethod]
        public void LuhnAlgorithmConversionTests()
        {
            PrivateType pt = new PrivateType(typeof(CreditCard));

            CollectionAssert.AreEqual(new int[] { 2, 2, 6, 4 }, (int[])pt.InvokeStatic("LuhnAlgorithmConversion", new int[] { 1, 2, 3, 4}));
            CollectionAssert.AreEqual(new int[] { 1, 4, 3, 8, 5 }, (int[])pt.InvokeStatic("LuhnAlgorithmConversion", new int[] { 1, 2, 3, 4, 5 }));
        }

        [TestMethod]
        public void ConvertStringToIntArrayTests()
        {
            PrivateType pt = new PrivateType(typeof(CreditCard));

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6 }, (int[])pt.InvokeStatic("ConvertStringToIntArray", "1234 56"));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6 }, (int[])pt.InvokeStatic("ConvertStringToIntArray", "123456"));
        }
    }
}
