namespace BudgetUI
{
    partial class BudgetMainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetMainUI));
            this.tabControlBudgetFiles = new System.Windows.Forms.TabControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.budgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateNextPaycheckPeriodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPaycheckPeriodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPreviousPaycheckPeriod = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRecalculatePaycheckPeriod = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPaycheckPeriod = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonEditCategories = new System.Windows.Forms.ToolStripButton();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlBudgetFiles
            // 
            this.tabControlBudgetFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBudgetFiles.Location = new System.Drawing.Point(0, 0);
            this.tabControlBudgetFiles.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlBudgetFiles.Name = "tabControlBudgetFiles";
            this.tabControlBudgetFiles.SelectedIndex = 0;
            this.tabControlBudgetFiles.Size = new System.Drawing.Size(1116, 718);
            this.tabControlBudgetFiles.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.budgetToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1116, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeFileToolStripMenuItem
            // 
            this.closeFileToolStripMenuItem.Name = "closeFileToolStripMenuItem";
            this.closeFileToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.closeFileToolStripMenuItem.Text = "Close file";
            this.closeFileToolStripMenuItem.Click += new System.EventHandler(this.closeFileToolStripMenuItem_Click);
            // 
            // budgetToolStripMenuItem
            // 
            this.budgetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateNextPaycheckPeriodToolStripMenuItem,
            this.previousPaycheckPeriodToolStripMenuItem,
            this.editCategoriesToolStripMenuItem,
            this.exportToCsvToolStripMenuItem});
            this.budgetToolStripMenuItem.Name = "budgetToolStripMenuItem";
            this.budgetToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.budgetToolStripMenuItem.Text = "Budget";
            // 
            // generateNextPaycheckPeriodToolStripMenuItem
            // 
            this.generateNextPaycheckPeriodToolStripMenuItem.Name = "generateNextPaycheckPeriodToolStripMenuItem";
            this.generateNextPaycheckPeriodToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.generateNextPaycheckPeriodToolStripMenuItem.Text = "Next paycheck period";
            this.generateNextPaycheckPeriodToolStripMenuItem.Click += new System.EventHandler(this.generateNextPaycheckPeriodToolStripMenuItem_Click);
            // 
            // previousPaycheckPeriodToolStripMenuItem
            // 
            this.previousPaycheckPeriodToolStripMenuItem.Name = "previousPaycheckPeriodToolStripMenuItem";
            this.previousPaycheckPeriodToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.previousPaycheckPeriodToolStripMenuItem.Text = "Previous paycheck period";
            this.previousPaycheckPeriodToolStripMenuItem.Click += new System.EventHandler(this.previousPaycheckPeriodToolStripMenuItem_Click);
            // 
            // editCategoriesToolStripMenuItem
            // 
            this.editCategoriesToolStripMenuItem.Name = "editCategoriesToolStripMenuItem";
            this.editCategoriesToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.editCategoriesToolStripMenuItem.Text = "Edit Categories...";
            // 
            // exportToCsvToolStripMenuItem
            // 
            this.exportToCsvToolStripMenuItem.Name = "exportToCsvToolStripMenuItem";
            this.exportToCsvToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.exportToCsvToolStripMenuItem.Text = "Export to csv...";
            this.exportToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportToCsvToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveFile,
            this.toolStripButtonOpenFile,
            this.toolStripSeparator1,
            this.toolStripButtonPreviousPaycheckPeriod,
            this.toolStripButtonRecalculatePaycheckPeriod,
            this.toolStripButtonNextPaycheckPeriod,
            this.toolStripSeparator2,
            this.toolStripButtonEditCategories});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1116, 27);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonSaveFile
            // 
            this.toolStripButtonSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveFile.Enabled = false;
            this.toolStripButtonSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveFile.Image")));
            this.toolStripButtonSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveFile.Name = "toolStripButtonSaveFile";
            this.toolStripButtonSaveFile.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSaveFile.Text = "Save budget file...";
            this.toolStripButtonSaveFile.Click += new System.EventHandler(this.toolStripButtonSaveFile_Click);
            // 
            // toolStripButtonOpenFile
            // 
            this.toolStripButtonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenFile.Image")));
            this.toolStripButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFile.Name = "toolStripButtonOpenFile";
            this.toolStripButtonOpenFile.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonOpenFile.Text = "Open budget file...";
            this.toolStripButtonOpenFile.Click += new System.EventHandler(this.toolStripButtonOpenFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonPreviousPaycheckPeriod
            // 
            this.toolStripButtonPreviousPaycheckPeriod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreviousPaycheckPeriod.Enabled = false;
            this.toolStripButtonPreviousPaycheckPeriod.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPreviousPaycheckPeriod.Image")));
            this.toolStripButtonPreviousPaycheckPeriod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousPaycheckPeriod.Name = "toolStripButtonPreviousPaycheckPeriod";
            this.toolStripButtonPreviousPaycheckPeriod.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonPreviousPaycheckPeriod.Text = "Open previous budget file";
            this.toolStripButtonPreviousPaycheckPeriod.Click += new System.EventHandler(this.toolStripButtonPreviousPaycheckPeriod_Click);
            // 
            // toolStripButtonRecalculatePaycheckPeriod
            // 
            this.toolStripButtonRecalculatePaycheckPeriod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRecalculatePaycheckPeriod.Enabled = false;
            this.toolStripButtonRecalculatePaycheckPeriod.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecalculatePaycheckPeriod.Image")));
            this.toolStripButtonRecalculatePaycheckPeriod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRecalculatePaycheckPeriod.Name = "toolStripButtonRecalculatePaycheckPeriod";
            this.toolStripButtonRecalculatePaycheckPeriod.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonRecalculatePaycheckPeriod.Text = "Recalculate starting budget";
            this.toolStripButtonRecalculatePaycheckPeriod.Click += new System.EventHandler(this.toolStripButtonRecalculatePaycheckPeriod_Click);
            // 
            // toolStripButtonNextPaycheckPeriod
            // 
            this.toolStripButtonNextPaycheckPeriod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextPaycheckPeriod.Enabled = false;
            this.toolStripButtonNextPaycheckPeriod.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextPaycheckPeriod.Image")));
            this.toolStripButtonNextPaycheckPeriod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPaycheckPeriod.Name = "toolStripButtonNextPaycheckPeriod";
            this.toolStripButtonNextPaycheckPeriod.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonNextPaycheckPeriod.Text = "Open next budget file";
            this.toolStripButtonNextPaycheckPeriod.Click += new System.EventHandler(this.toolStripButtonNextPaycheckPeriod_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonEditCategories
            // 
            this.toolStripButtonEditCategories.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditCategories.Enabled = false;
            this.toolStripButtonEditCategories.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditCategories.Image")));
            this.toolStripButtonEditCategories.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditCategories.Name = "toolStripButtonEditCategories";
            this.toolStripButtonEditCategories.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditCategories.Text = "Edit categories...";
            this.toolStripButtonEditCategories.Click += new System.EventHandler(this.toolStripButtonEditCategories_Click);
            // 
            // panelBackground
            // 
            this.panelBackground.Controls.Add(this.tabControlBudgetFiles);
            this.panelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackground.Location = new System.Drawing.Point(0, 55);
            this.panelBackground.Margin = new System.Windows.Forms.Padding(4);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(1116, 718);
            this.panelBackground.TabIndex = 3;
            // 
            // BudgetMainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 773);
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BudgetMainUI";
            this.Text = "Budget";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panelBackground.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlBudgetFiles;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem budgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateNextPaycheckPeriodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousPaycheckPeriodToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousPaycheckPeriod;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPaycheckPeriod;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecalculatePaycheckPeriod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.ToolStripMenuItem editCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditCategories;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem closeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCsvToolStripMenuItem;
    }
}

