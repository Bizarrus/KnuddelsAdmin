using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace KnuddelsAdmin.Knuddels;
public class Applet {
    public Applet() { }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    private void find() {
        EnumWindows(EnumWindowsCallback, IntPtr.Zero);

        bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam) {
            StringBuilder windowText = new StringBuilder(256);
            GetWindowText(hWnd, windowText, 256);

            if(Regex.IsMatch(windowText.ToString(), "Channel: .*,.*Nick: .*")) {
                Console.WriteLine($"Fenster gefunden: {windowText}");
            }
            
            return true;
        }
    }

    public void sendCommand(String command) {
        find();
    }
}
