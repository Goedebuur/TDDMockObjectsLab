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
            TestableLoginManagerWithStatics testableLoginManager = new TestableLoginManagerWithStatics();
            testableLoginManager.AddUser(SomeUser, SomePassword);

            //Act
            testableLoginManager.IsLoginOK(SomeUser, SomePassword);

            //Assert
            StringAssert.Contains(string.Format("login ok: user: {0}", SomeUser), testableLoginManager.CallLogText);

        }
    }

    class TestableLoginManagerWithStatics : LoginManagerWithStatics
    {
        public string CallLogText = string.Empty;

        protected override void CallLog(string text)
        {
            CallLogText = text;
        }
    }
}
