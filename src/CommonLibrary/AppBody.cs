using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NHotkey;
using NHotkey.WindowsForms;

namespace CommonLibrary
{
    public class AppBody : Form
    {
        public AppBody(string option, Action execMethod)
        {
            ShowInTaskbar = false;
            SetComponents();
            try
            {
                var keys = SettingUtil.StringToKeys(option);
                HotkeyManager.Current.AddOrReplace(AppName, keys, (s, e) => execMethod());
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(-1);
            }
            catch (HotkeyAlreadyRegisteredException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(-1);
            }

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
                }

                var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                HotkeyManager.Current.Remove(appName);
            };
        }

        public static string AppName { get; } = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetComponents()
        {
            var icon = new NotifyIcon {Icon = GetAppIcon(), Visible = true, Text = AppName};
            var menu = new ContextMenuStrip();
            var menuItem = new ToolStripMenuItem {Text = "&終了"};
            menuItem.Click += Close_Click;
            menu.Items.Add(menuItem);
            icon.ContextMenuStrip = menu;
        }

        private static Icon GetAppIcon()
        {
            var appDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var appName = AppName;
            var appPath = Path.Combine(appDirPath, appName);
            return Icon.ExtractAssociatedIcon(appPath);
        }
    }
}
