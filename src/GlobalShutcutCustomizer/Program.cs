using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;

namespace GlobalShutcutCustomizer
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var settings = ReadSetting(SettingFileName);
            var body = new AppBody();
            foreach (var setting in settings)
            {
                var icon = Icon.ExtractAssociatedIcon(setting.Path).ToBitmap();
                body.AddNotifyMenuItem($"{setting.Name} ({setting.Key})", icon);
            }
            Application.Run();
        }

        public static string SettingFileName { get; } = "settings.xlsx";

        public static Setting[] ReadSetting(string settingPath)
        {
            using (var stream = File.Open(settingPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // ヘッダーを読み飛ばす
                reader.Read();

                var settings = new List<Setting>();
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var args = reader.GetString(1) ?? "";
                    var keyString = reader.GetString(2);
                    var key = SettingUtil.StringToKeys(keyString);
                    var path = reader.GetString(3);
                    var setting = new Setting()
                    {
                        Name = name,
                        Args = args,
                        Key = key,
                        Path = path
                    };
                    settings.Add(setting);
                }
                return settings.ToArray();
            }
        }
    }
}
