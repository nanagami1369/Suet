using System;
using System.Runtime.InteropServices;
using System.Text;
using CommonLibrary;

namespace AtTheFront
{
    public static class WindowManager
    {
        private const int SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;

        // (x, y), (cx, cy)を無視するようにする.
        private const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;
        private const uint NOTOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW;

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;

        public static bool SetAtTheFront(IntPtr handle)
        {
            return NativeMethods.SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        public static bool UnSetAtTheFront(IntPtr handle)
        {
            return NativeMethods.SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, NOTOPMOST_FLAGS);
        }

        public static IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }

        public static bool IsAtTheFrontForWindow(IntPtr handle)
        {
            const int TopMostFlag = 0x00000008;
            var resultCode = NativeMethods.GetWindowInfo(handle, out var windowInfo);
            if (resultCode == 0)
            {
                var errorCode = Marshal.GetLastWin32Error();
                var errorMessage = CommonUtil.GetErrorMessage(errorCode);
                throw new WindowManagerException(errorMessage);
            }

            return 0 != (windowInfo.dwExStyle & TopMostFlag);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWINFO
        {
            public readonly uint cbSize;
            public readonly RECT rcWindow;
            public readonly RECT rcClient;
            public readonly uint dwStyle;
            public readonly uint dwExStyle;
            public readonly uint dwWindowStatus;
            public readonly uint cxWindowBorders;
            public readonly uint cyWindowBorders;
            public readonly ushort atomWindowType;
            public readonly ushort wCreatorVersion;
        }

        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy,
                uint flags);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowInfo(IntPtr hwnd, out WINDOWINFO pwi);
        }
    }
}
