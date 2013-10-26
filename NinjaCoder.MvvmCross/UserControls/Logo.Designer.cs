namespace NinjaCoder.MvvmCross.UserControls
{
    partial class Logo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logo));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelMvvmCross = new System.Windows.Forms.Label();
            this.labelBuildDateTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(48, 13);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(179, 125);
            this.pictureBox2.TabIndex = 87;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(39, 142);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 135);
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Location = new System.Drawing.Point(39, 291);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(58, 20);
            this.labelVersion.TabIndex = 88;
            this.labelVersion.Text = "Version";
            // 
            // labelMvvmCross
            // 
            this.labelMvvmCross.AutoSize = true;
            this.labelMvvmCross.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMvvmCross.ForeColor = System.Drawing.Color.Gray;
            this.labelMvvmCross.Location = new System.Drawing.Point(40, 314);
            this.labelMvvmCross.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMvvmCross.Name = "labelMvvmCross";
            this.labelMvvmCross.Size = new System.Drawing.Size(84, 20);
            this.labelMvvmCross.TabIndex = 89;
            this.labelMvvmCross.Text = "MvvmCross";
            // 
            // labelBuildDateTime
            // 
            this.labelBuildDateTime.AutoSize = true;
            this.labelBuildDateTime.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBuildDateTime.ForeColor = System.Drawing.Color.Gray;
            this.labelBuildDateTime.Location = new System.Drawing.Point(40, 339);
            this.labelBuildDateTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBuildDateTime.Name = "labelBuildDateTime";
            this.labelBuildDateTime.Size = new System.Drawing.Size(63, 17);
            this.labelBuildDateTime.TabIndex = 90;
            this.labelBuildDateTime.Text = "DateTime";
            // 
            // Logo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelBuildDateTime);
            this.Controls.Add(this.labelMvvmCross);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Logo";
            this.Size = new System.Drawing.Size(259, 389);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelMvvmCross;
        private System.Windows.Forms.Label labelBuildDateTime;
    }
}
