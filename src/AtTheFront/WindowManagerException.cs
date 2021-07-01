using System;

namespace AtTheFront
{
    [Serializable]
    public class WindowManagerException : Exception
    {
        public WindowManagerException(string message) : base(message)
        {
        }
    }
}
