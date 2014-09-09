using System;

using Moq;

using MyBillingProduct;

using NUnit.Framework;

using Step1Mocks;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class LoginManager2Tests
    {
        private static string SomePassword
        {
            get { return "testPassword"; }
        }

        private static string SomeUser
        {
            get { return "testUser"; }
        }

        private LoginManager2 CreateLoginManager2(ILogger logger, IWebService service)
        {
            return new LoginManager2(logger, service);
        }

        [Test]
        public void IsLoginOK_ExistingUserNameExistingPassword_LogContainsOk()
        {
            //Arrange
            Mock<ILogger> loggerMock = new Mock<ILogger>();
            Mock<IWebService> serviceMock = new Mock<IWebService>();

            LoginManager2 loginManager2 = CreateLoginManager2(loggerMock.Object, serviceMock.Object);
            loginManager2.AddUser(SomeUser, SomePassword);

            //Act
            loginManager2.IsLoginOK(SomeUser, SomePassword);

            //Assert
            loggerMock.Verify(logger => logger.Write(It.Is<TraceMessage>(
                            message =>
                                message.Severity == 1000
                                    && message.Message == string.Format("login ok: user: {0}", SomeUser))));
        }

        [Test]
        public void IsLoginOK_LoggerThrowsException_CallsWebserviceWrite()
        {
            //Arrange
            Mock<ILogger> loggerMock = new Mock<ILogger>();
            Mock<IWebService> serviceMock = new Mock<IWebService>();

            loggerMock.Setup(x => x.Write(It.IsAny<TraceMessage>()))
                .Throws<NotImplementedException>();

            LoginManager2 loginManager2 = CreateLoginManager2(loggerMock.Object, serviceMock.Object);
            loginManager2.AddUser(SomeUser, SomePassword);

            //Act
            loginManager2.IsLoginOK(SomeUser, SomePassword);

            //Assert
            serviceMock.Verify(service => service.Write("logger failed"));
        }
    }
}