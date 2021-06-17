using System.Windows.Forms;

namespace GlobalShutcutCustomizer
{
    internal class Setting
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Keys Key { get; set; }
        public string Args { get; set; }
    }
}
