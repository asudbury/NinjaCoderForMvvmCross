namespace NinjaCoder.MvvmCross.Views
{
    partial class ViewModelViewsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewModelViewsForm));
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxViewModel = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.lblRequiredViews = new System.Windows.Forms.Label();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.checkBoxIncludeUnitTests = new System.Windows.Forms.CheckBox();
            this.labelInitViewModel = new System.Windows.Forms.Label();
            this.comboBoxInitViewModel = new System.Windows.Forms.ComboBox();
            this.labelNavigateToViewModel = new System.Windows.Forms.Label();
            this.comboBoxNavigateToViewModel = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "View Model Name";
            // 
            // textBoxViewModel
            // 
            this.textBoxViewModel.Location = new System.Drawing.Point(252, 48);
            this.textBoxViewModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxViewModel.Name = "textBoxViewModel";
            this.textBoxViewModel.Size = new System.Drawing.Size(148, 25);
            this.textBoxViewModel.TabIndex = 1;
            this.textBoxViewModel.Text = "ViewModel";
            this.textBoxViewModel.TextChanged += new System.EventHandler(this.TextBoxViewModelTextChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(324, 479);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 29);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(418, 479);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 29);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // lblRequiredViews
            // 
            this.lblRequiredViews.Location = new System.Drawing.Point(248, 90);
            this.lblRequiredViews.Name = "lblRequiredViews";
            this.lblRequiredViews.Size = new System.Drawing.Size(130, 22);
            this.lblRequiredViews.TabIndex = 2;
            this.lblRequiredViews.Text = "Required Views";
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(9, 7);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(234, 153);
            this.mvxListView1.TabIndex = 3;
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(24, 27);
            this.logo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(195, 415);
            this.logo1.TabIndex = 7;
            // 
            // checkBoxIncludeUnitTests
            // 
            this.checkBoxIncludeUnitTests.AutoSize = true;
            this.checkBoxIncludeUnitTests.Checked = true;
            this.checkBoxIncludeUnitTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeUnitTests.Location = new System.Drawing.Point(252, 294);
            this.checkBoxIncludeUnitTests.Name = "checkBoxIncludeUnitTests";
            this.checkBoxIncludeUnitTests.Size = new System.Drawing.Size(225, 23);
            this.checkBoxIncludeUnitTests.TabIndex = 98;
            this.checkBoxIncludeUnitTests.Text = "Create ViewModel unit tests file";
            this.checkBoxIncludeUnitTests.UseVisualStyleBackColor = true;
            // 
            // labelInitViewModel
            // 
            this.labelInitViewModel.AutoSize = true;
            this.labelInitViewModel.Location = new System.Drawing.Point(247, 334);
            this.labelInitViewModel.Name = "labelInitViewModel";
            this.labelInitViewModel.Size = new System.Drawing.Size(209, 19);
            this.labelInitViewModel.TabIndex = 105;
            this.labelInitViewModel.Text = "ViewModel will be initiated from ";
            // 
            // comboBoxInitViewModel
            // 
            this.comboBoxInitViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInitViewModel.FormattingEnabled = true;
            this.comboBoxInitViewModel.Location = new System.Drawing.Point(248, 356);
            this.comboBoxInitViewModel.Name = "comboBoxInitViewModel";
            this.comboBoxInitViewModel.Size = new System.Drawing.Size(258, 25);
            this.comboBoxInitViewModel.TabIndex = 104;
            // 
            // labelNavigateToViewModel
            // 
            this.labelNavigateToViewModel.AutoSize = true;
            this.labelNavigateToViewModel.Location = new System.Drawing.Point(246, 394);
            this.labelNavigateToViewModel.Name = "labelNavigateToViewModel";
            this.labelNavigateToViewModel.Size = new System.Drawing.Size(173, 19);
            this.labelNavigateToViewModel.TabIndex = 107;
            this.labelNavigateToViewModel.Text = "ViewModel will navigate to";
            // 
            // comboBoxNavigateToViewModel
            // 
            this.comboBoxNavigateToViewModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNavigateToViewModel.FormattingEnabled = true;
            this.comboBoxNavigateToViewModel.Location = new System.Drawing.Point(246, 417);
            this.comboBoxNavigateToViewModel.Name = "comboBoxNavigateToViewModel";
            this.comboBoxNavigateToViewModel.Size = new System.Drawing.Size(260, 25);
            this.comboBoxNavigateToViewModel.TabIndex = 106;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mvxListView1);
            this.panel1.Location = new System.Drawing.Point(250, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 173);
            this.panel1.TabIndex = 108;
            // 
            // ViewModelViewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 530);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelNavigateToViewModel);
            this.Controls.Add(this.comboBoxNavigateToViewModel);
            this.Controls.Add(this.labelInitViewModel);
            this.Controls.Add(this.comboBoxInitViewModel);
            this.Controls.Add(this.checkBoxIncludeUnitTests);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblRequiredViews);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxViewModel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewModelViewsForm";
            this.Text = "Ninja - Add View Model & Views";
            this.Load += new System.EventHandler(this.ViewModelOptionsViewLoad);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxViewModel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label lblRequiredViews;
        private UserControls.MvxListView mvxListView1;
        private UserControls.Logo logo1;
        private System.Windows.Forms.CheckBox checkBoxIncludeUnitTests;
        private System.Windows.Forms.Label labelInitViewModel;
        private System.Windows.Forms.ComboBox comboBoxInitViewModel;
        private System.Windows.Forms.Label labelNavigateToViewModel;
        private System.Windows.Forms.ComboBox comboBoxNavigateToViewModel;
        private System.Windows.Forms.Panel panel1;
    }
}