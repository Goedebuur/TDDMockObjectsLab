using System;
using System.Collections;

using Step1Mocks;

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
            if (m_users[user] != oldPass)
            {
                throw new ArgumentException();
            }

            m_users[user] = newPassword;
            log.Write(string.Format("pass changed: [{0}],[{1}],[{2}]", user, oldPass, newPassword));
        }

        public bool IsLoginOK(string user, string password)
        {
            if (m_users[user] != null
                &&
                m_users[user] == password)
            {
                try
                {
                    log.Write(new TraceMessage(1000, string.Format("login ok: user: {0}", user)));
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