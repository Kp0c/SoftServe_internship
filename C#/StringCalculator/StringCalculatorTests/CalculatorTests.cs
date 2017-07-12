using System;
using NUnit.Framework;

namespace StringCalculator.Tests
{

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_EmptyString_Returns0()
        {
            Assert.AreEqual(0, Calculator.Add(String.Empty));
        }

        [Test]
        public void Add_OneArgument_ReturnsThisArgument()
        {
            Assert.AreEqual(1, Calculator.Add("1"));
        }

        [Test]
        public void Add_TwoArguments_ReturnsSum()
        {
            Assert.AreEqual(10, Calculator.Add("5,5"));
        }

        [Test]
        public void Add_FiveArguments_ReturnsSum()
        {
            Assert.AreEqual(25, Calculator.Add("5,5,5,5,5"));
        }

        [Test]
        public void Add_NewLineInArguments_ReturnsSum()
        {
            Assert.AreEqual(15, Calculator.Add("5\n5,5"));
        }

        [Test]
        public void Add_SupportDifferentDelimiters_ReturnsSum()
        {
            Assert.AreEqual(500, Calculator.Add("//[;]\n50;450"));
        }

        [Test]
        public void Add_SupportDelimitersBiggerThan1Char_ReturnsSum()
        {
            Assert.AreEqual(12, Calculator.Add("//[***]\n5***5***2"));
        }

        [Test]
        public void Add_AllowMultipleDelimiters_ReturnsSum()
        {
            Assert.AreEqual(15, Calculator.Add("//[***][%%][$]\n5***5%%2$3"));
        }

        [Test]
        public void Add_NegativeNumber_ThrowArgumentException()
        {
            Assert.That(() => { Calculator.Add("3, -5, -8"); }, Throws.TypeOf<ArgumentException>().And.Message.EqualTo("Negatives not allowed: -5, -8"));
        }

        [Test]
        public void Add_NumbersBiggerThan1000_IgnoreNumbersBiggerThan1000()
        {
            Assert.AreEqual(2, Calculator.Add("2,1001"));
        }
    }
}