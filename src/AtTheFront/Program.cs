using System;
using System.IO;
using System.Windows.Forms;
using CommonLibrary;

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
    {AppBody.AppName} [option] [key]
フォーカスのあたっているウィンドウを最前面に固定します
すでにウィンドウが最前面に固定されている場合は解除します

オプション:
    /? -h --help ヘルプ
    -s /s [Key]スタンドアローンモード アプリ自体が常駐して
    [key]に指定したキーが入力された場合に最前面に表示します

例:
    {AppBody.AppName}                 ...実行
    {AppBody.AppName} -s Ctrl+Shift+K ...Ctrl+Shift+Kを同時に押すと実行
    {AppBody.AppName} -s Ctrl+Alt+S   ...Ctrl+Alt+Sを同時に押すと実行
    {AppBody.AppName} -s Shift+F11    ...Shift+F11を同時に押すと実行
");
                return;
            }

            // スタンドアローンモード
            if (args.Length == 2 && (args[0] == "-s" || args[0] == "/s"))
            {
                    var app = new AppBody(args[1], ToFront);
                    Application.Run();
            }
            else
            {
                ToFront();
            }
        }

        public static void ToFront()
        {
            try
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
            catch (WindowManagerException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
