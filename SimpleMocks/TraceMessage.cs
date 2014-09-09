using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Server;

namespace Step1Mocks
{
    public class TraceMessage
    {
        private int _severity;
        private string _message;

        public TraceMessage(int severity, string message)
        {
            _severity = severity;
            _message = message;
        }
    }
}
