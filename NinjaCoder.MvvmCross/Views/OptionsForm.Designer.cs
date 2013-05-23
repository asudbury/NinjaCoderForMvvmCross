namespace NinjaCoder.MvvmCross.Views
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.chkCreateLogFile = new System.Windows.Forms.CheckBox();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.textBoxLogFile = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.SuspendLayout();
            // 
            // chkCreateLogFile
            // 
            this.chkCreateLogFile.AutoSize = true;
            this.chkCreateLogFile.Location = new System.Drawing.Point(376, 36);
            this.chkCreateLogFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkCreateLogFile.Name = "chkCreateLogFile";
            this.chkCreateLogFile.Size = new System.Drawing.Size(97, 17);
            this.chkCreateLogFile.TabIndex = 0;
            this.chkCreateLogFile.Text = "Create Log File";
            this.chkCreateLogFile.UseVisualStyleBackColor = true;
            // 
            // lblLogFile
            // 
            this.lblLogFile.Location = new System.Drawing.Point(324, 63);
            this.lblLogFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(61, 13);
            this.lblLogFile.TabIndex = 1;
            this.lblLogFile.Text = "Log File";
            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Location = new System.Drawing.Point(376, 58);
            this.textBoxLogFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.ReadOnly = true;
            this.textBoxLogFile.Size = new System.Drawing.Size(293, 20);
            this.textBoxLogFile.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(514, 289);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 22);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(595, 289);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 22);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(12, 13);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(290, 306);
            this.logo1.TabIndex = 5;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 331);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxLogFile);
            this.Controls.Add(this.lblLogFile);
            this.Controls.Add(this.chkCreateLogFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OptionsForm";
            this.Text = "Ninja Coder for MvvmCross - Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCreateLogFile;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.TextBox textBoxLogFile;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private UserControls.Logo logo1;
    }
}