using System;

using Step1Mocks;

namespace MyBillingProduct
{
    public class RealLogger : ILogger
    {
        #region ILogger Members

        public void Write(string text)
        {
            throw new NotImplementedException();
        }

        public void Write(TraceMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}