using System;

using MyBillingProduct;

using NUnit.Framework;

using TypeMock.ArrangeActAssert;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class LoginManagerWithFutureObjectTests
    {
        private static string SomePassword
        {
            get { return "testPassword"; }
        }

        private static string SomeUser
        {
            get { return "testUser"; }
        }

        [Test, Isolated]
        public void AIsLoginOK_StaticLoggerThrowsException_CallsStaticWebService()
        {
            //Arrange
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            const string exceptionMessage = "fake exception";

            Isolate
                .WhenCalled(() => fakeLogger.Write(""))
                .WillThrow(new LoggerException(exceptionMessage));
            Isolate.Swap.NextInstance<RealLogger>()
                .With(fakeLogger);

            var fakeWebService = Isolate.Fake.Instance<WebService>();
            Isolate.Swap.NextInstance<WebService>()
                .With(fakeWebService);

            var loginManager = new LoginManagerWithFutureObject();

            string expectedResult = string.Format(
                "{0} {1}",
                exceptionMessage,
                Environment.MachineName);

            //Act
            loginManager.IsLoginOK("a", "b");

            //Assert
            Isolate.Verify.WasCalledWithArguments(() => fakeWebService.Write(""))
                .Matching(args => (string)args[0] == expectedResult);
        }

        [Test]
        public void ChangePass_CorrectPassword_CallsLogWithChanged()
        {
            //Arrange
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            Isolate.Swap.NextInstance<RealLogger>()
                .With(fakeLogger);

            var loginManager = new LoginManagerWithFutureObject();
            loginManager.AddUser(SomeUser, SomePassword);

            //Act
            loginManager.ChangePass(SomeUser, SomePassword, SomePassword + "ABC");

            //Assert
            Isolate.Verify.WasCalledWithArguments(() => fakeLogger.Write(""))
                .Matching(args => (string)args[0] == "changed");
        }

        [Test]
        public void ChangePass_InCorrectPassword_CallsLogWithNotChanged()
        {
            //Arrange
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            Isolate.Swap.NextInstance<RealLogger>()
                .With(fakeLogger);

            var loginManager = new LoginManagerWithFutureObject();
            loginManager.AddUser(SomeUser, SomePassword);

            //Act
            loginManager.ChangePass(SomeUser, SomePassword + "ABC", SomePassword + "XYZ");

            //Assert
            Isolate.Verify.WasCalledWithArguments(() => fakeLogger.Write(""))
                .Matching(args => (string)args[0] == "not changed");
        }
    }
}