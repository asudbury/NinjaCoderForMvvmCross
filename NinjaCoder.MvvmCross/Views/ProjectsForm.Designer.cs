namespace NinjaCoder.MvvmCross.Views
{
    partial class ProjectsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsForm));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxUseNuget = new System.Windows.Forms.CheckBox();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.lblRequiredProjects = new System.Windows.Forms.Label();
            this.buttonPath = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxProject = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.buttonOK.Location = new System.Drawing.Point(340, 439);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 29);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // checkBoxUseNuget
            // 
            this.checkBoxUseNuget.AutoSize = true;
            this.checkBoxUseNuget.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.checkBoxUseNuget.Location = new System.Drawing.Point(265, 105);
            this.checkBoxUseNuget.Name = "checkBoxUseNuget";
            this.checkBoxUseNuget.Size = new System.Drawing.Size(283, 23);
            this.checkBoxUseNuget.TabIndex = 11;
            this.checkBoxUseNuget.Text = "Use Nuget (Internet connection required)";
            this.checkBoxUseNuget.UseVisualStyleBackColor = true;
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(12, 21);
            this.logo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(189, 405);
            this.logo1.TabIndex = 10;
            // 
            // mvxListView1
            // 
            this.mvxListView1.BackColor = System.Drawing.Color.White;
            this.mvxListView1.Location = new System.Drawing.Point(10, 12);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(10);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(229, 223);
            this.mvxListView1.TabIndex = 6;
            // 
            // lblRequiredProjects
            // 
            this.lblRequiredProjects.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.lblRequiredProjects.Location = new System.Drawing.Point(261, 138);
            this.lblRequiredProjects.Name = "lblRequiredProjects";
            this.lblRequiredProjects.Size = new System.Drawing.Size(217, 17);
            this.lblRequiredProjects.TabIndex = 5;
            this.lblRequiredProjects.Text = "Required Projects";
            // 
            // buttonPath
            // 
            this.buttonPath.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.buttonPath.Location = new System.Drawing.Point(531, 21);
            this.buttonPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(34, 24);
            this.buttonPath.TabIndex = 2;
            this.buttonPath.Text = "...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.ButtonPathClick);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(264, 21);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(256, 25);
            this.textBoxPath.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label9.Location = new System.Drawing.Point(218, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 19);
            this.label9.TabIndex = 0;
            this.label9.Text = "Path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label4.Location = new System.Drawing.Point(205, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Project";
            // 
            // textBoxProject
            // 
            this.textBoxProject.Location = new System.Drawing.Point(264, 62);
            this.textBoxProject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxProject.Name = "textBoxProject";
            this.textBoxProject.Size = new System.Drawing.Size(256, 25);
            this.textBoxProject.TabIndex = 4;
            this.textBoxProject.TextChanged += new System.EventHandler(this.TextBoxProjectTextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.buttonCancel.Location = new System.Drawing.Point(436, 439);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 29);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mvxListView1);
            this.panel1.Location = new System.Drawing.Point(264, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 263);
            this.panel1.TabIndex = 12;
            // 
            // ProjectsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 491);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxUseNuget);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblRequiredProjects);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxProject);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectsForm";
            this.Text = "Ninja - Add Projects";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectsFormFormClosing);
            this.Load += new System.EventHandler(this.SolutionOptionsFormLoad);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox checkBoxUseNuget;
        private System.Windows.Forms.Panel panel1;
    }
}