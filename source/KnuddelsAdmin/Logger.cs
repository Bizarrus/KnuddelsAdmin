namespace KnuddelsAdmin;

class Logger {
    private static readonly object _lock        = new object();
    private static readonly string logFilePath  = "C:\\Users\\Bizzi\\AppData\\Roaming\\HotSpot\\StreamDock\\plugins\\com.github.bizarrus.knuddelsadmin.sdPlugin\\plugin.log";

    public static void Log(string message) {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

        Console.WriteLine(logEntry);
        Console.Out.Flush();

        lock (_lock) {
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}
