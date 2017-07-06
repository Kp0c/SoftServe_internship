using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CreditCardManager.Tests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RangeExceptionTest()
        {
            new Range(5, 1);
            Assert.Fail();
        }

        [TestMethod]
        public void RangeTest()
        {
            Range range = new Range(1, 5);
            Assert.AreEqual(1, range.from);
            Assert.AreEqual(5, range.to);
        }

        [TestMethod()]
        public void IsInRangeTest()
        {
            Range range = new Range(1, 10);
            Assert.IsTrue(range.IsInRange(10));
            Assert.IsFalse(range.IsInRange(11));
        }
    }
}