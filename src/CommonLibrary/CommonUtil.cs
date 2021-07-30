using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary
{
    public static class CommonUtil
    {
        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        public static string GetErrorMessage(int errorCode)
        {
            var message = new StringBuilder(255);
            _ = NativeMethods.FormatMessage(
                FORMAT_MESSAGE_FROM_SYSTEM,
                IntPtr.Zero,
                (uint)errorCode,
                0,
                message,
                message.Capacity,
                IntPtr.Zero);
            return message.ToString();
        }

        private static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId,
                StringBuilder lpBuffer, int nSize, IntPtr Arguments);
        }
    }
}
