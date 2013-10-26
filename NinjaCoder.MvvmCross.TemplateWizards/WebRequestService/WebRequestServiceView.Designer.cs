namespace NinjaCoder.MvvmCross.TemplateWizards.WebRequestService
{
    partial class WebRequestServiceView
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
            this.logo1 = new NinjaCoder.MvvmCross.UserControls.Logo();
            this.TextBoxDataServiceInterface = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEntityName = new System.Windows.Forms.TextBox();
            this.TextBoxDataService = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxUseIEnumerable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.White;
            this.logo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logo1.Location = new System.Drawing.Point(13, 13);
            this.logo1.Margin = new System.Windows.Forms.Padding(4);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(273, 428);
            this.logo1.TabIndex = 12;
            // 
            // TextBoxDataServiceInterface
            // 
            this.TextBoxDataServiceInterface.Enabled = false;
            this.TextBoxDataServiceInterface.Location = new System.Drawing.Point(305, 266);
            this.TextBoxDataServiceInterface.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxDataServiceInterface.Name = "TextBoxDataServiceInterface";
            this.TextBoxDataServiceInterface.ReadOnly = true;
            this.TextBoxDataServiceInterface.Size = new System.Drawing.Size(282, 22);
            this.TextBoxDataServiceInterface.TabIndex = 24;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(378, 414);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(486, 414);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "&Cancel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Entity Name";
            // 
            // textBoxEntityName
            // 
            this.textBoxEntityName.Location = new System.Drawing.Point(304, 175);
            this.textBoxEntityName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEntityName.Name = "textBoxEntityName";
            this.textBoxEntityName.Size = new System.Drawing.Size(282, 22);
            this.textBoxEntityName.TabIndex = 19;
            this.textBoxEntityName.TextChanged += new System.EventHandler(this.TextBoxEntityNameTextChanged);
            // 
            // TextBoxDataService
            // 
            this.TextBoxDataService.Enabled = false;
            this.TextBoxDataService.Location = new System.Drawing.Point(305, 324);
            this.TextBoxDataService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxDataService.Name = "TextBoxDataService";
            this.TextBoxDataService.ReadOnly = true;
            this.TextBoxDataService.Size = new System.Drawing.Size(282, 22);
            this.TextBoxDataService.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Web Request Service Interface";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(304, 13);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(283, 129);
            this.textBox3.TabIndex = 27;
            this.textBox3.Text = "Please enter an Entity name below.\r\n\r\nThe skeleleton Entity class will be created" +
    " along with a web request service and an interface.\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Web Request Service";
            // 
            // checkBoxUseIEnumerable
            // 
            this.checkBoxUseIEnumerable.AutoSize = true;
            this.checkBoxUseIEnumerable.Checked = true;
            this.checkBoxUseIEnumerable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseIEnumerable.Location = new System.Drawing.Point(307, 203);
            this.checkBoxUseIEnumerable.Name = "checkBoxUseIEnumerable";
            this.checkBoxUseIEnumerable.Size = new System.Drawing.Size(255, 21);
            this.checkBoxUseIEnumerable.TabIndex = 28;
            this.checkBoxUseIEnumerable.Text = "Web Request will return a collection";
            this.checkBoxUseIEnumerable.UseVisualStyleBackColor = true;
            // 
            // WebRequestServiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 470);
            this.Controls.Add(this.checkBoxUseIEnumerable);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxDataService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxDataServiceInterface);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxEntityName);
            this.Controls.Add(this.logo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebRequestServiceView";
            this.Text = "Ninja Code for MvvmCross | WebRequest Options";
            this.Load += new System.EventHandler(this.WebRequestServiceViewLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Logo logo1;
        private System.Windows.Forms.TextBox TextBoxDataServiceInterface;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEntityName;
        private System.Windows.Forms.TextBox TextBoxDataService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxUseIEnumerable;
    }
}