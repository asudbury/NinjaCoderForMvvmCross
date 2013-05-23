namespace NinjaCoder.MvvmCross.Views
{
    partial class ViewModelOptionsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewModelOptionsView));
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxViewModel = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.lblRequiredViews = new System.Windows.Forms.Label();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "View Model Name";
            // 
            // textBoxViewModel
            // 
            this.textBoxViewModel.Location = new System.Drawing.Point(576, 22);
            this.textBoxViewModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxViewModel.Name = "textBoxViewModel";
            this.textBoxViewModel.Size = new System.Drawing.Size(248, 22);
            this.textBoxViewModel.TabIndex = 1;
            this.textBoxViewModel.Text = "ViewModel";
            this.textBoxViewModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxViewModelKeyDown);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(616, 414);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(724, 414);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // lblRequiredViews
            // 
            this.lblRequiredViews.Location = new System.Drawing.Point(576, 69);
            this.lblRequiredViews.Name = "lblRequiredViews";
            this.lblRequiredViews.Size = new System.Drawing.Size(149, 21);
            this.lblRequiredViews.TabIndex = 2;
            this.lblRequiredViews.Text = "Required Views";
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(575, 105);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(249, 272);
            this.mvxListView1.TabIndex = 3;
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(28, 22);
            this.logo1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(386, 391);
            this.logo1.TabIndex = 7;
            // 
            // ViewModelOptionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 455);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.mvxListView1);
            this.Controls.Add(this.lblRequiredViews);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxViewModel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewModelOptionsView";
            this.Text = "Ninja Coder for MvvmCross - Add View Model and Views";
            this.Load += new System.EventHandler(this.ViewModelOptionsViewLoad);
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
    }
}