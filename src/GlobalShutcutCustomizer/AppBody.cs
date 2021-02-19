using System;
using System.Drawing;
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
            var AppPath = Application.ExecutablePath;
            return Icon.ExtractAssociatedIcon(AppPath);
        }

        public void AddNotifyMenuItem(string title, Image image = null, Action action = null)
        {

            if (action == null)
            {
                NotifyMenu.Items.Add($"&{title}", image);
                return;
            }
            var menuItem = new ToolStripMenuItem { Text = $"&{title}" };
            menuItem.Click += new EventHandler((sender, e) => action());
            NotifyMenu.Items.Add(menuItem);
            return;
        }

    }
}
