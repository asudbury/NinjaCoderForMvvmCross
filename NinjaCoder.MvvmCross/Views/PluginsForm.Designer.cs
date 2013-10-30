namespace NinjaCoder.MvvmCross.Views
{
    partial class PluginsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.lblSelectTheRequiredConverters = new System.Windows.Forms.Label();
            this.comboBoxViewModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.mvxListViewCore = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.checkBoxIncludeUnitTests = new System.Windows.Forms.CheckBox();
            this.checkBoxUseNuget = new System.Windows.Forms.CheckBox();
            this.LinkLabelDisplayWikiPage = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mvxListViewCommunity = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.mvxListViewUser = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.linkLabelOpenUserPluginsFolder = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.buttonOK.Location = new System.Drawing.Point(435, 558);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 29);
            this.buttonOK.TabIndex = 91;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.buttonCancel.Location = new System.Drawing.Point(530, 558);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 29);
            this.buttonCancel.TabIndex = 92;
            this.buttonCancel.Text = "&Cancel";
            // 
            // lblSelectTheRequiredConverters
            // 
            this.lblSelectTheRequiredConverters.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.lblSelectTheRequiredConverters.Location = new System.Drawing.Point(221, 19);
            this.lblSelectTheRequiredConverters.Name = "lblSelectTheRequiredConverters";
            this.lblSelectTheRequiredConverters.Size = new System.Drawing.Size(238, 19);
            this.lblSelectTheRequiredConverters.TabIndex = 93;
            this.lblSelectTheRequiredConverters.Text = "Select the required plugins";
            // 
            // comboBoxViewModel
            // 
            this.comboBoxViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewModel.FormattingEnabled = true;
            this.comboBoxViewModel.Location = new System.Drawing.Point(443, 490);
            this.comboBoxViewModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxViewModel.Name = "comboBoxViewModel";
            this.comboBoxViewModel.Size = new System.Drawing.Size(183, 25);
            this.comboBoxViewModel.TabIndex = 95;
            this.comboBoxViewModel.SelectedValueChanged += new System.EventHandler(this.ComboBoxViewModelSelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label1.Location = new System.Drawing.Point(121, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 19);
            this.label1.TabIndex = 96;
            this.label1.Text = "Optionally select ViewModel to implement plugins";
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(24, 41);
            this.logo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(188, 408);
            this.logo1.TabIndex = 94;
            // 
            // mvxListViewCore
            // 
            this.mvxListViewCore.Location = new System.Drawing.Point(4, 5);
            this.mvxListViewCore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListViewCore.Name = "mvxListViewCore";
            this.mvxListViewCore.Size = new System.Drawing.Size(376, 367);
            this.mvxListViewCore.TabIndex = 90;
            // 
            // checkBoxIncludeUnitTests
            // 
            this.checkBoxIncludeUnitTests.AutoSize = true;
            this.checkBoxIncludeUnitTests.Checked = true;
            this.checkBoxIncludeUnitTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeUnitTests.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.checkBoxIncludeUnitTests.Location = new System.Drawing.Point(419, 525);
            this.checkBoxIncludeUnitTests.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxIncludeUnitTests.Name = "checkBoxIncludeUnitTests";
            this.checkBoxIncludeUnitTests.Size = new System.Drawing.Size(208, 23);
            this.checkBoxIncludeUnitTests.TabIndex = 97;
            this.checkBoxIncludeUnitTests.Text = "Include ViewModel unit tests";
            this.checkBoxIncludeUnitTests.UseVisualStyleBackColor = true;
            this.checkBoxIncludeUnitTests.Visible = false;
            // 
            // checkBoxUseNuget
            // 
            this.checkBoxUseNuget.AutoSize = true;
            this.checkBoxUseNuget.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUseNuget.Location = new System.Drawing.Point(349, 462);
            this.checkBoxUseNuget.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseNuget.Name = "checkBoxUseNuget";
            this.checkBoxUseNuget.Size = new System.Drawing.Size(283, 23);
            this.checkBoxUseNuget.TabIndex = 98;
            this.checkBoxUseNuget.Text = "Use Nuget (Internet connection required)";
            this.checkBoxUseNuget.UseVisualStyleBackColor = true;
            // 
            // LinkLabelDisplayWikiPage
            // 
            this.LinkLabelDisplayWikiPage.AutoSize = true;
            this.LinkLabelDisplayWikiPage.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.LinkLabelDisplayWikiPage.Location = new System.Drawing.Point(185, 568);
            this.LinkLabelDisplayWikiPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkLabelDisplayWikiPage.Name = "LinkLabelDisplayWikiPage";
            this.LinkLabelDisplayWikiPage.Size = new System.Drawing.Size(242, 19);
            this.LinkLabelDisplayWikiPage.TabIndex = 99;
            this.LinkLabelDisplayWikiPage.TabStop = true;
            this.LinkLabelDisplayWikiPage.Text = "Display MvvmCross Plugins Wiki page";
            this.LinkLabelDisplayWikiPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDisplayWikiPageLinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(225, 41);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 413);
            this.tabControl1.TabIndex = 100;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mvxListViewCore);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(395, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Core Plugins";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.mvxListViewCommunity);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(395, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Community Plugins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // mvxListViewCommunity
            // 
            this.mvxListViewCommunity.Location = new System.Drawing.Point(4, 5);
            this.mvxListViewCommunity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListViewCommunity.Name = "mvxListViewCommunity";
            this.mvxListViewCommunity.Size = new System.Drawing.Size(356, 367);
            this.mvxListViewCommunity.TabIndex = 91;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabelOpenUserPluginsFolder);
            this.tabPage3.Controls.Add(this.mvxListViewUser);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(395, 383);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "User Plugins";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // mvxListViewUser
            // 
            this.mvxListViewUser.Location = new System.Drawing.Point(4, 5);
            this.mvxListViewUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListViewUser.Name = "mvxListViewUser";
            this.mvxListViewUser.Size = new System.Drawing.Size(356, 335);
            this.mvxListViewUser.TabIndex = 92;
            // 
            // linkLabelOpenUserPluginsFolder
            // 
            this.linkLabelOpenUserPluginsFolder.AutoSize = true;
            this.linkLabelOpenUserPluginsFolder.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.linkLabelOpenUserPluginsFolder.Location = new System.Drawing.Point(4, 352);
            this.linkLabelOpenUserPluginsFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelOpenUserPluginsFolder.Name = "linkLabelOpenUserPluginsFolder";
            this.linkLabelOpenUserPluginsFolder.Size = new System.Drawing.Size(181, 19);
            this.linkLabelOpenUserPluginsFolder.TabIndex = 100;
            this.linkLabelOpenUserPluginsFolder.TabStop = true;
            this.linkLabelOpenUserPluginsFolder.Text = "Open User Plugins directory";
            this.linkLabelOpenUserPluginsFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelOpenUserPluginsFolderLinkClicked);
            // 
            // PluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 611);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.LinkLabelDisplayWikiPage);
            this.Controls.Add(this.checkBoxUseNuget);
            this.Controls.Add(this.checkBoxIncludeUnitTests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxViewModel);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblSelectTheRequiredConverters);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginsForm";
            this.Text = "Ninja - Add Plugins";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.MvxListView mvxListViewCore;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label lblSelectTheRequiredConverters;
        private UserControls.Logo logo1;
        private System.Windows.Forms.ComboBox comboBoxViewModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxIncludeUnitTests;
        private System.Windows.Forms.CheckBox checkBoxUseNuget;
        private System.Windows.Forms.LinkLabel LinkLabelDisplayWikiPage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControls.MvxListView mvxListViewCommunity;
        private System.Windows.Forms.TabPage tabPage3;
        private UserControls.MvxListView mvxListViewUser;
        private System.Windows.Forms.LinkLabel linkLabelOpenUserPluginsFolder;
    }
}