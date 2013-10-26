namespace NinjaCoder.MvvmCross.Views
{
    partial class ServicesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesForm));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxViewModel = new System.Windows.Forms.ComboBox();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.lblSelectTheRequiredConverters = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.checkBoxIncludeUnitTests = new System.Windows.Forms.CheckBox();
            this.checkBoxUseNuget = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 19);
            this.label1.TabIndex = 103;
            this.label1.Text = "Select the ViewModel to implement services or leave blank";
            // 
            // comboBoxViewModel
            // 
            this.comboBoxViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewModel.FormattingEnabled = true;
            this.comboBoxViewModel.Location = new System.Drawing.Point(238, 348);
            this.comboBoxViewModel.Name = "comboBoxViewModel";
            this.comboBoxViewModel.Size = new System.Drawing.Size(184, 25);
            this.comboBoxViewModel.TabIndex = 102;
            this.comboBoxViewModel.SelectedValueChanged += new System.EventHandler(this.ComboBoxViewModelSelectedValueChanged);
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(23, 31);
            this.logo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(187, 399);
            this.logo1.TabIndex = 101;
            // 
            // lblSelectTheRequiredConverters
            // 
            this.lblSelectTheRequiredConverters.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.lblSelectTheRequiredConverters.Location = new System.Drawing.Point(235, 30);
            this.lblSelectTheRequiredConverters.Name = "lblSelectTheRequiredConverters";
            this.lblSelectTheRequiredConverters.Size = new System.Drawing.Size(238, 19);
            this.lblSelectTheRequiredConverters.TabIndex = 100;
            this.lblSelectTheRequiredConverters.Text = "Select the required services";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(422, 438);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 29);
            this.buttonOK.TabIndex = 98;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(516, 438);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 29);
            this.buttonCancel.TabIndex = 99;
            this.buttonCancel.Text = "&Cancel";
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(13, 12);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(252, 198);
            this.mvxListView1.TabIndex = 97;
            // 
            // checkBoxIncludeUnitTests
            // 
            this.checkBoxIncludeUnitTests.AutoSize = true;
            this.checkBoxIncludeUnitTests.Checked = true;
            this.checkBoxIncludeUnitTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeUnitTests.Location = new System.Drawing.Point(238, 380);
            this.checkBoxIncludeUnitTests.Name = "checkBoxIncludeUnitTests";
            this.checkBoxIncludeUnitTests.Size = new System.Drawing.Size(208, 23);
            this.checkBoxIncludeUnitTests.TabIndex = 104;
            this.checkBoxIncludeUnitTests.Text = "Include ViewModel unit tests";
            this.checkBoxIncludeUnitTests.UseVisualStyleBackColor = true;
            this.checkBoxIncludeUnitTests.Visible = false;
            // 
            // checkBoxUseNuget
            // 
            this.checkBoxUseNuget.AutoSize = true;
            this.checkBoxUseNuget.Location = new System.Drawing.Point(238, 282);
            this.checkBoxUseNuget.Name = "checkBoxUseNuget";
            this.checkBoxUseNuget.Size = new System.Drawing.Size(283, 23);
            this.checkBoxUseNuget.TabIndex = 105;
            this.checkBoxUseNuget.Text = "Use Nuget (Internet connection required)";
            this.checkBoxUseNuget.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mvxListView1);
            this.panel1.Location = new System.Drawing.Point(239, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 224);
            this.panel1.TabIndex = 106;
            // 
            // ServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 480);
            this.Controls.Add(this.panel1);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServicesForm";
            this.Text = "Ninja - Add Services";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxViewModel;
        private UserControls.Logo logo1;
        private System.Windows.Forms.Label lblSelectTheRequiredConverters;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private UserControls.MvxListView mvxListView1;
        private System.Windows.Forms.CheckBox checkBoxIncludeUnitTests;
        private System.Windows.Forms.CheckBox checkBoxUseNuget;
        private System.Windows.Forms.Panel panel1;
    }
}