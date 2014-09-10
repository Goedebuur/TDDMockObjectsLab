using System;
using System.Collections;

namespace MyBillingProduct
{
    public class LoginManagerWithFutureObject
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
                new RealLogger().Write("not changed");
                return;
            }

            m_users[user] = newPassword;
            new RealLogger().Write("changed");
        }

        public bool IsLoginOK(string user, string password)
        {
            try
            {
                new RealLogger().Write("blah");
            }
            catch (LoggerException e)
            {
                new WebService().Write(string.Format("{0} {1}", e.Message, Environment.MachineName));
            }
            if (m_users[user] != null
                &&
                m_users[user] == password)
            {
                return true;
            }
            return false;
        }
    }
}