using Moq;

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

        private static LoginManager1 CreateLoginManager1(ILogger fakeLogger)
        {
            return new LoginManager1(fakeLogger);
        }

        [Test]
        public void AddUser_SomeUserSomePassword_LogContainsUserAdded()
        {
            //Arrange
            FakeLogger fakeLogger = CreateFakeLogger();
            LoginManager1 loginManager1 = CreateLoginManager1(fakeLogger);

            //Act
            loginManager1.AddUser(SomeUser, SomePassword);

            //Assert
            StringAssert.Contains(string.Format("user added: [{0}],[{1}]", SomeUser, SomePassword), fakeLogger.Log);
        }

        [Test]
        public void ChangePass_SomePasswordToSomethingElse_LogContainsPassChanged()
        {
            //Arrange
            Mock<ILogger> loggerMock = new Mock<ILogger>();
            LoginManager1 loginManager1 = CreateLoginManager1(loggerMock.Object);
            loginManager1.AddUser(SomeUser, SomePassword);
            const string newPassword = "B";

            //Act
            loginManager1.ChangePass(SomeUser, SomePassword, newPassword);

            //Assert
            loggerMock.Verify(
                logger =>
                    logger.Write(string.Format("pass changed: [{0}],[{1}],[{2}]", SomeUser, SomePassword, newPassword)));
        }

        [Test]
        public void IsLoginOK_ExistingUserNameEmptyPassword_LogContainsFail()
        {
            //Arrange
            FakeLogger fakeLogger = CreateFakeLogger();
            LoginManager1 loginManager1 = CreateLoginManager1(fakeLogger);
            loginManager1.AddUser(SomeUser, SomePassword);
            const string unknownPassword = "A";

            //Act
            loginManager1.IsLoginOK(SomeUser, unknownPassword);

            //Assert
            StringAssert.Contains(string.Format("bad login: [{0}],[{1}]", SomeUser, unknownPassword), fakeLogger.Log);
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

        [Test]
        public void IsLoginOK_UnknownUserNameExistingPassword_LogContainsFail()
        {
            //Arrange
            FakeLogger fakeLogger = CreateFakeLogger();
            LoginManager1 loginManager1 = CreateLoginManager1(fakeLogger);
            loginManager1.AddUser(SomeUser, SomePassword);
            const string unknownUser = "A";

            //Act
            loginManager1.IsLoginOK(unknownUser, SomePassword);

            //Assert
            StringAssert.Contains(string.Format("bad login: [{0}],[{1}]", unknownUser, SomePassword), fakeLogger.Log);
        }
    }
}