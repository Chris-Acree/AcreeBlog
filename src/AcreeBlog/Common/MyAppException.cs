using System;

namespace AcreeBlog.Common
{
    // http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
    public class MyAppException : Exception
    {
        public MyAppException()
        { }

        public MyAppException(string message)
            : base(message)
        { }

        public MyAppException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
