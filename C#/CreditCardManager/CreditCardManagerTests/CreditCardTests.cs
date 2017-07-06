﻿using CreditCardManager;
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
            CreditCard.GenerateNextCreditCardNumber(null);
            Assert.Fail();
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
            Assert.IsFalse(CreditCard.IsCreditCardNumberValid("3530111333300001"));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid("4012 8888 8888 1881"));
            Assert.IsFalse(CreditCard.IsCreditCardNumberValid("4111 1111 1111 1110"));
        }

        [TestMethod]
        public void GenerateNextCreditCardNumberTests()
        {
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("5555555555554444")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("3530111333300000")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("4012 8888 8888 1881")));
            Assert.IsTrue(CreditCard.IsCreditCardNumberValid(CreditCard.GenerateNextCreditCardNumber("4111 1111 1111 1111")));
        }

        [TestMethod]
        public void GetBinTests()
        {
            PrivateType pt = new PrivateType(typeof(CreditCard));

            Assert.AreEqual(123456u, (uint)pt.InvokeStatic("GetBin", "1234 56"));
            Assert.AreEqual(123456u, (uint)pt.InvokeStatic("GetBin", "123456"));
        }

        [TestMethod]
        public void LuhnAlgorithmConversionTests()
        {
            PrivateType pt = new PrivateType(typeof(CreditCard));

            CollectionAssert.AreEqual(new int[] { 2, 2, 6, 4 }, (int[])pt.InvokeStatic("LuhnAlgorithmConversion", new int[] { 1, 2, 3, 4}));
            CollectionAssert.AreEqual(new int[] { 2, 2, 6, 4, 1 }, (int[])pt.InvokeStatic("LuhnAlgorithmConversion", new int[] { 1, 2, 3, 4, 5 }));
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