using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalShutcutCustomizer
{
    class SettingValidationException : Exception
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
