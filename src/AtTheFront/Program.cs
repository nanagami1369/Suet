using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NHotkey;
using NHotkey.WindowsForms;

namespace AtTheFront
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            // スタンドアローンモード
            if (args.Length == 2 && (args[0] == "-s" || args[0] == "/s"))
            {
                try
                {
                    var app = new AppBody(args[1], ToFront);
                    Application.ThreadException += (sender, e) =>
                    {
                        MessageBox.Show(e.Exception.Message);
                        var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                        HotkeyManager.Current.Remove(appName);
                    };
                    Thread.GetDomain().UnhandledException += (sender, e) =>
                    {
                        if (e.ExceptionObject is Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                            HotkeyManager.Current.Remove(appName);
                        }
                    };
                    Application.Run();
                }
                catch (HotkeyAlreadyRegisteredException e)
                {
                    MessageBox.Show(e.Message);
                    var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    HotkeyManager.Current.Remove(appName);
                    return;
                }
                finally
                {
                    var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    HotkeyManager.Current.Remove(appName);
                }
                return;
            }
            else
            {
                ToFront();
            }
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
