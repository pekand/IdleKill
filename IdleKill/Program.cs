namespace IdleKill
{
    internal static class Program
    {
        public const string AppName = "IdleKill";

        public static string logFilePath;

        public static int pid = System.Diagnostics.Process.GetCurrentProcess().Id;

        public static void InitAppLog()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string logDir = Path.Combine(localAppData, AppName, "Logs");

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            logFilePath = Path.Combine(logDir, "app.log");

            if (!File.Exists(logFilePath))
                Program.WriteAppLog("Log file created started");

            TimeZoneInfo localZone = TimeZoneInfo.Local;
            string timezoneName = localZone.IsDaylightSavingTime(DateTime.Now) ? localZone.DaylightName : localZone.StandardName;
            Program.WriteAppLog("Log init Timezone: "+ timezoneName);
        }

        public static void WriteAppLog(string message)
        {
            if (string.IsNullOrEmpty(logFilePath))
                throw new InvalidOperationException("Log not initialized. Call InitAppLog first.");

            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}] [PID:{pid}] {message}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Program.InitAppLog();
            Program.WriteAppLog("App started");

            bool createdNew;
            using (Mutex mutex = new Mutex(true, Program.AppName, out createdNew))
            {
                if (!createdNew)
                {
                    Program.WriteAppLog("App stoped: duplicate run detected");
                    return;
                }                

                ApplicationConfiguration.Initialize();
                Application.Run(new FormIdle());
            }


            Program.WriteAppLog("App stoped");
        }
    }
}