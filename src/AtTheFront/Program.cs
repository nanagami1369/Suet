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
            // ヘルプ
            if (args.Length >= 1 && (args[0] == "/?" || args[0] == "-h" || args[0] == "--help"))
            {
                MessageBox.Show(
                    $@"使用法:
    {Path.GetFileName(Environment.GetCommandLineArgs()[0])} -s [Key]
フォーカスのあたっているウィンドウを最前面に固定します
すでにウィンドウが最前面に固定されている場合は解除します

オプション:
    /? -h --help ヘルプ
    -s /s [Key]スタンドアローンモード アプリ自体が常駐して
    [key]に指定したキーが入力された場合に最前面に表示します

例:
    {Path.GetFileName(Environment.GetCommandLineArgs()[0])}                 ...実行
    {Path.GetFileName(Environment.GetCommandLineArgs()[0])} -s Ctrl+Shift+K ...Ctrl+Shift+Kを同時に押すと実行
    {Path.GetFileName(Environment.GetCommandLineArgs()[0])} -s Ctrl+Alt+S   ...Ctrl+Alt+Sを同時に押すと実行
    {Path.GetFileName(Environment.GetCommandLineArgs()[0])} -s Shift+F11    ...Shift+F11を同時に押すと実行
");
                return;
            }

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
                }
                finally
                {
                    var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    HotkeyManager.Current.Remove(appName);
                }
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
