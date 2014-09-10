using MyBillingProduct;

namespace SimpleMocks.tests
{
    public class TestableLoginManager : LoginManagerWithStatics
    {
        #region Member fields

        public string CallLogText = string.Empty;
        public string CallWebServiceText = string.Empty;

        #endregion

        protected override void CallLog(string text)
        {
            CallLogText = text;
        }
    }
}