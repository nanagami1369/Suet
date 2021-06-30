using System;

namespace AtTheFront
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            ToFront();
        }

        public static void ToFront()
        {
            var handle = WindowManager.GetForegroundWindow();
            var isAtTheFront = WindowManager.IsAtTheFrontForWindow(handle);
            if (isAtTheFront)
            {
                WindowManager.UnSetAtTheFront(handle);
            }
            else
            {
                WindowManager.SetAtTheFront(handle);
            }
        }
    }
}
