using System;
using System.Collections;

namespace MyBillingProduct
{
    public class LoginManager1
    {
        #region Member fields

        private readonly ILogger _logger;
        private readonly Hashtable m_users = new Hashtable();

        #endregion

        public LoginManager1(ILogger logger)
        {
            _logger = logger;
        }

        public void AddUser(string user, string password)
        {
            m_users[user] = password;
        }

        public void ChangePass(string user, string oldPass, string newPassword)
        {
            m_users[user] = newPassword;
        }

        public bool IsLoginOK(string user, string password)
        {
            if (m_users[user] != null
                &&
                m_users[user] == password)
            {
                _logger.Write(String.Format("login ok: user: {0}", user));
                return true;
            }
            return false;
        }
    }
}