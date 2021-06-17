using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GlobalShutcutCustomizer
{
    class AppBody : Form
    {
        private Icon AppIcon { get; }

        public NotifyIcon NotifyIcon { get; }

        public ContextMenuStrip NotifyMenu { get; } = new ContextMenuStrip();

        public AppBody()
        {
            AppIcon = GetAppIcon();
            ShowInTaskbar = false;
            NotifyIcon = new NotifyIcon
            {
                Icon = AppIcon,
                Visible = true,
                Text = nameof(GlobalShutcutCustomizer)
            };
            AddNotifyMenuItem(title: "終了", action: () => Application.Exit());
            NotifyIcon.ContextMenuStrip = NotifyMenu;
        }

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
            var menuItem = new ToolStripMenuItem { Text = $"&{title}", Image = image };
            menuItem.Click += new EventHandler((sender, e) => action());
            NotifyMenu.Items.Add(menuItem);
            return;
        }

    }
}
