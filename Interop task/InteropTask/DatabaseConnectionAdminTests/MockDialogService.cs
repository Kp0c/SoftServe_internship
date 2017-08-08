using DatabaseConnectionAdmin;

namespace DatabaseConnectionAdminTests
{
    class MockDialogService : IDialogService
    {
        string _textInService;

        public string TextInService
        {
            get { return _textInService; }
        }

        public void ShowMessageBox(string text)
        {
            _textInService = text;
        }
    }
}
