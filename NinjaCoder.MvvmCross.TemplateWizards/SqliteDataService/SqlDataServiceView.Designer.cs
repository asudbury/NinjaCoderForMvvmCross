namespace NinjaCoder.MvvmCross.TemplateWizards.SqliteDataService
{
    partial class SqlDataServiceView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlDataServiceView));
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEntityName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxDataServiceInterface = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxDataService = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Entity Name";
            // 
            // textBoxEntityName
            // 
            this.textBoxEntityName.Location = new System.Drawing.Point(309, 191);
            this.textBoxEntityName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEntityName.Name = "textBoxEntityName";
            this.textBoxEntityName.Size = new System.Drawing.Size(282, 22);
            this.textBoxEntityName.TabIndex = 0;
            this.textBoxEntityName.TextChanged += new System.EventHandler(this.TextBoxEntityNameTextChanged);

            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(384, 363);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(492, 363);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "&Cancel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Data Service Interface";
            // 
            // TextBoxDataServiceInterface
            // 
            this.TextBoxDataServiceInterface.Enabled = false;
            this.TextBoxDataServiceInterface.Location = new System.Drawing.Point(309, 259);
            this.TextBoxDataServiceInterface.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxDataServiceInterface.Name = "TextBoxDataServiceInterface";
            this.TextBoxDataServiceInterface.ReadOnly = true;
            this.TextBoxDataServiceInterface.Size = new System.Drawing.Size(282, 22);
            this.TextBoxDataServiceInterface.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Data Service";
            // 
            // TextBoxDataService
            // 
            this.TextBoxDataService.Enabled = false;
            this.TextBoxDataService.Location = new System.Drawing.Point(309, 322);
            this.TextBoxDataService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxDataService.Name = "TextBoxDataService";
            this.TextBoxDataService.ReadOnly = true;
            this.TextBoxDataService.Size = new System.Drawing.Size(282, 22);
            this.TextBoxDataService.TabIndex = 17;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(309, 13);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(283, 142);
            this.textBox3.TabIndex = 18;
            this.textBox3.Text = "Please enter an Entity name below.\r\n\r\nThe skeleleton Entity class will be created" +
    " along with a data service and an interface to the dataservice.\r\n";
            // 
            // SqlDataServiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 417);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxDataService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxDataServiceInterface);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxEntityName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SqlDataServiceView";
            this.Text = "Ninja Code for MvvmCross | Sqlite Options";
            this.Load += new System.EventHandler(this.SqlDataServiceViewLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEntityName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxDataServiceInterface;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxDataService;
        private System.Windows.Forms.TextBox textBox3;
    }
}