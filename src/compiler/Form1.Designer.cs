namespace compiler
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.SourceBox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.logMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.logMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 5);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1059, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(177, 24);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.saveToolStripMenuItem.Text = "Save as";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem,
            this.runToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.runToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.runToolStripMenuItem.Text = "Compile && Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // openFileDlg
            // 
            this.openFileDlg.DefaultExt = "ps";
            this.openFileDlg.Filter = "PySharp Files (*.ps) |*.ps";
            // 
            // logListBox
            // 
            this.logListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logListBox.FormattingEnabled = true;
            this.logListBox.ItemHeight = 16;
            this.logListBox.Location = new System.Drawing.Point(5, 496);
            this.logListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(1059, 180);
            this.logListBox.TabIndex = 2;
            this.logListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.logListBox_MouseDoubleClick);
            this.logListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.logListBox_MouseUp);
            // 
            // SourceBox
            // 
            this.SourceBox.AcceptsTab = true;
            this.SourceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceBox.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SourceBox.HideSelection = false;
            this.SourceBox.Location = new System.Drawing.Point(5, 33);
            this.SourceBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.SourceBox.Size = new System.Drawing.Size(1059, 463);
            this.SourceBox.TabIndex = 1;
            this.SourceBox.Text = "";
            this.SourceBox.WordWrap = false;
            this.SourceBox.TextChanged += new System.EventHandler(this.SourceBox_TextChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "PySharp Files (*.ps) |*.ps";
            // 
            // logMenuStrip
            // 
            this.logMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem});
            this.logMenuStrip.Name = "logMenuStrip";
            this.logMenuStrip.Size = new System.Drawing.Size(139, 28);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.clearLogToolStripMenuItem.Text = "Clear log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 681);
            this.Controls.Add(this.SourceBox);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Text = "Py# Compiler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.logMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
		private System.Windows.Forms.ListBox logListBox;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
		private System.Windows.Forms.RichTextBox SourceBox;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip logMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
    }
}

