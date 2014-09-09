using MyBillingProduct;

using NUnit.Framework;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class LoginManager1Tests
    {
        private static FakeLogger CreateFakeLogger()
        {
            return new FakeLogger();
        }

        private static string SomePassword
        {
            get { return "testPassword"; }
        }

        private static string SomeUser
        {
            get { return "testUser"; }
        }

        private static LoginManager1 CreateLoginManager1(FakeLogger fakeLogger)
        {
            return new LoginManager1(fakeLogger);
        }

        [Test]
        public void IsLoginOK_ExistingUserNameExistingPassword_LogContainsOk()
        {
            //Arrange
            FakeLogger fakeLogger = CreateFakeLogger();
            LoginManager1 loginManager1 = CreateLoginManager1(fakeLogger);
            loginManager1.AddUser(SomeUser, SomePassword);

            //Act
            loginManager1.IsLoginOK(SomeUser, SomePassword);

            //Assert
            StringAssert.Contains(string.Format("login ok: user: {0}", SomeUser), fakeLogger.Log);
        }
    }
}