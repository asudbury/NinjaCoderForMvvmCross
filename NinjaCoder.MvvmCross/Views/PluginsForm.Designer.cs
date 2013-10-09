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
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.checkBoxIncludeUnitTests = new System.Windows.Forms.CheckBox();
            this.checkBoxUseNuget = new System.Windows.Forms.CheckBox();
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(506, 523);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 91;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(614, 523);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 92;
            this.buttonCancel.Text = "&Cancel";
            // 
            // lblSelectTheRequiredConverters
            // 
            this.lblSelectTheRequiredConverters.Location = new System.Drawing.Point(337, 18);
            this.lblSelectTheRequiredConverters.Name = "lblSelectTheRequiredConverters";
            this.lblSelectTheRequiredConverters.Size = new System.Drawing.Size(272, 18);
            this.lblSelectTheRequiredConverters.TabIndex = 93;
            this.lblSelectTheRequiredConverters.Text = "Select the required plugins";
            // 
            // comboBoxViewModel
            // 
            this.comboBoxViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewModel.FormattingEnabled = true;
            this.comboBoxViewModel.Location = new System.Drawing.Point(340, 462);
            this.comboBoxViewModel.Name = "comboBoxViewModel";
            this.comboBoxViewModel.Size = new System.Drawing.Size(210, 24);
            this.comboBoxViewModel.TabIndex = 95;
            this.comboBoxViewModel.SelectedValueChanged += new System.EventHandler(this.ComboBoxViewModelSelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 17);
            this.label1.TabIndex = 96;
            this.label1.Text = "Optionally select ViewModel to implement plugins";
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(26, 39);
            this.logo1.Margin = new System.Windows.Forms.Padding(5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(278, 376);
            this.logo1.TabIndex = 94;
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(341, 39);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(373, 376);
            this.mvxListView1.TabIndex = 90;
            // 
            // checkBoxIncludeUnitTests
            // 
            this.checkBoxIncludeUnitTests.AutoSize = true;
            this.checkBoxIncludeUnitTests.Checked = true;
            this.checkBoxIncludeUnitTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeUnitTests.Location = new System.Drawing.Point(340, 493);
            this.checkBoxIncludeUnitTests.Name = "checkBoxIncludeUnitTests";
            this.checkBoxIncludeUnitTests.Size = new System.Drawing.Size(207, 21);
            this.checkBoxIncludeUnitTests.TabIndex = 97;
            this.checkBoxIncludeUnitTests.Text = "Include ViewModel unit tests";
            this.checkBoxIncludeUnitTests.UseVisualStyleBackColor = true;
            this.checkBoxIncludeUnitTests.Visible = false;
            // 
            // checkBoxUseNuget
            // 
            this.checkBoxUseNuget.AutoSize = true;
            this.checkBoxUseNuget.Location = new System.Drawing.Point(340, 435);
            this.checkBoxUseNuget.Name = "checkBoxUseNuget";
            this.checkBoxUseNuget.Size = new System.Drawing.Size(289, 21);
            this.checkBoxUseNuget.TabIndex = 98;
            this.checkBoxUseNuget.Text = "Use Nuget (Internet connection required)";
            this.checkBoxUseNuget.UseVisualStyleBackColor = true;
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(18, 533);
            this.LinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(326, 17);
            this.LinkLabel.TabIndex = 99;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "Click here to display MvvmCross Plugins Wiki page";
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // PluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 575);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.checkBoxUseNuget);
            this.Controls.Add(this.checkBoxIncludeUnitTests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxViewModel);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblSelectTheRequiredConverters);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.mvxListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginsForm";
            this.Text = "Ninja Coder for MvvmCross - Plugins";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.MvxListView mvxListView1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label lblSelectTheRequiredConverters;
        private UserControls.Logo logo1;
        private System.Windows.Forms.ComboBox comboBoxViewModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxIncludeUnitTests;
        private System.Windows.Forms.CheckBox checkBoxUseNuget;
        private System.Windows.Forms.LinkLabel LinkLabel;
    }
}