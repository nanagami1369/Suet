using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GlobalShutcutCustomizer
{
    internal class AppBody : Form
    {
        public AppBody()
        {
            AppIcon = GetAppIcon();
            ShowInTaskbar = false;
            NotifyIcon = new NotifyIcon {Icon = AppIcon, Visible = true, Text = nameof(GlobalShutcutCustomizer)};
            AddNotifyMenuItem("終了", action: () => Application.Exit());
            NotifyIcon.ContextMenuStrip = NotifyMenu;
        }

        private Icon AppIcon { get; }

        public NotifyIcon NotifyIcon { get; }

        public ContextMenuStrip NotifyMenu { get; } = new ContextMenuStrip();

        private Icon GetAppIcon()
        {
            var appDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            var appPath = Path.Combine(appDirPath, appName);
            return Icon.ExtractAssociatedIcon(appPath);
        }

        public void AddNotifyMenuItem(string title, Image image = null, Action action = null)
        {
            if (action == null)
            {
                NotifyMenu.Items.Add($"&{title}", image);
                return;
            }

            var menuItem = new ToolStripMenuItem {Text = $"&{title}", Image = image};
            menuItem.Click += (sender, e) => action();
            NotifyMenu.Items.Add(menuItem);
        }
    }
}
