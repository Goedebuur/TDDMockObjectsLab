using System;

using MyBillingProduct;

using Step1Mocks;

namespace SimpleMocks.tests
{
    internal class FakeLogger : ILogger {

        public void Write(string text)
        {
            Log += text;
        }

        public void Write(TraceMessage message)
        {
            throw new NotImplementedException();
        }

        public string Log { get; set; }
    }
}