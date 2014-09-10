using System;
using System.Collections;

using Step1Mocks;

namespace MyBillingProduct
{
    public class LoginManagerWithStatics
    {
        #region Member fields

        private readonly Hashtable m_users = new Hashtable();

        #endregion

        public void AddUser(string user, string password)
        {
            m_users[user] = password;
        }

        public void ChangePass(string user, string oldPass, string newPassword)
        {
            if (m_users[user] != oldPass)
            {
                CallLog("not changed");
                return;
            }

            m_users[user] = newPassword;
            CallLog("changed");
        }

        public bool IsLoginOK(string user, string password)
        {
            try
            {
                CallLog("blah");

                if (m_users[user] != null
                    &&
                    (string)m_users[user] == password)
                {
                    CallLog(
                        string.Format("login ok: user: {0}", user));
                    return true;
                }

                CallLog("failed");
            }
            catch (LoggerException e)
            {
                CallWebService(e, DateTime.Now.TimeOfDay);
            }

            return false;
        }

        protected virtual void CallLog(string text)
        {
            StaticLogger.Write(text);
        }

        protected virtual void CallWebService(LoggerException e, TimeSpan now)
        {
            StaticWebService.Write(string.Format("{0} {1} {2}", e.Message, Environment.MachineName, now));
        }
    }
}