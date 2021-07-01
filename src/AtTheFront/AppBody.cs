using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NHotkey.WindowsForms;

namespace AtTheFront
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
                HotkeyManager.Current.AddOrReplace(GetAppName(), keys, (s, e) => execMethod());
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(-1);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetComponents()
        {
            var icon = new NotifyIcon
            {
                Icon = GetAppIcon(),
                Visible = true,
                Text = GetAppName()
            };
            var menu = new ContextMenuStrip();
            var menuItem = new ToolStripMenuItem
            {
                Text = "&終了"
            };
            menuItem.Click += Close_Click;
            menu.Items.Add(menuItem);
            icon.ContextMenuStrip = menu;
        }

        private Icon GetAppIcon()
        {
            var appDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var appName = GetAppName();
            var appPath = Path.Combine(appDirPath, appName);
            return Icon.ExtractAssociatedIcon(appPath);
        }

        private string GetAppName()
        {
            return Path.GetFileName(Environment.GetCommandLineArgs()[0]);
        }
    }
}
