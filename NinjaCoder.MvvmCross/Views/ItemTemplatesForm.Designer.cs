namespace NinjaCoder.MvvmCross.Views
{
    partial class ItemTemplatesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemTemplatesForm));
            this.lblSelectTheRequiredConverters = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.mvxListView1 = new NinjaCoder.MvvmCross.UserControls.MvxListView();
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.SuspendLayout();
            // 
            // lblSelectTheRequiredConverters
            // 
            this.lblSelectTheRequiredConverters.Location = new System.Drawing.Point(325, 14);
            this.lblSelectTheRequiredConverters.Name = "lblSelectTheRequiredConverters";
            this.lblSelectTheRequiredConverters.Size = new System.Drawing.Size(272, 18);
            this.lblSelectTheRequiredConverters.TabIndex = 0;
            this.lblSelectTheRequiredConverters.Text = "Select the required converters";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonHelp.Location = new System.Drawing.Point(153, 418);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(100, 27);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "&Help";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(516, 363);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(624, 363);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // mvxListView1
            // 
            this.mvxListView1.Location = new System.Drawing.Point(328, 46);
            this.mvxListView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mvxListView1.Name = "mvxListView1";
            this.mvxListView1.Size = new System.Drawing.Size(396, 263);
            this.mvxListView1.TabIndex = 1;
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(14, 14);
            this.logo1.Margin = new System.Windows.Forms.Padding(5);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(278, 376);
            this.logo1.TabIndex = 5;
            // 
            // ItemTemplatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 416);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.mvxListView1);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.lblSelectTheRequiredConverters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemTemplatesForm";
            this.Text = "Ninja Coder for MvvmCross - Converters";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSelectTheRequiredConverters;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private UserControls.MvxListView mvxListView1;
        private UserControls.Logo logo1;
    }
}