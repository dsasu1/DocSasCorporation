using System;
using System.Collections.Generic;
using System.Text;

namespace DSCAppEssentials.ErrorLogging
{
    public class DSCClientException : Exception
    {
        public DSCClientException(string message, Exception exception) : base(message,exception)
        {

        }

        public DSCClientException(string message) : base(message)
        {

        }
    }
}
