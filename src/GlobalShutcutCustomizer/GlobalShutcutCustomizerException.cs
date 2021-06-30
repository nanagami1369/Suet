using System;

namespace GlobalShutcutCustomizer
{
    internal class SettingValidationException : Exception
    {
        public SettingValidationException(string message) : base(message)
        {
        }

        public SettingValidationException()
        {
        }

        public SettingValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
