using System;
using System.Runtime.InteropServices;

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
        private static readonly int HWND_TOPMOST = -1;
        private static readonly int HWND_NOTOPMOST = -2;

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
            _ = NativeMethods.GetWindowInfo(handle, out var windowInfo);
            return 0 != (windowInfo.dwExStyle & TopMostFlag);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
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
