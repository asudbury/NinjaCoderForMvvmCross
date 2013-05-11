namespace NinjaCoder.MvvmCross.UI
{
    partial class FormMain
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addViewModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addConvertersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackOverflowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jabbRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 233);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(282, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(282, 28);
            this.menuStrip.TabIndex = 0;
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProjectsToolStripMenuItem,
            this.addViewModelToolStripMenuItem,
            this.addConvertersToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.stackOverflowToolStripMenuItem,
            this.jabbRToolStripMenuItem,
            this.gitHubToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // addProjectsToolStripMenuItem
            // 
            this.addProjectsToolStripMenuItem.Name = "addProjectsToolStripMenuItem";
            this.addProjectsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addProjectsToolStripMenuItem.Text = "Add Projects";
            this.addProjectsToolStripMenuItem.Click += new System.EventHandler(this.AddProjectsToolStripMenuItem_Click);
            // 
            // addViewModelToolStripMenuItem
            // 
            this.addViewModelToolStripMenuItem.Name = "addViewModelToolStripMenuItem";
            this.addViewModelToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addViewModelToolStripMenuItem.Text = "Add View Model";
            this.addViewModelToolStripMenuItem.Click += new System.EventHandler(this.AddViewModelToolStripMenuItem_Click);
            // 
            // addConvertersToolStripMenuItem
            // 
            this.addConvertersToolStripMenuItem.Name = "addConvertersToolStripMenuItem";
            this.addConvertersToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addConvertersToolStripMenuItem.Text = "Add Converters";
            this.addConvertersToolStripMenuItem.Click += new System.EventHandler(this.AddConvertersToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // stackOverflowToolStripMenuItem
            // 
            this.stackOverflowToolStripMenuItem.Name = "stackOverflowToolStripMenuItem";
            this.stackOverflowToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.stackOverflowToolStripMenuItem.Text = "Stack Overflow";
            this.stackOverflowToolStripMenuItem.Click += new System.EventHandler(this.StackOverflowToolStripMenuItemClick);
            // 
            // jabbRToolStripMenuItem
            // 
            this.jabbRToolStripMenuItem.Name = "jabbRToolStripMenuItem";
            this.jabbRToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.jabbRToolStripMenuItem.Text = "JabbR";
            this.jabbRToolStripMenuItem.Click += new System.EventHandler(this.JabbRToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // gitHubToolStripMenuItem
            // 
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.gitHubToolStripMenuItem.Text = "GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.GitHubToolStripMenuItemClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(282, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // lblThisApplicationNeedsToBeRunAsAnAdminstrator
            // 
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.Location = new System.Drawing.Point(5, 102);
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.Name = "lblThisApplicationNeedsToBeRunAsAnAdminstrator";
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.Size = new System.Drawing.Size(277, 66);
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.TabIndex = 2;
            this.lblThisApplicationNeedsToBeRunAsAnAdminstrator.Text = "This application needs to be run as an adminstrator";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.lblThisApplicationNeedsToBeRunAsAnAdminstrator);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Ninja Coder for MvvmCross ";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addViewModelToolStripMenuItem;
        private System.Windows.Forms.Label lblThisApplicationNeedsToBeRunAsAnAdminstrator;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addConvertersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stackOverflowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jabbRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitHubToolStripMenuItem;
    }
}

