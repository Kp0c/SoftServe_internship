using System;
using System.Windows.Forms;
using DatabaseConnectionAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnectionAdminTests
{
    [TestClass]
    public class ValidatorTests
    {
        private TextBox text;
        private TextBox integer;
        private MockDialogService dialogMock;

        [TestInitialize]
        public void Setup()
        {
            text = new TextBox {Text = @"uesrname"};
            integer = new TextBox {Text = @"123"};

            dialogMock = new MockDialogService();
            Validator.SetDialogService(dialogMock);
        }

        [TestMethod]
        public void TryValidateAndAct_EmptyField()
        {
            text.Text = String.Empty;
            Validator.TryValidateAndAct(new[] {text}, null, null);
            Assert.AreEqual($@"""{text.Name}"" cannot be empty.", dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_SpaceCharacterField()
        {
            text.Text = @"aa bb";
            Validator.TryValidateAndAct(new[] {text}, null, null);
            Assert.AreEqual($@"""{text.Name}"" cannot contain ' or space symbol", dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_ApostropheCharacterField()
        {
            text.Text = @"aa'bb";
            Validator.TryValidateAndAct(new[] {text}, null, null);
            Assert.AreEqual($@"""{text.Name}"" cannot contain ' or space symbol", dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_InvalidMoneyField()
        {
            integer.Text = @"ab";
            Validator.TryValidateAndAct(new[] {text}, new[] {integer}, null);
            Assert.AreEqual($@"Wrong {integer.Name} field value", dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_AllFieldsValid_ReturnSumOfIntegerFields()
        {
            TextBox integer2 = new TextBox {Text = @"10"};
            integer.Text = @"20";

            int sum = 0;
            Validator.TryValidateAndAct(new [] {text}, new [] {integer,integer2}, money => sum = money);
            
            Assert.AreEqual(30, sum);
        }
    }
}
