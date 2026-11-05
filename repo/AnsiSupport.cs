using System.Runtime.InteropServices;
namespace Conwey
{

    static class AnsiSupport
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(
            IntPtr hConsoleHandle,
            out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(
            IntPtr hConsoleHandle,
            uint dwMode);

        public static bool Enable()
        {
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);

            if (!GetConsoleMode(handle, out uint mode))
                return false;

            return SetConsoleMode(
                handle,
                mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
        }
    }
}
