using MyBillingProduct;

namespace SimpleMocks.tests
{
    internal class FakeLogger : ILogger {

        public void Write(string text)
        {
            Log += text;
        }

        public string Log { get; set; }
    }
}