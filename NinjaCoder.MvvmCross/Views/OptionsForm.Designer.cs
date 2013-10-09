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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxUseNugetForPlugins = new System.Windows.Forms.CheckBox();
            this.checkBoxUseNugetForProjectTemplates = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoveCodeComments = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoveFileHeaders = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeLibFolders = new System.Windows.Forms.CheckBox();
            this.checkBoxTrace = new System.Windows.Forms.CheckBox();
            this.chkCreateLogFile = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayErrors = new System.Windows.Forms.CheckBox();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.textBoxLogFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseNugetForServices = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxSuspendReSharper = new System.Windows.Forms.CheckBox();
            this.checkBoxFormatFunctionParameters = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(235, 723);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(341, 723);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // checkBoxUseNugetForPlugins
            // 
            this.checkBoxUseNugetForPlugins.AutoSize = true;
            this.checkBoxUseNugetForPlugins.Location = new System.Drawing.Point(11, 58);
            this.checkBoxUseNugetForPlugins.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseNugetForPlugins.Name = "checkBoxUseNugetForPlugins";
            this.checkBoxUseNugetForPlugins.Size = new System.Drawing.Size(168, 21);
            this.checkBoxUseNugetForPlugins.TabIndex = 16;
            this.checkBoxUseNugetForPlugins.Text = "Use Nuget for Plugins";
            this.checkBoxUseNugetForPlugins.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseNugetForProjectTemplates
            // 
            this.checkBoxUseNugetForProjectTemplates.AutoSize = true;
            this.checkBoxUseNugetForProjectTemplates.Location = new System.Drawing.Point(11, 33);
            this.checkBoxUseNugetForProjectTemplates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseNugetForProjectTemplates.Name = "checkBoxUseNugetForProjectTemplates";
            this.checkBoxUseNugetForProjectTemplates.Size = new System.Drawing.Size(236, 21);
            this.checkBoxUseNugetForProjectTemplates.TabIndex = 14;
            this.checkBoxUseNugetForProjectTemplates.Text = "Use Nuget for Project Templates";
            this.checkBoxUseNugetForProjectTemplates.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemoveCodeComments
            // 
            this.checkBoxRemoveCodeComments.AutoSize = true;
            this.checkBoxRemoveCodeComments.Location = new System.Drawing.Point(11, 59);
            this.checkBoxRemoveCodeComments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxRemoveCodeComments.Name = "checkBoxRemoveCodeComments";
            this.checkBoxRemoveCodeComments.Size = new System.Drawing.Size(189, 21);
            this.checkBoxRemoveCodeComments.TabIndex = 9;
            this.checkBoxRemoveCodeComments.Text = "Remove Code Comments";
            this.checkBoxRemoveCodeComments.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemoveFileHeaders
            // 
            this.checkBoxRemoveFileHeaders.AutoSize = true;
            this.checkBoxRemoveFileHeaders.Location = new System.Drawing.Point(11, 30);
            this.checkBoxRemoveFileHeaders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxRemoveFileHeaders.Name = "checkBoxRemoveFileHeaders";
            this.checkBoxRemoveFileHeaders.Size = new System.Drawing.Size(166, 21);
            this.checkBoxRemoveFileHeaders.TabIndex = 8;
            this.checkBoxRemoveFileHeaders.Text = "Remove File Headers";
            this.checkBoxRemoveFileHeaders.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeLibFolders
            // 
            this.checkBoxIncludeLibFolders.AutoSize = true;
            this.checkBoxIncludeLibFolders.Location = new System.Drawing.Point(11, 55);
            this.checkBoxIncludeLibFolders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxIncludeLibFolders.Name = "checkBoxIncludeLibFolders";
            this.checkBoxIncludeLibFolders.Size = new System.Drawing.Size(214, 21);
            this.checkBoxIncludeLibFolders.TabIndex = 7;
            this.checkBoxIncludeLibFolders.Text = "Include Lib folders in projects";
            this.checkBoxIncludeLibFolders.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrace
            // 
            this.checkBoxTrace.AutoSize = true;
            this.checkBoxTrace.Location = new System.Drawing.Point(11, 34);
            this.checkBoxTrace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxTrace.Name = "checkBoxTrace";
            this.checkBoxTrace.Size = new System.Drawing.Size(169, 21);
            this.checkBoxTrace.TabIndex = 9;
            this.checkBoxTrace.Text = "Trace Output enabled";
            this.checkBoxTrace.UseVisualStyleBackColor = true;
            // 
            // chkCreateLogFile
            // 
            this.chkCreateLogFile.AutoSize = true;
            this.chkCreateLogFile.Location = new System.Drawing.Point(9, 59);
            this.chkCreateLogFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkCreateLogFile.Name = "chkCreateLogFile";
            this.chkCreateLogFile.Size = new System.Drawing.Size(126, 21);
            this.chkCreateLogFile.TabIndex = 0;
            this.chkCreateLogFile.Text = "Create Log File";
            this.chkCreateLogFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayErrors
            // 
            this.checkBoxDisplayErrors.AutoSize = true;
            this.checkBoxDisplayErrors.Location = new System.Drawing.Point(11, 144);
            this.checkBoxDisplayErrors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxDisplayErrors.Name = "checkBoxDisplayErrors";
            this.checkBoxDisplayErrors.Size = new System.Drawing.Size(119, 21);
            this.checkBoxDisplayErrors.TabIndex = 8;
            this.checkBoxDisplayErrors.Text = "Display Errors";
            this.checkBoxDisplayErrors.UseVisualStyleBackColor = true;
            // 
            // lblLogFile
            // 
            this.lblLogFile.Location = new System.Drawing.Point(7, 96);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(63, 16);
            this.lblLogFile.TabIndex = 1;
            this.lblLogFile.Text = "Log File";
            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Location = new System.Drawing.Point(9, 114);
            this.textBoxLogFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.Size = new System.Drawing.Size(389, 22);
            this.textBoxLogFile.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxFormatFunctionParameters);
            this.groupBox1.Controls.Add(this.checkBoxRemoveFileHeaders);
            this.groupBox1.Controls.Add(this.checkBoxRemoveCodeComments);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(12, 143);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(429, 120);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coding Style";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxUseNugetForServices);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.LinkLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkBoxUseNugetForPlugins);
            this.groupBox2.Controls.Add(this.checkBoxUseNugetForProjectTemplates);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(12, 279);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(429, 227);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nuget";
            // 
            // checkBoxUseNugetForServices
            // 
            this.checkBoxUseNugetForServices.AutoSize = true;
            this.checkBoxUseNugetForServices.Location = new System.Drawing.Point(11, 83);
            this.checkBoxUseNugetForServices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseNugetForServices.Name = "checkBoxUseNugetForServices";
            this.checkBoxUseNugetForServices.Size = new System.Drawing.Size(300, 21);
            this.checkBoxUseNugetForServices.TabIndex = 20;
            this.checkBoxUseNugetForServices.Text = "Use Nuget for Services (Where applicable)";
            this.checkBoxUseNugetForServices.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Nuget version must be 2.5 or above";
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(11, 172);
            this.LinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(296, 17);
            this.LinkLabel.TabIndex = 18;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "Click here to download latest version of Nuget";
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "(Nuget integration requires internet connection)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxTrace);
            this.groupBox3.Controls.Add(this.chkCreateLogFile);
            this.groupBox3.Controls.Add(this.textBoxLogFile);
            this.groupBox3.Controls.Add(this.checkBoxDisplayErrors);
            this.groupBox3.Controls.Add(this.lblLogFile);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(12, 510);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(429, 192);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tracing";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxIncludeLibFolders);
            this.groupBox4.Controls.Add(this.checkBoxSuspendReSharper);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(12, 33);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(429, 86);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Build Options";
            // 
            // checkBoxSuspendReSharper
            // 
            this.checkBoxSuspendReSharper.AutoSize = true;
            this.checkBoxSuspendReSharper.Location = new System.Drawing.Point(11, 31);
            this.checkBoxSuspendReSharper.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxSuspendReSharper.Name = "checkBoxSuspendReSharper";
            this.checkBoxSuspendReSharper.Size = new System.Drawing.Size(237, 21);
            this.checkBoxSuspendReSharper.TabIndex = 7;
            this.checkBoxSuspendReSharper.Text = "Suspend ReSharper during build";
            this.checkBoxSuspendReSharper.UseVisualStyleBackColor = true;
            // 
            // checkBoxFormatFunctionParameters
            // 
            this.checkBoxFormatFunctionParameters.AutoSize = true;
            this.checkBoxFormatFunctionParameters.Location = new System.Drawing.Point(9, 89);
            this.checkBoxFormatFunctionParameters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxFormatFunctionParameters.Name = "checkBoxFormatFunctionParameters";
            this.checkBoxFormatFunctionParameters.Size = new System.Drawing.Size(307, 21);
            this.checkBoxFormatFunctionParameters.TabIndex = 10;
            this.checkBoxFormatFunctionParameters.Text = "Format Function Parameters on to new lines";
            this.checkBoxFormatFunctionParameters.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 762);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "Ninja Coder for MvvmCross - Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxUseNugetForPlugins;
        private System.Windows.Forms.CheckBox checkBoxUseNugetForProjectTemplates;
        private System.Windows.Forms.CheckBox checkBoxRemoveCodeComments;
        private System.Windows.Forms.CheckBox checkBoxRemoveFileHeaders;
        private System.Windows.Forms.CheckBox checkBoxIncludeLibFolders;
        private System.Windows.Forms.CheckBox checkBoxTrace;
        private System.Windows.Forms.CheckBox chkCreateLogFile;
        private System.Windows.Forms.CheckBox checkBoxDisplayErrors;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.TextBox textBoxLogFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxSuspendReSharper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel LinkLabel;
        private System.Windows.Forms.CheckBox checkBoxUseNugetForServices;
        private System.Windows.Forms.CheckBox checkBoxFormatFunctionParameters;
    }
}