using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreditCardManager.Tests
{
    [TestClass()]
    public class CreditCardVendorMethodsTests
    {
        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMinRange1_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3400 0011 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMiddleRange1_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3433 1111 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMaxRange1_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3499 9911 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMinRange2_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3700 0011 22 22333"));
        }
        
        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMiddleRange2_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3733 1111 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_AmericanExpressMaxRange2_ReturnsAmericanExpress()
        {
            Assert.AreEqual(CreditCardVendor.AmericanExpress, CreditCard.GetCreditCardVendor("3799 9911 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMinRange1_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5000 0011 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMiddleRange1_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5055 5511 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMaxRange1_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5099 9911 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMinRange2_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5600 0011 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMiddleRange2_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5755 5511 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMaxRange2_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("5899 9911 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMinRange3_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("6000 0011 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMiddleRange3_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("6555 5511 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MaestroMaxRange3_ReturnsMaestro()
        {
            Assert.AreEqual(CreditCardVendor.Maestro, CreditCard.GetCreditCardVendor("6999 9911 22 22336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMinRange1_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("2221 0011 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMiddleRange1_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("2405 5511 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMaxRange1_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("2720 9911 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMinRange2_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("5100 0011 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMiddleRange2_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("5355 5511 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_MasterCardMaxRange2_ReturnsMasterCard()
        {
            Assert.AreEqual(CreditCardVendor.MasterCard, CreditCard.GetCreditCardVendor("5599 9911 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_VisaMinRange1_ReturnsVisa()
        {
            Assert.AreEqual(CreditCardVendor.VISA, CreditCard.GetCreditCardVendor("4000 0011 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_VisaMiddleRange1_ReturnsVisa()
        {
            Assert.AreEqual(CreditCardVendor.VISA, CreditCard.GetCreditCardVendor("4555 5511 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_VisaMaxRange1_ReturnsVisa()
        {
            Assert.AreEqual(CreditCardVendor.VISA, CreditCard.GetCreditCardVendor("4999 9911 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_JcbMinRange1_ReturnsJcb()
        {
            Assert.AreEqual(CreditCardVendor.JCB, CreditCard.GetCreditCardVendor("3528 0011 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_JcbMiddleRange1_ReturnsJcb()
        {
            Assert.AreEqual(CreditCardVendor.JCB, CreditCard.GetCreditCardVendor("3545 5511 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_JcbMaxRange1_ReturnsJcb()
        {
            Assert.AreEqual(CreditCardVendor.JCB, CreditCard.GetCreditCardVendor("3589 9911 2222 3336"));
        }

        [TestMethod]
        public void GetCreditCardVendor_UnknowVendor_ReturnsUnknow()
        {
            Assert.AreEqual(CreditCardVendor.Unknow, CreditCard.GetCreditCardVendor("1111 2222 3333 4444"));
        }
    }
}