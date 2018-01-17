namespace FileTransferWindowsSDKDemo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.peerId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpenSelectedFile = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.lblPeerId = new System.Windows.Forms.Label();
            this.txtPeerId = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.sdk = new AxTrueConf_CallXLib.AxTrueConfCallX();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLblConference = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnChangeServer = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUpdateFileList = new System.Windows.Forms.Button();
            this.labelErrorOpenFile = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sdk)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.peerId,
            this.path});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.Location = new System.Drawing.Point(400, 41);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(504, 506);
            this.listViewFiles.TabIndex = 1;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Name";
            // 
            // peerId
            // 
            this.peerId.Text = "Sender";
            // 
            // path
            // 
            this.path.Text = "Path";
            // 
            // btnOpenSelectedFile
            // 
            this.btnOpenSelectedFile.Location = new System.Drawing.Point(659, 12);
            this.btnOpenSelectedFile.Name = "btnOpenSelectedFile";
            this.btnOpenSelectedFile.Size = new System.Drawing.Size(197, 23);
            this.btnOpenSelectedFile.TabIndex = 2;
            this.btnOpenSelectedFile.Text = "Open selected file";
            this.btnOpenSelectedFile.UseVisualStyleBackColor = true;
            this.btnOpenSelectedFile.Click += new System.EventHandler(this.btnOpenSelectedFile_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(12, 381);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Choose file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // lblPeerId
            // 
            this.lblPeerId.AutoSize = true;
            this.lblPeerId.Location = new System.Drawing.Point(21, 358);
            this.lblPeerId.Name = "lblPeerId";
            this.lblPeerId.Size = new System.Drawing.Size(68, 13);
            this.lblPeerId.TabIndex = 4;
            this.lblPeerId.Text = "Type user id:";
            // 
            // txtPeerId
            // 
            this.txtPeerId.Location = new System.Drawing.Point(95, 355);
            this.txtPeerId.Name = "txtPeerId";
            this.txtPeerId.Size = new System.Drawing.Size(299, 20);
            this.txtPeerId.TabIndex = 5;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(93, 381);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(299, 20);
            this.txtFilePath.TabIndex = 5;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(218, 407);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(174, 35);
            this.btnSendFile.TabIndex = 3;
            this.btnSendFile.Text = "Send file";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // sdk
            // 
            this.sdk.Enabled = true;
            this.sdk.Location = new System.Drawing.Point(13, 44);
            this.sdk.Name = "sdk";
            this.sdk.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("sdk.OcxState")));
            this.sdk.Size = new System.Drawing.Size(379, 294);
            this.sdk.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLblConference,
            this.statusLblLogin,
            this.statusLblServer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 553);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(916, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLblConference
            // 
            this.statusLblConference.Name = "statusLblConference";
            this.statusLblConference.Size = new System.Drawing.Size(31, 17);
            this.statusLblConference.Text = "conf";
            // 
            // statusLblLogin
            // 
            this.statusLblLogin.Name = "statusLblLogin";
            this.statusLblLogin.Size = new System.Drawing.Size(34, 17);
            this.statusLblLogin.Text = "login";
            // 
            // statusLblServer
            // 
            this.statusLblServer.Name = "statusLblServer";
            this.statusLblServer.Size = new System.Drawing.Size(38, 17);
            this.statusLblServer.Text = "server";
            // 
            // btnChangeServer
            // 
            this.btnChangeServer.Enabled = false;
            this.btnChangeServer.Location = new System.Drawing.Point(95, 12);
            this.btnChangeServer.Name = "btnChangeServer";
            this.btnChangeServer.Size = new System.Drawing.Size(197, 23);
            this.btnChangeServer.TabIndex = 2;
            this.btnChangeServer.Text = "Change server or relogin";
            this.btnChangeServer.UseVisualStyleBackColor = true;
            this.btnChangeServer.Click += new System.EventHandler(this.btnChangeServer_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUpdateFileList
            // 
            this.btnUpdateFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdateFileList.Location = new System.Drawing.Point(862, 5);
            this.btnUpdateFileList.Name = "btnUpdateFileList";
            this.btnUpdateFileList.Size = new System.Drawing.Size(42, 34);
            this.btnUpdateFileList.TabIndex = 8;
            this.btnUpdateFileList.Text = "🔃";
            this.btnUpdateFileList.UseVisualStyleBackColor = true;
            this.btnUpdateFileList.Click += new System.EventHandler(this.btnUpdateFileList_Click);
            // 
            // labelErrorOpenFile
            // 
            this.labelErrorOpenFile.AutoSize = true;
            this.labelErrorOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelErrorOpenFile.ForeColor = System.Drawing.Color.Red;
            this.labelErrorOpenFile.Location = new System.Drawing.Point(397, 12);
            this.labelErrorOpenFile.Name = "labelErrorOpenFile";
            this.labelErrorOpenFile.Size = new System.Drawing.Size(0, 17);
            this.labelErrorOpenFile.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 575);
            this.Controls.Add(this.labelErrorOpenFile);
            this.Controls.Add(this.btnUpdateFileList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.sdk);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.txtPeerId);
            this.Controls.Add(this.lblPeerId);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnChangeServer);
            this.Controls.Add(this.btnOpenSelectedFile);
            this.Controls.Add(this.listViewFiles);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.sdk)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader peerId;
        private System.Windows.Forms.Button btnOpenSelectedFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label lblPeerId;
        private System.Windows.Forms.TextBox txtPeerId;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSendFile;
        private AxTrueConf_CallXLib.AxTrueConfCallX sdk;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLblConference;
        private System.Windows.Forms.ToolStripStatusLabel statusLblLogin;
        private System.Windows.Forms.ToolStripStatusLabel statusLblServer;
        private System.Windows.Forms.Button btnChangeServer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.Button btnUpdateFileList;
        private System.Windows.Forms.Label labelErrorOpenFile;
    }
}