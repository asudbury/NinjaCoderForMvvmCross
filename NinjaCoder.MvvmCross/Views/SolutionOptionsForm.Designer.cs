namespace NinjaCoder.MvvmCross.Views
{
    partial class SolutionOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionOptionsForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxProject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonPath = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblRequiredProjects = new System.Windows.Forms.Label();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(505, 312);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 22);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(424, 312);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 22);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // textBoxProject
            // 
            this.textBoxProject.Location = new System.Drawing.Point(364, 43);
            this.textBoxProject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxProject.Name = "textBoxProject";
            this.textBoxProject.Size = new System.Drawing.Size(187, 20);
            this.textBoxProject.TabIndex = 4;
            this.textBoxProject.TextChanged += new System.EventHandler(this.TextBoxProjectTextChanged);
            this.textBoxProject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxProjectKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Project";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(324, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Path";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(364, 12);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(187, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(555, 12);
            this.buttonPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(29, 19);
            this.buttonPath.TabIndex = 2;
            this.buttonPath.Text = "...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.ButtonPathClick);
            // 
            // lblRequiredProjects
            // 
            this.lblRequiredProjects.Location = new System.Drawing.Point(364, 81);
            this.lblRequiredProjects.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRequiredProjects.Name = "lblRequiredProjects";
            this.lblRequiredProjects.Size = new System.Drawing.Size(186, 13);
            this.lblRequiredProjects.TabIndex = 5;
            this.lblRequiredProjects.Text = "Required Projects";
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(364, 96);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(184, 188);
            this.mvxListView1.TabIndex = 6;
            // 
            // logo1
            // 
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(12, 12);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(290, 306);
            this.logo1.TabIndex = 10;
            // 
            // SolutionOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 367);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.mvxListView1);
            this.Controls.Add(this.lblRequiredProjects);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxProject);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolutionOptionsForm";
            this.Text = "Ninja Coder for MvvmCross - Add Projects";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxProject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblRequiredProjects;
        private UserControls.MvxListView mvxListView1;
        private UserControls.Logo logo1;
    }
}