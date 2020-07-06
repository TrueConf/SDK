namespace WindowsFormsControlLibrary
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.axTrueConfCallX1 = new AxTrueConf_CallXLib.AxTrueConfCallX();
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axTrueConfCallX1
            // 
            this.axTrueConfCallX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTrueConfCallX1.Enabled = true;
            this.axTrueConfCallX1.Location = new System.Drawing.Point(0, 0);
            this.axTrueConfCallX1.Name = "axTrueConfCallX1";
            this.axTrueConfCallX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTrueConfCallX1.OcxState")));
            this.axTrueConfCallX1.Size = new System.Drawing.Size(800, 450);
            this.axTrueConfCallX1.TabIndex = 0;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axTrueConfCallX1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.axTrueConfCallX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTrueConf_CallXLib.AxTrueConfCallX axTrueConfCallX1;
    }
}
