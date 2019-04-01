namespace DemoSDK
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonCall = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTrueConfIdForCall = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusConference = new System.Windows.Forms.ToolStripStatusLabel();
            this.axTrueConfCallX1 = new AxTrueConf_CallXLib.AxTrueConfCallX();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseMicrophoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseSpreakersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCall
            // 
            this.buttonCall.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonCall.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.buttonCall.FlatAppearance.BorderSize = 2;
            this.buttonCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.ForestGreen;
            this.buttonCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.buttonCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(525, 123);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(304, 51);
            this.buttonCall.TabIndex = 25;
            this.buttonCall.Text = "📞";
            this.buttonCall.UseVisualStyleBackColor = false;
            this.buttonCall.Click += new System.EventHandler(this.buttonCall_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(525, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(304, 23);
            this.label5.TabIndex = 24;
            this.label5.Text = "Type TrueConf id:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTrueConfIdForCall
            // 
            this.textBoxTrueConfIdForCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxTrueConfIdForCall.Location = new System.Drawing.Point(525, 88);
            this.textBoxTrueConfIdForCall.Name = "textBoxTrueConfIdForCall";
            this.textBoxTrueConfIdForCall.Size = new System.Drawing.Size(304, 26);
            this.textBoxTrueConfIdForCall.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(525, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 47);
            this.label4.TabIndex = 23;
            this.label4.Text = "Call the User:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLogin,
            this.toolStripStatusServer,
            this.toolStripStatusConference});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(848, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLogin
            // 
            this.toolStripStatusLogin.Name = "toolStripStatusLogin";
            this.toolStripStatusLogin.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLogin.Text = "Login status";
            // 
            // toolStripStatusServer
            // 
            this.toolStripStatusServer.Name = "toolStripStatusServer";
            this.toolStripStatusServer.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusServer.Text = "Server status";
            // 
            // toolStripStatusConference
            // 
            this.toolStripStatusConference.Name = "toolStripStatusConference";
            this.toolStripStatusConference.Size = new System.Drawing.Size(102, 17);
            this.toolStripStatusConference.Text = "Conference status";
            // 
            // axTrueConfCallX1
            // 
            this.axTrueConfCallX1.Enabled = true;
            this.axTrueConfCallX1.Location = new System.Drawing.Point(12, 39);
            this.axTrueConfCallX1.Name = "axTrueConfCallX1";
            this.axTrueConfCallX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTrueConfCallX1.OcxState")));
            this.axTrueConfCallX1.Size = new System.Drawing.Size(507, 375);
            this.axTrueConfCallX1.TabIndex = 30;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(848, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loginToolStripMenuItem.Text = "Login ➔";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.LoginToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseCameraToolStripMenuItem,
            this.chooseMicrophoneToolStripMenuItem,
            this.chooseSpreakersToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // chooseCameraToolStripMenuItem
            // 
            this.chooseCameraToolStripMenuItem.Name = "chooseCameraToolStripMenuItem";
            this.chooseCameraToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.chooseCameraToolStripMenuItem.Text = "Choose camera 🎥";
            this.chooseCameraToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ChooseCameraToolStripMenuItem_DropDownItemClicked);
            // 
            // chooseMicrophoneToolStripMenuItem
            // 
            this.chooseMicrophoneToolStripMenuItem.Name = "chooseMicrophoneToolStripMenuItem";
            this.chooseMicrophoneToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.chooseMicrophoneToolStripMenuItem.Text = "Choose microphone 🎤";
            this.chooseMicrophoneToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ChooseMicrophoneToolStripMenuItem_DropDownItemClicked);
            // 
            // chooseSpreakersToolStripMenuItem
            // 
            this.chooseSpreakersToolStripMenuItem.Name = "chooseSpreakersToolStripMenuItem";
            this.chooseSpreakersToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.chooseSpreakersToolStripMenuItem.Text = "Choose spreakers 🎧";
            this.chooseSpreakersToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ChooseSpreakersToolStripMenuItem_DropDownItemClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 454);
            this.Controls.Add(this.axTrueConfCallX1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTrueConfIdForCall);
            this.Controls.Add(this.label4);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTrueConfIdForCall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLogin;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusServer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConference;
        private AxTrueConf_CallXLib.AxTrueConfCallX axTrueConfCallX1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseMicrophoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseSpreakersToolStripMenuItem;
    }
}

