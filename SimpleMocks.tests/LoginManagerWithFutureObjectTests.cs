using System;

using MyBillingProduct;
using NUnit.Framework;
using Step1Mocks;
using TypeMock.ArrangeActAssert;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class LoginManagerWithFutureObjectTests
    {
        [Test, Isolated]
        public void AIsLoginOK_StaticLoggerThrowsException_CallsStaticWebService()
        {
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            const string exceptionMessage = "fake exception";

            Isolate
                .WhenCalled(() => fakeLogger.Write(""))
                .WillThrow(new LoggerException(exceptionMessage));
            Isolate.Swap.NextInstance<RealLogger>().With(fakeLogger);


            var fakeWebService = Isolate.Fake.Instance<WebService>();
            Isolate.Swap.NextInstance<WebService>().With(fakeWebService);   


            var loginManager = new LoginManagerWithFutureObject();
            loginManager.IsLoginOK("a", "b");

            string expectedResult = string.Format(
                "{0} {1}",
                exceptionMessage,
                Environment.MachineName);

            Isolate.Verify.WasCalledWithArguments(() => fakeWebService.Write("")).Matching(args => (string)args[0] == expectedResult);
        }

    }
}