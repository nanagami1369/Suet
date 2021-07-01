using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ExcelDataReader;
using NHotkey.WindowsForms;
#if NET5_0
using System.Text;

#endif

namespace GlobalShutcutCustomizer
{
    internal static class Program
    {
        public static string SettingFileName { get; } = "settings.xlsx";

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if NET5_0
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            var appDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var settingName = SettingFileName;
            var settingPath = Path.Combine(appDirPath, settingName);
            Setting[] settings;
            try
            {
                settings = ReadSetting(settingPath);
            }
            catch (SettingValidationException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show($"ファイル「{SettingFileName}」が見つかりませんでした\nファイルパス:\n{e.FileName}");
                return;
            }

            var body = new AppBody();
            try
            {
                foreach (var setting in settings)
                {
                    var icon = Icon.ExtractAssociatedIcon(setting.Path).ToBitmap();
                    body.AddNotifyMenuItem($"{setting.Name} ({setting.Key})", icon, () => InvokeApp(setting));
                    HotkeyManager.Current.AddOrReplace(setting.Name, setting.Key, (sender, e) => InvokeApp(setting));
                }

                Application.ThreadException += (sender, e) =>
                {
                    MessageBox.Show(e.Exception.Message);
                    foreach (var setting in settings)
                    {
                        HotkeyManager.Current.Remove(setting.Name);
                    }
                };
                Thread.GetDomain().UnhandledException += (sender, e) =>
                {
                    if (e.ExceptionObject is Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        foreach (var setting in settings)
                        {
                            HotkeyManager.Current.Remove(setting.Name);
                        }
                    }
                };
                Application.Run();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                foreach (var setting in settings)
                {
                    HotkeyManager.Current.Remove(setting.Name);
                }

                throw;
            }
            finally
            {
                foreach (var setting in settings)
                {
                    HotkeyManager.Current.Remove(setting.Name);
                }
            }
        }

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
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new SettingValidationException("名前の書かれていない行があります");
                    }

                    var args = reader.GetString(1) ?? "";
                    var keyString = reader.GetString(2);
                    Keys key;
                    try
                    {
                        key = SettingUtil.StringToKeys(keyString);
                    }
                    catch (ArgumentException)
                    {
                        throw new SettingValidationException($"{name}のキーが書かれていません");
                    }
                    catch (FormatException)
                    {
                        throw new SettingValidationException($"{name}のキーに変換できない文字がありました\n値：{keyString}");
                    }

                    var path = reader.GetString(3);
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        throw new SettingValidationException($"{name}のパスが書かれていません");
                    }

                    if (!File.Exists(path))
                    {
                        throw new SettingValidationException($"{name}で指定されている\n実行ファイル{path}が存在しません");
                    }

                    var setting = new Setting {Name = name, Args = args, Key = key, Path = path};
                    settings.Add(setting);
                }

                return settings.ToArray();
            }
        }

        public static void InvokeApp(Setting setting)
        {
            var info = new ProcessStartInfo
            {
                FileName = setting.Path, UseShellExecute = false, Arguments = setting.Args, CreateNoWindow = true
            };
            Process.Start(info);
        }
    }
}
