using Microsoft.Win32;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IdleKill
{
    public partial class FormIdle : Form
    {
        public int SecLimit = 60 * 20;
        public bool isLocked = false;
        public bool sleep = true;
        public bool hibernate = false;
        public string beepSoundFile = null;

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        [DllImport("powrprof.dll", SetLastError = true)]
        static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);


        // CONSTRUCTOR
        public FormIdle()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            LoadConfig();

            SystemEvents.SessionSwitch += (s, e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    this.timer.Stop();
                    isLocked = true;
                    Program.WriteAppLog("PC was locked");
                }
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    this.timer.Start();
                    isLocked = false;
                    Program.WriteAppLog("PC was unlocked");
                }
            };
        }

        // EVENT FORM LOAD
        private void FormIdle_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Normal;
        }


        // API GET IDLE TIME
        static TimeSpan GetIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint idleTicks = GetTickCount() - lastInputInfo.dwTime;
                return TimeSpan.FromMilliseconds(idleTicks);
            }
            else
            {
                throw new Exception("Failed to get last input info.");
            }
        }

        public FormNotification notification = null;

        // TIMER TRIGGER
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isLocked)
            {
                this.timer.Stop();
                return;
            }

            TimeSpan idleTime = GetIdleTime();

            Program.WriteAppLog("Current iddle time: " + idleTime.TotalSeconds.ToString());  // DEBUG

            if (idleTime.TotalSeconds > SecLimit)
            {
                this.timer.Stop();
                this.Hibernate();
            }
            else if ((SecLimit - idleTime.TotalSeconds) < 11)
            {
                if (notification == null)
                {
                    notification = new FormNotification(() =>
                    {
                        this.notification = null;
                    }, beepSoundFile);
                    notification.Show();
                }
            }
            else
            {
                if (notification != null)
                {
                    notification.Close();
                }
            }
        }


        // SLEEP
        public void Sleep()
        {
            Program.WriteAppLog("Going to sleep.");
            bool result = SetSuspendState(false, true, false);
            if (!result)
            {
                Program.WriteAppLog("Failed to put the PC to sleep.");
            }
            else
            {
                Program.WriteAppLog("Sleep command sent.");
            }
        }

        // HIBERNATE
        public void Hibernate()
        {
            Program.WriteAppLog("Going to hibernate.");
            bool result = SetSuspendState(true, true, true);
            if (!result)
            {
                Program.WriteAppLog("Failed to put the PC to hibernate.");
            }
            else
            {
                Program.WriteAppLog("Hibernate command sent.");
            }
        }


        //  MAIN FORM BUTTON CLICk
        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                SecLimit = Int32.Parse(textBox.Text);

                if (SecLimit < 65)
                {
                    SecLimit = 65;
                }

                Program.WriteAppLog("Limit set to: " + SecLimit.ToString());

                SaveConfig();
                this.Hide();
            }
            catch (Exception)
            {


            }

        }

        // MAIN FORM TEXTBOX INPUT CHANGE
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // GET CONFIG PATH
        private string GetConfigPath()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Program.AppName
            );

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return Path.Combine(folder, "config.xml");
        }

        // CONFIG SAVE
        public void SaveConfig()
        {
            var xml = new XElement("Config",
                         new XElement("time", SecLimit),
                         new XElement("hibernate", hibernate ? "1" : "0"),
                         new XElement("sleep", sleep ? "1" : "0"),
                         new XElement("beepSoundFile", beepSoundFile != null && File.Exists(beepSoundFile) ? beepSoundFile : "")
            );

            xml.Save(GetConfigPath());

            Program.WriteAppLog("Configuration saved");
        }

        // CONFIG LOAD
        public void LoadConfig()
        {
            string path = GetConfigPath();
            if (!File.Exists(path))
                return;

            try
            {
                var xml = XElement.Load(path);
                SecLimit = int.Parse(xml.Element("time")?.Value ?? "1200");
                hibernate = (xml.Element("hibernate")?.Value ?? "0") == "1";
                sleep = (xml.Element("sleep")?.Value ?? "0") == "1";
                beepSoundFile = xml.Element("beepSoundFile")?.Value ?? "";

                if (beepSoundFile != "" && !File.Exists(beepSoundFile)) {
                    beepSoundFile = null;
                }

                Program.WriteAppLog("Config: SecLimit = " + SecLimit.ToString());
                Program.WriteAppLog("Config: hibernate = " + hibernate.ToString());
                Program.WriteAppLog("Config: sleep = " + sleep.ToString());

            }
            catch
            {

            }
        }


        // APP CURRENT EXECUTABLE PATH
        private static string GetAppPath()
        {
            return Application.ExecutablePath;
        }

        // AUTORUN SET
        public static void SetAutoRun()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key.SetValue(Program.AppName, $"\"{GetAppPath()}\"");
            }
        }

        // AUTORUN UNSET
        public static void UnsetAutoRun()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key.GetValue(Program.AppName) != null)
                    key.DeleteValue(Program.AppName);
            }
        }

        // AUTORUN CHECK
        public static bool IsAutoRunSet()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false))
            {
                string value = key?.GetValue(Program.AppName) as string;
                return string.Equals(value, $"\"{GetAppPath()}\"", StringComparison.OrdinalIgnoreCase);
            }
        }


        // EVENT FORM CLOSING
        private void FormIdle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible)
            {
                e.Cancel = true;
                this.Hide();
            }

        }

        // FORM SHOW
        private void ShowForm()
        {
            textBox.Text = SecLimit.ToString();

            var screen = Screen.FromControl(this);
            var workingArea = screen.WorkingArea;

            this.Location = new Point(
                workingArea.Left + (workingArea.Width - this.Width) / 2,
                workingArea.Top + (workingArea.Height - this.Height) / 2
            );
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Show();
            this.TopMost = true;
            this.BringToFront();
            this.Activate();
            this.TopMost = false;
            this.WindowState = FormWindowState.Normal;
        }

        // NOTIFIICON DBL CLICK
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.ShowForm();
        }

        // CONTEXTMENU OPENING
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.autorunToolStripMenuItem1.Checked = IsAutoRunSet();
            this.hibernateToolStripMenuItem.Checked = hibernate;
            this.sleepToolStripMenuItem.Checked = sleep;
        }

        // CONTEXTMENU SET TIME INTERVAL
        private void setIntervalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowForm();
        }

        // CONTEXTMENU EXIT APP
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveConfig();
            Application.Exit();
        }

        // CONTEXTMENU AUTORUN
        private void autorunToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAutoRunSet())
            {
                UnsetAutoRun();
                Program.WriteAppLog("Config: unset autorun");
            }
            else
            {
                SetAutoRun();
                Program.WriteAppLog("Config: set autorun");
            }

            this.autorunToolStripMenuItem1.Checked = IsAutoRunSet();
        }

        // CONTEXTMENU HIBERNATE
        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.WriteAppLog("Config: set to hibernate");

            hibernate = true;
            sleep = false;
            this.hibernateToolStripMenuItem.Checked = hibernate;
            this.sleepToolStripMenuItem.Checked = sleep;
            SaveConfig();
        }

        // CONTEXTMENU SLEEP
        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.WriteAppLog("Config: set to sleep");

            hibernate = false;
            sleep = true;
            this.hibernateToolStripMenuItem.Checked = hibernate;
            this.sleepToolStripMenuItem.Checked = sleep;
            SaveConfig();
        }

        // CONTEXTMENU VIEW LOG
        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Program.logFilePath,
                UseShellExecute = true
            });
        }

        // CONTEXTMENU CLEAR LOG 
        private void clearLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            File.WriteAllText(Program.logFilePath, string.Empty);
        }

        private void selectBeepSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "WAV files (*.wav)|*.wav";
                ofd.Title = "Select a WAV Sound File";

                if (ofd.ShowDialog() == DialogResult.OK && File.Exists(ofd.FileName))
                {
                    beepSoundFile = ofd.FileName;
                }
            }
        }
    }
}
