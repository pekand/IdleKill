namespace IdleKill
{
    partial class FormIdle
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIdle));
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            setIntervalToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            hibernateToolStripMenuItem = new ToolStripMenuItem();
            sleepToolStripMenuItem = new ToolStripMenuItem();
            autorunToolStripMenuItem1 = new ToolStripMenuItem();
            viewLogToolStripMenuItem = new ToolStripMenuItem();
            clearLogFileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            timer = new System.Windows.Forms.Timer(components);
            textBox = new TextBox();
            button = new Button();
            selectBeepSoundToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "IddleKill";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { setIntervalToolStripMenuItem, optionsToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(181, 98);
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            // 
            // setIntervalToolStripMenuItem
            // 
            setIntervalToolStripMenuItem.Name = "setIntervalToolStripMenuItem";
            setIntervalToolStripMenuItem.Size = new Size(180, 24);
            setIntervalToolStripMenuItem.Text = "Set interval";
            setIntervalToolStripMenuItem.Click += setIntervalToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hibernateToolStripMenuItem, sleepToolStripMenuItem, autorunToolStripMenuItem1, viewLogToolStripMenuItem, clearLogFileToolStripMenuItem, selectBeepSoundToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(180, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // hibernateToolStripMenuItem
            // 
            hibernateToolStripMenuItem.Name = "hibernateToolStripMenuItem";
            hibernateToolStripMenuItem.Size = new Size(189, 24);
            hibernateToolStripMenuItem.Text = "Hibernate";
            hibernateToolStripMenuItem.Click += hibernateToolStripMenuItem_Click;
            // 
            // sleepToolStripMenuItem
            // 
            sleepToolStripMenuItem.Name = "sleepToolStripMenuItem";
            sleepToolStripMenuItem.Size = new Size(189, 24);
            sleepToolStripMenuItem.Text = "Sleep";
            sleepToolStripMenuItem.Click += sleepToolStripMenuItem_Click;
            // 
            // autorunToolStripMenuItem1
            // 
            autorunToolStripMenuItem1.Name = "autorunToolStripMenuItem1";
            autorunToolStripMenuItem1.Size = new Size(189, 24);
            autorunToolStripMenuItem1.Text = "Autorun";
            autorunToolStripMenuItem1.Click += autorunToolStripMenuItem1_Click;
            // 
            // viewLogToolStripMenuItem
            // 
            viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            viewLogToolStripMenuItem.Size = new Size(189, 24);
            viewLogToolStripMenuItem.Text = "View log";
            viewLogToolStripMenuItem.Click += viewLogToolStripMenuItem_Click;
            // 
            // clearLogFileToolStripMenuItem
            // 
            clearLogFileToolStripMenuItem.Name = "clearLogFileToolStripMenuItem";
            clearLogFileToolStripMenuItem.Size = new Size(189, 24);
            clearLogFileToolStripMenuItem.Text = "Clear log file";
            clearLogFileToolStripMenuItem.Click += clearLogFileToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Tick += timer_Tick;
            // 
            // textBox
            // 
            textBox.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.Location = new Point(12, 12);
            textBox.Name = "textBox";
            textBox.Size = new Size(228, 46);
            textBox.TabIndex = 1;
            textBox.KeyPress += textBox_KeyPress;
            // 
            // button
            // 
            button.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button.Location = new Point(246, 12);
            button.Name = "button";
            button.Size = new Size(126, 46);
            button.TabIndex = 2;
            button.Text = "Set";
            button.UseVisualStyleBackColor = true;
            button.Click += button_Click;
            // 
            // selectBeepSoundToolStripMenuItem
            // 
            selectBeepSoundToolStripMenuItem.Name = "selectBeepSoundToolStripMenuItem";
            selectBeepSoundToolStripMenuItem.Size = new Size(189, 24);
            selectBeepSoundToolStripMenuItem.Text = "Select beep sound";
            selectBeepSoundToolStripMenuItem.Click += selectBeepSoundToolStripMenuItem_Click;
            // 
            // FormIdle
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 73);
            Controls.Add(button);
            Controls.Add(textBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormIdle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IdleKill";
            FormClosing += FormIdle_FormClosing;
            Load += FormIdle_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private TextBox textBox;
        private Button button;
        private ToolStripMenuItem setIntervalToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem hibernateToolStripMenuItem;
        private ToolStripMenuItem sleepToolStripMenuItem;
        private ToolStripMenuItem autorunToolStripMenuItem1;
        private ToolStripMenuItem viewLogToolStripMenuItem;
        private ToolStripMenuItem clearLogFileToolStripMenuItem;
        private ToolStripMenuItem selectBeepSoundToolStripMenuItem;
    }
}
