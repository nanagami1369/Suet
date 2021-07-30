using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using NAudio.CoreAudioApi;

namespace VolumeCommandManager
{
    static class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option(new[] {"--help", "-h", "-?", "/?"}),
                new Option<string>(new[] {"-mode", "-m"}),
            };
            rootCommand.Handler = CommandHandler.Create<bool, string>((help, mode) =>
             {
                 if (help)
                 {
                     MessageBox.Show(
                                      $@"使用法:
    {AppBody.AppName} [args] [option]
システムのボリュームを調整します

引数
    -m -mode <Mute,Up,Down> どの操作をするのかを指定します

オプション:
    /? -? -h --help   ヘルプ

例:
    {AppBody.AppName} -m Mute         ...ミュートにする。すでにミュートなら解除する
    {AppBody.AppName} -m Up           ...音量を上げる
    {AppBody.AppName} -m Down         ...音量を下げる
");
                     return;
                 }

                 var enumerator = new MMDeviceEnumerator();
                 var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                 switch (mode)
                 {
                     case "Mute":
                         device.AudioEndpointVolume.Mute = !device.AudioEndpointVolume.Mute;
                         break;
                     case "Up":
                         if (device.AudioEndpointVolume.MasterVolumeLevelScalar + 0.01f <= 1.00f)
                         {
                             device.AudioEndpointVolume.MasterVolumeLevelScalar += 0.01f;
                         }
                         break;
                     case "Down":
                         if (device.AudioEndpointVolume.MasterVolumeLevelScalar - 0.01f >= 0.00f)
                         {
                             device.AudioEndpointVolume.MasterVolumeLevelScalar -= 0.01f;
                         }
                         break;
                     default:
                         MessageBox.Show($"mode optionは以下の３つのみ受け付けます<Mute,Up,Down> 入力:{mode}");
                         break;
                 }
             });
            return await rootCommand.InvokeAsync(args);
        }
    }
}
