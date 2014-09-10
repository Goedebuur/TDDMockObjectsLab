using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyBillingProduct;

using NUnit.Framework;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class LoginManagerWithStaticsTests
    {
        private static string SomePassword
        {
            get { return "testPassword"; }
        }

        private static string SomeUser
        {
            get { return "testUser"; }
        }

        [Test]
        public void IsLoginOK_KnownUserKnownPassword_LogsOK()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);

            //Act
            testableLoginManager.IsLoginOK(SomeUser, SomePassword);

            //Assert
            StringAssert.Contains(string.Format("login ok: user: {0}", SomeUser), testableLoginManager.CallLogText);

        }

        [Test]
        public void ChangePass_CorrectOldPasswordSomeNewPassword_LogsChanged()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);

            //Act
            testableLoginManager.ChangePass(SomeUser, SomePassword, "ABC");

            //Assert
            StringAssert.Contains("changed", testableLoginManager.CallLogText);

        }

        [Test]
        public void ChangePass_InCorrectOldPasswordSomeNewPassword_LogsNotChanged()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);
            string incorrectPassword = SomePassword + "ABC";

            //Act
            testableLoginManager.ChangePass(SomeUser, incorrectPassword, "ABC");

            //Assert
            StringAssert.Contains("not changed", testableLoginManager.CallLogText);

        }

        [Test]
        public void IsLoginOK_UnknownUserKnownPassword_LogsFailed()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);
            
            string unknownUser = SomeUser + "ABC";

            //Act
            testableLoginManager.IsLoginOK(unknownUser, SomePassword);

            //Assert
            StringAssert.Contains("failed", testableLoginManager.CallLogText);

        }

        [Test]
        public void IsLoginOK_KnownUserUnknownPassword_LogsFailed()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);
            
            string unknownPassword = SomePassword + "ABC";

            //Act
            testableLoginManager.IsLoginOK(SomeUser, unknownPassword);

            //Assert
            StringAssert.Contains("failed", testableLoginManager.CallLogText);

        }

        [Test]
        public void IsLoginOK_UnknownUserUnknownPassword_LogsFailed()
        {
            //Arrange
            TestableLoginManager testableLoginManager = new TestableLoginManager();
            testableLoginManager.AddUser(SomeUser, SomePassword);
           
            string unknownUser = SomeUser + "ABC";
            string unknownPassword = SomePassword + "ABC";

            //Act
            testableLoginManager.IsLoginOK(unknownUser, unknownPassword);

            //Assert
            StringAssert.Contains("failed", testableLoginManager.CallLogText);

        }

        [Test]
        public void IsLoginOK_LoggerThrowsException_CallsWebservice()
        {
            //Arrange
            const string exceptionMessage = "Fake Exception";
            TestableLoginManagerLoggerThrowsException testableLoginManager = new TestableLoginManagerLoggerThrowsException(exceptionMessage);
            testableLoginManager.AddUser(SomeUser, SomePassword);

            //Act
            testableLoginManager.IsLoginOK(SomeUser, SomePassword);


            //Assert
            StringAssert.Contains(string.Format("{0} {1}", exceptionMessage, Environment.MachineName), testableLoginManager.CallWebServiceText);
        }

    }

    class TestableLoginManager : LoginManagerWithStatics
    {
        public string CallLogText = string.Empty;
        public string CallWebServiceText = string.Empty;

        protected override void CallLog(string text)
        {
            CallLogText = text;
        }
    }

    class TestableLoginManagerLoggerThrowsException : LoginManagerWithStatics
    {
        public string CallLogText = string.Empty;
        public string CallWebServiceText = string.Empty;
        private readonly string _exceptionMessage;

        public TestableLoginManagerLoggerThrowsException(string exceptionMessage)
        {
            _exceptionMessage = exceptionMessage;
        }

        protected override void CallLog(string text)
        {
            throw new LoggerException(_exceptionMessage);
        }

        protected override void CallWebService(LoggerException e)
        {
            CallWebServiceText = string.Format("{0} {1}", e.Message, Environment.MachineName);
        }
    }
}
