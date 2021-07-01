using System;

namespace AtTheFront
{
    [Serializable]
    public class WindowManagerException : Exception
    {
        public WindowManagerException(string message) : base(message)
        {
        }

        public WindowManagerException()
        {
        }

        public WindowManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
