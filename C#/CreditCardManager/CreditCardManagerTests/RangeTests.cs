using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CreditCardManager.Tests
{
    [TestClass]
    public class RangeTests
    {
        static Range range;
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            range = new Range(1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewRange_FromBiggerThanTo_ThrowsArgumentException()
        {
            new Range(5, 1);
            Assert.Fail();
        }

        [TestMethod]
        public void NewRange_ConstructProperly()
        {
            Assert.AreEqual(1, range.From);
            Assert.AreEqual(5, range.To);
        }

        [TestMethod()]
        public void IsInRange_InRange_ReturnsTrue()
        {
            Assert.IsTrue(range.IsInRange(5));
        }

        [TestMethod()]
        public void IsInRange_NotInRange_ReturnsFalse()
        {
            Assert.IsFalse(range.IsInRange(0));
        }
    }
}