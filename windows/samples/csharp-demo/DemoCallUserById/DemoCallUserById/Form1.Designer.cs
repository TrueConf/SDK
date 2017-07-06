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
            this.buttonEndCall = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTrueConfIdForCall = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSpeakers = new System.Windows.Forms.ComboBox();
            this.comboBoxMicrophones = new System.Windows.Forms.ComboBox();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.axTrueConfCallX1 = new AxTrueConf_CallXLib.AxTrueConfCallX();
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEndCall
            // 
            this.buttonEndCall.BackColor = System.Drawing.Color.Red;
            this.buttonEndCall.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEndCall.FlatAppearance.BorderSize = 2;
            this.buttonEndCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.buttonEndCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonEndCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEndCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEndCall.Location = new System.Drawing.Point(680, 123);
            this.buttonEndCall.Name = "buttonEndCall";
            this.buttonEndCall.Size = new System.Drawing.Size(149, 36);
            this.buttonEndCall.TabIndex = 27;
            this.buttonEndCall.Text = "End call";
            this.buttonEndCall.UseVisualStyleBackColor = false;
            this.buttonEndCall.Click += new System.EventHandler(this.buttonEndCall_Click);
            // 
            // buttonCall
            // 
            this.buttonCall.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonCall.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.buttonCall.FlatAppearance.BorderSize = 2;
            this.buttonCall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.ForestGreen;
            this.buttonCall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.buttonCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(525, 123);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(149, 36);
            this.buttonCall.TabIndex = 25;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = false;
            this.buttonCall.Click += new System.EventHandler(this.buttonCall_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(525, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 47);
            this.label4.TabIndex = 23;
            this.label4.Text = "Call the User:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(353, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 22;
            this.label3.Text = "Choose speakers:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(193, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Choose microphone:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Choose camera:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSpeakers
            // 
            this.comboBoxSpeakers.FormattingEnabled = true;
            this.comboBoxSpeakers.Location = new System.Drawing.Point(353, 35);
            this.comboBoxSpeakers.Name = "comboBoxSpeakers";
            this.comboBoxSpeakers.Size = new System.Drawing.Size(152, 21);
            this.comboBoxSpeakers.TabIndex = 19;
            this.comboBoxSpeakers.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpeakers_SelectedIndexChanged);
            // 
            // comboBoxMicrophones
            // 
            this.comboBoxMicrophones.FormattingEnabled = true;
            this.comboBoxMicrophones.Location = new System.Drawing.Point(193, 35);
            this.comboBoxMicrophones.Name = "comboBoxMicrophones";
            this.comboBoxMicrophones.Size = new System.Drawing.Size(154, 21);
            this.comboBoxMicrophones.TabIndex = 18;
            this.comboBoxMicrophones.SelectedIndexChanged += new System.EventHandler(this.comboBoxMicrophones_SelectedIndexChanged);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(26, 35);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(161, 21);
            this.comboBoxCameras.TabIndex = 17;
            this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameras_SelectedIndexChanged);
            // 
            // axTrueConfCallX1
            // 
            this.axTrueConfCallX1.Enabled = true;
            this.axTrueConfCallX1.Location = new System.Drawing.Point(12, 62);
            this.axTrueConfCallX1.Name = "axTrueConfCallX1";
            this.axTrueConfCallX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTrueConfCallX1.OcxState")));
            this.axTrueConfCallX1.Size = new System.Drawing.Size(493, 388);
            this.axTrueConfCallX1.TabIndex = 28;
            this.axTrueConfCallX1.OnXAfterStart += new System.EventHandler(this.axTrueConfCallX1_OnXAfterStart);
            this.axTrueConfCallX1.OnConferenceCreated += new AxTrueConf_CallXLib._ITrueConfCallXEvents_OnConferenceCreatedEventHandler(this.axTrueConfCallX1_OnConferenceCreated);
            this.axTrueConfCallX1.OnServerConnected += new AxTrueConf_CallXLib._ITrueConfCallXEvents_OnServerConnectedEventHandler(this.axTrueConfCallX1_OnServerConnected);
            this.axTrueConfCallX1.OnXLogin += new System.EventHandler(this.axTrueConfCallX1_OnXLoginOk);
            this.axTrueConfCallX1.OnXLoginError += new AxTrueConf_CallXLib._ITrueConfCallXEvents_OnXLoginErrorEventHandler(this.axTrueConfCallX1_OnXLoginError);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 462);
            this.Controls.Add(this.axTrueConfCallX1);
            this.Controls.Add(this.buttonEndCall);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTrueConfIdForCall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSpeakers);
            this.Controls.Add(this.comboBoxMicrophones);
            this.Controls.Add(this.comboBoxCameras);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEndCall;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTrueConfIdForCall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSpeakers;
        private System.Windows.Forms.ComboBox comboBoxMicrophones;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private AxTrueConf_CallXLib.AxTrueConfCallX axTrueConfCallX1;
    }
}

