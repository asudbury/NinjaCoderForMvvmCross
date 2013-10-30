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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addViewModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addConvertersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(230, 28);
            this.menuStrip.TabIndex = 0;
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProjectsToolStripMenuItem,
            this.addViewModelToolStripMenuItem,
            this.addConvertersToolStripMenuItem,
            this.addServicesToolStripMenuItem,
            this.addPluginsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.toolsToolStripMenuItem.Text = "File";
            // 
            // addProjectsToolStripMenuItem
            // 
            this.addProjectsToolStripMenuItem.Name = "addProjectsToolStripMenuItem";
            this.addProjectsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addProjectsToolStripMenuItem.Text = "Add Projects";
            this.addProjectsToolStripMenuItem.Click += new System.EventHandler(this.AddProjectsToolStripMenuItemClick);
            // 
            // addViewModelToolStripMenuItem
            // 
            this.addViewModelToolStripMenuItem.Name = "addViewModelToolStripMenuItem";
            this.addViewModelToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addViewModelToolStripMenuItem.Text = "Add View Model";
            this.addViewModelToolStripMenuItem.Click += new System.EventHandler(this.AddViewModelToolStripMenuItemClick);
            // 
            // addConvertersToolStripMenuItem
            // 
            this.addConvertersToolStripMenuItem.Name = "addConvertersToolStripMenuItem";
            this.addConvertersToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addConvertersToolStripMenuItem.Text = "Add Converters";
            this.addConvertersToolStripMenuItem.Click += new System.EventHandler(this.AddConvertersToolStripMenuItemClick);
            // 
            // addServicesToolStripMenuItem
            // 
            this.addServicesToolStripMenuItem.Name = "addServicesToolStripMenuItem";
            this.addServicesToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addServicesToolStripMenuItem.Text = "Add Services";
            this.addServicesToolStripMenuItem.Click += new System.EventHandler(this.AddServicesToolStripMenuItemClick);
            // 
            // addPluginsToolStripMenuItem
            // 
            this.addPluginsToolStripMenuItem.Name = "addPluginsToolStripMenuItem";
            this.addPluginsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addPluginsToolStripMenuItem.Text = "Add Plugins";
            this.addPluginsToolStripMenuItem.Click += new System.EventHandler(this.AddPluginsToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(230, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 53);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 246);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(9, 46);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(215, 241);
            this.treeView1.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 299);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Ninja Coder for MvvmCross";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addViewModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addConvertersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addServicesToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

