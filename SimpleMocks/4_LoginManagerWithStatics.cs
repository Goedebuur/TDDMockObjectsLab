using System;
using System.Collections;
using Step1Mocks;

namespace MyBillingProduct
{
	public class LoginManagerWithStatics
	{
		private Hashtable m_users = new Hashtable();

		public bool IsLoginOK(string user, string password)
		{
			try
			{
			   CallLog("blah");
			}
			catch (LoggerException e)
			{
				StaticWebService.Write(e.Message + Environment.MachineName);
			}
			if (m_users[user] != null &&
				(string) m_users[user] == password)
			{
			    CallLog(
			        string.Format("login ok: user: {0}", user));
				return true;
			}

            CallLog("failed");
			return false;
		}

		protected virtual void CallLog(string text)
		{
			StaticLogger.Write(text);
		}


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

			m_users[user]= newPassword;
            CallLog("changed");
		}
	}
}
