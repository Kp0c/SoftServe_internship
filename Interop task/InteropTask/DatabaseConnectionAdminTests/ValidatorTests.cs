using System;
using System.Windows.Forms;
using DatabaseConnectionAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnectionAdminTests
{
    [TestClass]
    public class ValidatorTests
    {
        private TextBox _text;
        private TextBox _integer;
        private MockDialogService _dialogMock;

        [TestInitialize]
        public void Setup()
        {
            _text = new TextBox {Text = @"uesrname"};
            _integer = new TextBox {Text = @"123"};

            _dialogMock = new MockDialogService();
            Validator.SetDialogService(_dialogMock);
        }

        [TestMethod]
        public void TryValidateAndAct_EmptyField()
        {
            _text.Text = String.Empty;
            Validator.TryValidateAndAct(new[] {_text}, null, null);
            Assert.AreEqual($@"""{_text.Name}"" cannot be empty.", _dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_SpaceCharacterField()
        {
            _text.Text = @"aa bb";
            Validator.TryValidateAndAct(new[] {_text}, null, null);
            Assert.AreEqual($@"""{_text.Name}"" cannot contain ' or space symbol", _dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_ApostropheCharacterField()
        {
            _text.Text = @"aa'bb";
            Validator.TryValidateAndAct(new[] {_text}, null, null);
            Assert.AreEqual($@"""{_text.Name}"" cannot contain ' or space symbol", _dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_InvalidMoneyField()
        {
            _integer.Text = @"ab";
            Validator.TryValidateAndAct(new[] {_text}, new[] {_integer}, null);
            Assert.AreEqual($@"Wrong {_integer.Name} field value", _dialogMock.TextInService);
        }

        [TestMethod]
        public void TryValidateAndAct_AllFieldsValid_ReturnSumOfIntegerFields()
        {
            TextBox integer2 = new TextBox {Text = @"10"};
            _integer.Text = @"20";

            int sum = 0;
            Validator.TryValidateAndAct(new [] {_text}, new [] {_integer,integer2}, money => sum = money);
            
            Assert.AreEqual(30, sum);
        }
    }
}
