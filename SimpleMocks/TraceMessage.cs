using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Server;

namespace Step1Mocks
{
    public class TraceMessage
    {
        private readonly int _severity;
        private readonly string _message;

        public TraceMessage(int severity, string message)
        {
            _severity = severity;
            _message = message;
        }

        public int Severity
        {
            get { return _severity; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
