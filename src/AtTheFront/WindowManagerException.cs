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

        protected WindowManagerException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
