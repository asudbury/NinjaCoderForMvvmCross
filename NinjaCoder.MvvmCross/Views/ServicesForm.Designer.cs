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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 17);
            this.label1.TabIndex = 103;
            this.label1.Text = "Select the ViewModel to implement services or leave blank";
            // 
            // comboBoxViewModel
            // 
            this.comboBoxViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewModel.FormattingEnabled = true;
            this.comboBoxViewModel.Location = new System.Drawing.Point(331, 482);
            this.comboBoxViewModel.Name = "comboBoxViewModel";
            this.comboBoxViewModel.Size = new System.Drawing.Size(210, 24);
            this.comboBoxViewModel.TabIndex = 102;
            this.comboBoxViewModel.SelectedValueChanged += new System.EventHandler(this.ComboBoxViewModelSelectedValueChanged);
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(26, 29);
            this.logo1.Margin = new System.Windows.Forms.Padding(5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(282, 376);
            this.logo1.TabIndex = 101;
            // 
            // lblSelectTheRequiredConverters
            // 
            this.lblSelectTheRequiredConverters.Location = new System.Drawing.Point(328, 26);
            this.lblSelectTheRequiredConverters.Name = "lblSelectTheRequiredConverters";
            this.lblSelectTheRequiredConverters.Size = new System.Drawing.Size(272, 18);
            this.lblSelectTheRequiredConverters.TabIndex = 100;
            this.lblSelectTheRequiredConverters.Text = "Select the required services";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(547, 536);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 98;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(655, 536);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 99;
            this.buttonCancel.Text = "&Cancel";
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(332, 47);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(423, 387);
            this.mvxListView1.TabIndex = 97;
            // 
            // checkBoxIncludeUnitTests
            // 
            this.checkBoxIncludeUnitTests.AutoSize = true;
            this.checkBoxIncludeUnitTests.Checked = true;
            this.checkBoxIncludeUnitTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeUnitTests.Location = new System.Drawing.Point(331, 512);
            this.checkBoxIncludeUnitTests.Name = "checkBoxIncludeUnitTests";
            this.checkBoxIncludeUnitTests.Size = new System.Drawing.Size(207, 21);
            this.checkBoxIncludeUnitTests.TabIndex = 104;
            this.checkBoxIncludeUnitTests.Text = "Include ViewModel unit tests";
            this.checkBoxIncludeUnitTests.UseVisualStyleBackColor = true;
            this.checkBoxIncludeUnitTests.Visible = false;
            // 
            // ServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 596);
            this.Controls.Add(this.checkBoxIncludeUnitTests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxViewModel);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblSelectTheRequiredConverters);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.mvxListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServicesForm";
            this.Text = "Ninja Coder for MvvmCross - Add Services";
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
    }
}