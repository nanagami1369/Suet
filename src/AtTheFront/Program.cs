using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;

namespace AtTheFront
{
    internal class Program
    {
        [STAThread]
        private static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option(new[] {"--help", "-h", "-?", "/?"}), new Option<string>(new[] {"--standalone", "-s"})
            };
            rootCommand.Handler = CommandHandler.Create<bool, string>((help, standalone) =>
            {
                if (!string.IsNullOrWhiteSpace(standalone))
                {
                    var app = new AppBody(standalone, ToFront);
                    Application.Run();
                    return;
                }

                if (help)
                {
                    MessageBox.Show(
                        $@"使用法:
    {AppBody.AppName} [option]
フォーカスのあたっているウィンドウを最前面に固定します
すでにウィンドウが最前面に固定されている場合は解除します

オプション:
    /? -? -h --help   ヘルプ
    -s --standalone <Key> スタンドアローンモード アプリ自体が常駐して
    <Key>に指定したキーが入力された場合に最前面に表示します

例:
    {AppBody.AppName}                 ...実行
    {AppBody.AppName} -s Ctrl+Shift+K ...Ctrl+Shift+Kを同時に押すと実行
    {AppBody.AppName} -s Ctrl+Alt+S   ...Ctrl+Alt+Sを同時に押すと実行
    {AppBody.AppName} -s Shift+F11    ...Shift+F11を同時に押すと実行
");
                    return;
                }

                ToFront();
            });
            return await rootCommand.InvokeAsync(args);
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
