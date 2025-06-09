using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleKill
{
    public partial class FormNotification : Form
    {
        private System.Windows.Forms.Timer autoCloseTimer;
        private Action callback = null;

        SoundPlayer player = new SoundPlayer(Properties.Resources.beep);

        public FormNotification(Action callback)
        {
            InitializeComponent();
            SetupForm();
            ShowInCorner();
            StartAutoCloseTimer();

            this.callback = callback;

            pictureBox1.Image = Properties.Resources.IdleKill;
        }

        private void FormNotification_Load(object sender, EventArgs e)
        {
            beepStart();
        }

        private void SetupForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.LightYellow;
            TopMost = true;
        }

        private void ShowInCorner()
        {
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(
                workingArea.Right - Width - 10,
                workingArea.Bottom - Height - 10
            );
        }

        private void StartAutoCloseTimer()
        {
            autoCloseTimer = new System.Windows.Forms.Timer();
            autoCloseTimer.Interval = 10000; // 10 seconds
            autoCloseTimer.Tick += (s, e) =>
            {
                this.CloseForm();
            };
            autoCloseTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }

        public void beepStart()
        {
            player.Play();
        }

        public void beepStop()
        {
            player.Stop();
        }

        public void CloseForm()
        {
            this.Close();
        }

        private void FormNotification_FormClosing(object sender, FormClosingEventArgs e)
        {
            beepStop();
            autoCloseTimer.Stop();
            this.callback?.Invoke();
        }
    }
}
