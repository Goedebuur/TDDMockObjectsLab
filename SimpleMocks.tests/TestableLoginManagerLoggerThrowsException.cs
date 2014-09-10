using System;

using MyBillingProduct;

namespace SimpleMocks.tests
{
    public class TestableLoginManagerLoggerThrowsException : LoginManagerWithStatics
    {
        #region Member fields

        private readonly string _exceptionMessage;
        public string CallLogText = string.Empty;
        public string CallWebServiceText = string.Empty;

        #endregion

        public TestableLoginManagerLoggerThrowsException(string exceptionMessage)
        {
            _exceptionMessage = exceptionMessage;
        }

        protected override void CallLog(string text)
        {
            throw new LoggerException(_exceptionMessage);
        }

        protected override void CallWebService(LoggerException e, TimeSpan now)
        {
            CallWebServiceText = string.Format("{0} {1} {2}", e.Message, Environment.MachineName, now);
        }
    }
}