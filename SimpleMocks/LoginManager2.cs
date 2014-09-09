using System;
using System.Collections;

namespace MyBillingProduct
{
    public class LoginManager2
    {
        #region Member fields

        private readonly ILogger log;
        private readonly Hashtable m_users = new Hashtable();
        private readonly IWebService service;

        #endregion

        public LoginManager2(ILogger logger, IWebService service)
        {
            this.service = service;
            log = logger;
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
                try
                {
                    log.Write(string.Format("login ok: user: {0}", user));
                }
                catch (Exception)
                {
                    service.Write("logger failed");
                }

                return true;
            }
            return false;
        }
    }
}