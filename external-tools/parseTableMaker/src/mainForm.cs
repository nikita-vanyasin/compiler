using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace parserMaker
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	 public class mainForm : System.Windows.Forms.Form
	{	
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem file;
		private System.Windows.Forms.MenuItem open;
		private System.Windows.Forms.MenuItem exit;
		private System.Windows.Forms.MenuItem save;
		private System.Windows.Forms.MenuItem saveAs;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		string path;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem MakeStates;
		State states;
		ParsHead parsHead;
		ParsTable parsTable;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage Grammer;
		private System.Windows.Forms.RichTextBox content;
		private System.Windows.Forms.TabPage firstFollow;
		private System.Windows.Forms.ToolBarButton open1;
		private System.Windows.Forms.ToolBarButton save1;
		private System.Windows.Forms.ToolBarButton makePars;
		private System.Windows.Forms.ToolBarButton close1;
		private System.Windows.Forms.ComboBox comboxNonTerm;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton fristRadio;
		private System.Windows.Forms.RadioButton followRadio;
		private System.Windows.Forms.ListBox lstFirstFollow;
		private System.Windows.Forms.GroupBox groupBox2;
         private System.Windows.Forms.TabPage parsTablePage;
		private System.Windows.Forms.MenuItem New;
		private System.Windows.Forms.ToolBarButton new1;
		private System.Windows.Forms.ListBox result;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lawCnt;
		private System.Windows.Forms.Label nonTerminalCnt;
		private System.Windows.Forms.Label terminalCnt;
         private System.Windows.Forms.Label stateCnt;
		private System.Diagnostics.Process process1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem cut;
		private System.Windows.Forms.MenuItem copy;
		private System.Windows.Forms.MenuItem paste;
		private System.Windows.Forms.MenuItem selectAll;
		private System.ComponentModel.IContainer components;
		private Font nonTerminalFont;
		private Font terminalFont;
		private Color terminalColor;
		private System.Windows.Forms.MenuItem SaveParsTable;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGrid dgTabel;
         private MenuItem menuItem4;
         private MenuItem MenuOpenHelp;
         private MenuItem MenuAbout;
		private Color nonTerminalColor;
		public mainForm()
		{
			InitializeComponent();
			path="";
			nonTerminalFont=new Font("Arial",13,FontStyle.Bold);
			nonTerminalColor=Color.Black;

			terminalFont=new Font("Arial",13,FontStyle.Italic);
			terminalColor=Color.Blue;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.file = new System.Windows.Forms.MenuItem();
            this.New = new System.Windows.Forms.MenuItem();
            this.open = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.SaveParsTable = new System.Windows.Forms.MenuItem();
            this.save = new System.Windows.Forms.MenuItem();
            this.saveAs = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.exit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.cut = new System.Windows.Forms.MenuItem();
            this.copy = new System.Windows.Forms.MenuItem();
            this.paste = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.selectAll = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MakeStates = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.new1 = new System.Windows.Forms.ToolBarButton();
            this.open1 = new System.Windows.Forms.ToolBarButton();
            this.save1 = new System.Windows.Forms.ToolBarButton();
            this.makePars = new System.Windows.Forms.ToolBarButton();
            this.close1 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Grammer = new System.Windows.Forms.TabPage();
            this.content = new System.Windows.Forms.RichTextBox();
            this.firstFollow = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.stateCnt = new System.Windows.Forms.Label();
            this.lawCnt = new System.Windows.Forms.Label();
            this.nonTerminalCnt = new System.Windows.Forms.Label();
            this.terminalCnt = new System.Windows.Forms.Label();
            this.lstFirstFollow = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.followRadio = new System.Windows.Forms.RadioButton();
            this.fristRadio = new System.Windows.Forms.RadioButton();
            this.comboxNonTerm = new System.Windows.Forms.ComboBox();
            this.parsTablePage = new System.Windows.Forms.TabPage();
            this.result = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgTabel = new System.Windows.Forms.DataGrid();
            this.process1 = new System.Diagnostics.Process();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.MenuOpenHelp = new System.Windows.Forms.MenuItem();
            this.MenuAbout = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Grammer.SuspendLayout();
            this.firstFollow.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.parsTablePage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTabel)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.file,
            this.menuItem2,
            this.menuItem1,
            this.menuItem4});
            // 
            // file
            // 
            this.file.Index = 0;
            this.file.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.New,
            this.open,
            this.menuItem5,
            this.SaveParsTable,
            this.save,
            this.saveAs,
            this.menuItem3,
            this.exit});
            this.file.Text = "File";
            // 
            // New
            // 
            this.New.Index = 0;
            this.New.Text = "&New";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // open
            // 
            this.open.Index = 1;
            this.open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.open.Text = "&Open";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "-";
            // 
            // SaveParsTable
            // 
            this.SaveParsTable.Index = 3;
            this.SaveParsTable.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.SaveParsTable.Text = "Save Pars Table";
            this.SaveParsTable.Click += new System.EventHandler(this.SaveParsTable_Click);
            // 
            // save
            // 
            this.save.Enabled = false;
            this.save.Index = 4;
            this.save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.save.Text = "&Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // saveAs
            // 
            this.saveAs.Index = 5;
            this.saveAs.Text = "Save &As";
            this.saveAs.Click += new System.EventHandler(this.saveAs_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Text = "-";
            // 
            // exit
            // 
            this.exit.Index = 7;
            this.exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.exit.Text = "E&xit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.cut,
            this.copy,
            this.paste,
            this.menuItem13,
            this.selectAll});
            this.menuItem2.Text = "&Edit";
            // 
            // cut
            // 
            this.cut.Enabled = false;
            this.cut.Index = 0;
            this.cut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.cut.Text = "&Cut";
            this.cut.Click += new System.EventHandler(this.cut_Click);
            // 
            // copy
            // 
            this.copy.DefaultItem = true;
            this.copy.Enabled = false;
            this.copy.Index = 1;
            this.copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.copy.Text = "&Copy";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // paste
            // 
            this.paste.Enabled = false;
            this.paste.Index = 2;
            this.paste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.paste.Text = "&Paste";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 3;
            this.menuItem13.Text = "-";
            // 
            // selectAll
            // 
            this.selectAll.Index = 4;
            this.selectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.selectAll.Text = "Select &All";
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MakeStates});
            this.menuItem1.Text = "Actions";
            // 
            // MakeStates
            // 
            this.MakeStates.Enabled = false;
            this.MakeStates.Index = 0;
            this.MakeStates.Text = "Make &Parse Table";
            this.MakeStates.Click += new System.EventHandler(this.MakeStates_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.new1,
            this.open1,
            this.save1,
            this.makePars,
            this.close1});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(712, 44);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // new1
            // 
            this.new1.ImageIndex = 0;
            this.new1.Name = "new1";
            this.new1.Text = "New";
            this.new1.ToolTipText = "New Grammar";
            // 
            // open1
            // 
            this.open1.ImageIndex = 1;
            this.open1.Name = "open1";
            this.open1.Text = "Open";
            this.open1.ToolTipText = "Open a new Grammar for pars";
            // 
            // save1
            // 
            this.save1.Enabled = false;
            this.save1.ImageIndex = 2;
            this.save1.Name = "save1";
            this.save1.Text = "Save";
            this.save1.ToolTipText = "Save this Grammar";
            // 
            // makePars
            // 
            this.makePars.Enabled = false;
            this.makePars.ImageIndex = 3;
            this.makePars.Name = "makePars";
            this.makePars.Text = "Make";
            this.makePars.ToolTipText = "Make Pars Table by SLR method";
            // 
            // close1
            // 
            this.close1.ImageIndex = 5;
            this.close1.Name = "close1";
            this.close1.Text = "Close";
            this.close1.ToolTipText = "Close the Program";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 509);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Grammer);
            this.tabControl1.Controls.Add(this.firstFollow);
            this.tabControl1.Controls.Add(this.parsTablePage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 509);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Grammer
            // 
            this.Grammer.Controls.Add(this.content);
            this.Grammer.Location = new System.Drawing.Point(4, 22);
            this.Grammer.Name = "Grammer";
            this.Grammer.Size = new System.Drawing.Size(704, 483);
            this.Grammer.TabIndex = 0;
            this.Grammer.Text = "Grammar";
            // 
            // content
            // 
            this.content.DetectUrls = false;
            this.content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.content.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content.Location = new System.Drawing.Point(0, 0);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(704, 483);
            this.content.TabIndex = 1;
            this.content.Text = "";
            this.content.WordWrap = false;
            this.content.SelectionChanged += new System.EventHandler(this.content_SelectionChanged);
            this.content.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // firstFollow
            // 
            this.firstFollow.Controls.Add(this.groupBox2);
            this.firstFollow.Location = new System.Drawing.Point(4, 22);
            this.firstFollow.Name = "firstFollow";
            this.firstFollow.Size = new System.Drawing.Size(704, 483);
            this.firstFollow.TabIndex = 1;
            this.firstFollow.Text = "First &  Follow";
            this.firstFollow.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.lstFirstFollow);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.comboxNonTerm);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 483);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.stateCnt);
            this.groupBox3.Controls.Add(this.lawCnt);
            this.groupBox3.Controls.Add(this.nonTerminalCnt);
            this.groupBox3.Controls.Add(this.terminalCnt);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(48, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 160);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details";
            // 
            // stateCnt
            // 
            this.stateCnt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stateCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateCnt.Location = new System.Drawing.Point(20, 120);
            this.stateCnt.Name = "stateCnt";
            this.stateCnt.Size = new System.Drawing.Size(268, 24);
            this.stateCnt.TabIndex = 3;
            // 
            // lawCnt
            // 
            this.lawCnt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lawCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lawCnt.Location = new System.Drawing.Point(24, 88);
            this.lawCnt.Name = "lawCnt";
            this.lawCnt.Size = new System.Drawing.Size(264, 24);
            this.lawCnt.TabIndex = 2;
            // 
            // nonTerminalCnt
            // 
            this.nonTerminalCnt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.nonTerminalCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nonTerminalCnt.Location = new System.Drawing.Point(24, 56);
            this.nonTerminalCnt.Name = "nonTerminalCnt";
            this.nonTerminalCnt.Size = new System.Drawing.Size(264, 24);
            this.nonTerminalCnt.TabIndex = 1;
            // 
            // terminalCnt
            // 
            this.terminalCnt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.terminalCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terminalCnt.Location = new System.Drawing.Point(24, 24);
            this.terminalCnt.Name = "terminalCnt";
            this.terminalCnt.Size = new System.Drawing.Size(264, 24);
            this.terminalCnt.TabIndex = 0;
            // 
            // lstFirstFollow
            // 
            this.lstFirstFollow.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFirstFollow.ItemHeight = 25;
            this.lstFirstFollow.Location = new System.Drawing.Point(360, 40);
            this.lstFirstFollow.Name = "lstFirstFollow";
            this.lstFirstFollow.Size = new System.Drawing.Size(200, 354);
            this.lstFirstFollow.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.followRadio);
            this.groupBox1.Controls.Add(this.fristRadio);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(48, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First or Follow";
            // 
            // followRadio
            // 
            this.followRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.followRadio.Location = new System.Drawing.Point(16, 56);
            this.followRadio.Name = "followRadio";
            this.followRadio.Size = new System.Drawing.Size(56, 24);
            this.followRadio.TabIndex = 1;
            this.followRadio.Text = "Follow";
            // 
            // fristRadio
            // 
            this.fristRadio.Checked = true;
            this.fristRadio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fristRadio.Location = new System.Drawing.Point(16, 24);
            this.fristRadio.Name = "fristRadio";
            this.fristRadio.Size = new System.Drawing.Size(80, 24);
            this.fristRadio.TabIndex = 0;
            this.fristRadio.TabStop = true;
            this.fristRadio.Text = "First";
            this.fristRadio.CheckedChanged += new System.EventHandler(this.fristRadio_CheckedChanged);
            // 
            // comboxNonTerm
            // 
            this.comboxNonTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxNonTerm.Location = new System.Drawing.Point(176, 72);
            this.comboxNonTerm.Name = "comboxNonTerm";
            this.comboxNonTerm.Size = new System.Drawing.Size(152, 21);
            this.comboxNonTerm.TabIndex = 0;
            this.comboxNonTerm.SelectedIndexChanged += new System.EventHandler(this.comboxNonTerm_SelectedIndexChanged);
            // 
            // parsTablePage
            // 
            this.parsTablePage.Controls.Add(this.result);
            this.parsTablePage.Location = new System.Drawing.Point(4, 22);
            this.parsTablePage.Name = "parsTablePage";
            this.parsTablePage.Size = new System.Drawing.Size(704, 483);
            this.parsTablePage.TabIndex = 2;
            this.parsTablePage.Text = "Pars Table";
            // 
            // result
            // 
            this.result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.HorizontalScrollbar = true;
            this.result.ItemHeight = 25;
            this.result.Location = new System.Drawing.Point(0, 0);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(704, 479);
            this.result.TabIndex = 0;
            this.result.SelectedIndexChanged += new System.EventHandler(this.result_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgTabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(704, 483);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Grid";
            // 
            // dgTabel
            // 
            this.dgTabel.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgTabel.BackColor = System.Drawing.Color.Gainsboro;
            this.dgTabel.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgTabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgTabel.CaptionBackColor = System.Drawing.Color.DarkKhaki;
            this.dgTabel.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dgTabel.CaptionForeColor = System.Drawing.Color.Black;
            this.dgTabel.DataMember = "";
            this.dgTabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTabel.FlatMode = true;
            this.dgTabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dgTabel.ForeColor = System.Drawing.Color.Black;
            this.dgTabel.GridLineColor = System.Drawing.Color.Silver;
            this.dgTabel.HeaderBackColor = System.Drawing.Color.Black;
            this.dgTabel.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dgTabel.HeaderForeColor = System.Drawing.Color.White;
            this.dgTabel.LinkColor = System.Drawing.Color.DarkSlateBlue;
            this.dgTabel.Location = new System.Drawing.Point(0, 0);
            this.dgTabel.Name = "dgTabel";
            this.dgTabel.ParentRowsBackColor = System.Drawing.Color.LightGray;
            this.dgTabel.ParentRowsForeColor = System.Drawing.Color.Black;
            this.dgTabel.PreferredColumnWidth = 40;
            this.dgTabel.ReadOnly = true;
            this.dgTabel.RowHeadersVisible = false;
            this.dgTabel.SelectionBackColor = System.Drawing.Color.Firebrick;
            this.dgTabel.SelectionForeColor = System.Drawing.Color.White;
            this.dgTabel.Size = new System.Drawing.Size(704, 483);
            this.dgTabel.TabIndex = 0;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.FileName = "help\\PTMaker.chm";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuOpenHelp,
            this.MenuAbout});
            this.menuItem4.Text = "Help";
            // 
            // MenuOpenHelp
            // 
            this.MenuOpenHelp.Index = 0;
            this.MenuOpenHelp.Text = "Product Help";
            this.MenuOpenHelp.Click += new System.EventHandler(this.MenuOpenHelp_Click);
            // 
            // MenuAbout
            // 
            this.MenuAbout.Index = 1;
            this.MenuAbout.Text = "About";
            this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // mainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(712, 553);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.Menu = this.mainMenu1;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Grammar";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Grammer.ResumeLayout(false);
            this.firstFollow.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.parsTablePage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new mainForm());
			}
			catch
			{
				MessageBox.Show("There is an unspecified Error!.Please run the program again");
			}
		}

		private void open_Click(object sender, System.EventArgs e)
		{			
			if(saveConfirm(sender,e))
			{
				OpenFileDialog openDlg=new OpenFileDialog();
				openDlg.Filter="(*.grm)|*.grm|(*.*)|*.*";
				if(openDlg.ShowDialog()==DialogResult.OK)
				{
					try
					{ 
						this.New_Click(sender,e);
						path=openDlg.FileName;
						StreamReader reader=new StreamReader(path,System.Text.Encoding.ASCII);
						//content.LoadFile(openDlg.FileName,System.Windows.Forms.RichTextBoxStreamType.PlainText);
						this.Text=openDlg.FileName;
						MakeStates.Enabled=true;
						makePars.Enabled=true;
						save.Enabled=false;
						save1.Enabled=false;
						highLight(reader);
						reader.Close();
					}
					catch(Exception exp)
					{
						MessageBox.Show(exp.Message);
					}
				}
			}
		}

		private void MakeParseTable_Click(object sender, System.EventArgs e)
		{			
			
			try
			{
			    this.parsHead = new ParsHead();
				foreach(string s in content.Lines)
					this.parsHead.load(s);
			}
			catch(Exception ee)
			{
				MessageBox.Show(ee.Message);
			}
		}

		private void saveAs_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog saveDlg=new SaveFileDialog();
			saveDlg.Filter="(*.grm)|*.grm|(*.prt)|*.oprt";
			if(saveDlg.ShowDialog()==DialogResult.OK)
			{
				content.SaveFile(saveDlg.FileName,System.Windows.Forms.RichTextBoxStreamType.PlainText);
				path=saveDlg.FileName;
				save.Enabled=false;
				save1.Enabled=false;
			}
		}

		private void save_Click(object sender, System.EventArgs e)
		{
			if(path!="")
			{
				content.SaveFile(path,System.Windows.Forms.RichTextBoxStreamType.PlainText);
				save.Enabled=false;
				save1.Enabled=false;
			}
			else
				saveAs_Click(sender,e);
		}

		private void exit_Click(object sender, System.EventArgs e)
		{
			if(save.Enabled)
			{
				switch(MessageBox.Show("Save Changes?","Save?",System.Windows.Forms.MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						 this.save_Click(sender,e);
						break;
					case DialogResult.Cancel:
						return;
				}
			}
			this.Close();
		}

		private void FindFirst_Click(object sender, System.EventArgs e)
		{
			if(this.parsHead == null)
			{
				MessageBox.Show("You Must Load First a Grammar ...");
				return;
			}
			Stack S=new Stack();
			if(this.parsHead.Head != null)
			{
				this.parsHead.Head.getFirst(parsHead.Head.NonTerminalHead.next.item.Name,S,true);			     
				string firsts="";
				while(S.Count!=0)
				{
					if(firsts!="")
						firsts+=",";
					firsts+=S.Pop().ToString();
				}
				MessageBox.Show("First for "+parsHead.Head.NonTerminalHead.next.item.Name+" : "+firsts);
			}
			else
				MessageBox.Show("Empty Grammar ! ");
		}

		private void FindFollow_Click(object sender, System.EventArgs e)
		{
			if(this.parsHead == null)
			{
				MessageBox.Show("You Must Load First a Grammar ...");
				return;
			}			
			Stack S=new Stack();
			Stack checkedNonTerm = new Stack();
			if(this.parsHead.Head != null)
			{
				this.parsHead.Head.getFollow(parsHead.Head.NonTerminalHead.next.item.Name,S,checkedNonTerm);			     
				string follows="";
				while(S.Count!=0)
				{
					if(follows!="")
						follows+=",";
					follows+=S.Pop().ToString();
				}
				MessageBox.Show("Follow for "+parsHead.Head.NonTerminalHead.next.item.Name+" : "+follows);
			}
			else
				MessageBox.Show("Empty Grammar ! ");
		}

		private void MakeStates_Click(object sender, System.EventArgs e)
		{
			try
			{
                result.Items.Clear();

				parsHead=new ParsHead();
				foreach(string s in content.Lines)
					parsHead.load(s);
				states=new State(parsHead);
				states.create();
				parsTable=new ParsTable(states.StateCount,ParsHead.terminals,parsHead.Head,this.dgTabel);				
				states.makeParsTable(this.parsTable);
				
				parsTable.showContents(result);

                PrintProductions();




				tabControl1.SelectedIndex=2;
				parsTable.writeGrid();
				SaveFileDialog saveDlg=new SaveFileDialog();
				saveDlg.Filter="(*.prt)|*.prt";
				if(saveDlg.ShowDialog()==DialogResult.OK)
				{

                    StreamWriter sw = new StreamWriter(saveDlg.FileName);
                    foreach (string s in result.Items)
                        sw.WriteLine(s);
                    sw.Close();

				//	parsTable.saveTo(sw);

				}
			}
			catch(Exception ex)
			{
//				MessageBox.Show(ex.Message);
				MessageBox.Show("Grammar is incorrect : please check your grammar , example : There is one NonTerminal in Right side that never apear in left side!!!");
			}
		}

        private void PrintProductions()
        {
            result.Items.Add("");
            result.Items.Add("");
            result.Items.Add("// PRODUCTIONS ");
            result.Items.Add("public Dictionary<int, ProductionInfo> CreateProductionsInfo()");
            result.Items.Add("{");
            result.Items.Add("return new Dictionary<int, ProductionInfo>()");
            result.Items.Add("{");

            var i = 1;
            foreach (string s in content.Lines)
            {
                if (s.Length < 2)
                {
                    continue;
                }

                var words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var bodyLength = words.Length - 1;
                var head = words[0].Substring(1);

                var line = "\"" + head + "\", " + bodyLength + ", \"\"";
                line = "{" + i + ", new ProductionInfo(" + line + ")}" + ( i >= content.Lines.Length - 1 ? "" : ",");
                line += " // " + s;

                result.Items.Add(line);
                ++i;
            }

            result.Items.Add("};");
            result.Items.Add("}");
        }

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(toolBar1.Buttons.IndexOf(e.Button))
			{
				case 0://new
					this.New_Click(sender,e);
					break;
				case 1://open
					 this.open_Click(sender,e);
					break;
				case 2://save 
					this.save_Click(sender,e);
					break;
				case 3:
					this.MakeStates_Click(sender,e);
					break;
				case 4:
					this.contents_Click(sender,e);
					break;
				case 5:
					this.exit_Click(sender,e);
					break;
			}
		}

		private void fristRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.comboxNonTerm.Items.Count==0)
			{
				MessageBox.Show("you must first load a grammar...");
				return;
			}
			if(this.comboxNonTerm.SelectedItem.ToString()=="")
			{
				MessageBox.Show("you must first load a grammar...");
				return;
			}

			Stack Result = new Stack();
			if(this.fristRadio.Checked==true)
			{
				if(this.parsHead.Head.getFirst(this.comboxNonTerm.SelectedItem.ToString(),Result,false))
				{
				  Result.Push("Have Epsilon"); 
				}
			}
			else
			{
				Stack checkedNonTerm = new Stack();
				this.parsHead.Head.getFollow(this.comboxNonTerm.SelectedItem.ToString(),Result,checkedNonTerm);			
			}
			lstFirstFollow.Items.Clear();
			while(Result.Count!=0)
			{
			   this.lstFirstFollow.Items.Add(Result.Pop());
			}
		}

		private void comboxNonTerm_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		  this.fristRadio_CheckedChanged(sender,e);
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(tabControl1.SelectedIndex==1)//first follow
				{
					this.parsHead = new ParsHead();
					foreach(string s in content.Lines)
						this.parsHead.load(s);
					this.comboxNonTerm.Items.Clear();				
					nonTerminalNode temp=this.parsHead.Head.NonTerminalHead;
					if(temp==null)
						return;
					else
						temp=temp.next;
					while(temp!=null)
					{
						this.comboxNonTerm.Items.Add(temp.item.Name);
						temp=temp.next;
					}
					if(this.comboxNonTerm.Items.Count!=0)
						this.comboxNonTerm.SelectedIndex=0;
				}
				else
				{
					if(tabControl1.SelectedIndex==2)
					{
						result.Items.Clear();
						if(this.parsTable!=null)
						{
							this.parsTable.showContents(this.result);
                            PrintProductions();
							this.terminalCnt.Text="Count Terminals: "+this.parsHead.TerminalCount.ToString();
							this.nonTerminalCnt.Text="Count of NonTerminals: "+this.parsHead.Head.NonTerminalCount;
							this.lawCnt.Text="Count of Laws: "+parsHead.LawCount.ToString();
							this.stateCnt.Text="Count of States:"+states.StateCount.ToString();
						}
					}
				}
			}
			catch(Exception ee)
			{
				MessageBox.Show(ee.Message);
			}
		}

		private void content_TextChanged(object sender, System.EventArgs e)
		{
			
			save.Enabled=true;
			save1.Enabled=true;
			if(content.Text.Length!=0)
			{
				makePars.Enabled=true;
				MakeStates.Enabled=true;
				 checkHighLight();
			}
			else
			{
				makePars.Enabled=false;
				MakeStates.Enabled=false;
				copy.Enabled=false;
				cut.Enabled=false;
				selectAll.Enabled=false;
			}
		}

		private void New_Click(object sender, System.EventArgs e)
		{
			if(saveConfirm(sender,e))
			{
				this.content.Clear();
				this.Text="New Grammer";
				this.path="";
				this.parsTable = null;
				this.parsHead = null;
				this.result.Items.Clear();
				this.lstFirstFollow.Items.Clear();
			}

		}
		private bool saveConfirm(object sender, System.EventArgs e)
		{
			if(save.Enabled)
			{
				switch(MessageBox.Show("Save Changes?","Save?",System.Windows.Forms.MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						this.save_Click(sender,e);
						return true;
					case DialogResult.Cancel:
						return false;
				}
			}
			return true;
		}

		private void cut_Click(object sender, System.EventArgs e)
		{
			content.Cut();
			paste.Enabled=true;
		}

		private void copy_Click(object sender, System.EventArgs e)
		{
			content.Copy();
			paste.Enabled=true;
		}

		private void paste_Click(object sender, System.EventArgs e)
		{
			content.Paste();
		}

		private void selectAll_Click(object sender, System.EventArgs e)
		{
			content.SelectAll();
			copy.Enabled=true;
			cut.Enabled=true;
		}

		private void content_SelectionChanged(object sender, System.EventArgs e)
		{
			if(content.SelectedText.Length!=0)
			{
				cut.Enabled=true;
				copy.Enabled=true;
			}
		}
		private void checkHighLight()
		{
			try
			{
				int index=content.SelectionStart;
				//int line=content.GetLineFromCharIndex(index);
				for(int i=0;i<=index;i++)
				{
					string subStr=content.Text.Substring(index-i,i);
					if(subStr.Length>0)
					{
						if(subStr[0]==' '||subStr[0]=='\n'||index==1)
						{
							subStr=subStr.Trim();
							if(subStr.Length>0)
							{
								content.Select(index-subStr.Length,subStr.Length);
								if(subStr[0].CompareTo('#')==0)
								{
									this.changeGui(false);
								}
								else
								{
									this.changeGui(true);
								}
							}
							else
							{
								this.changeGui(true);
							}
							break;
						}
					}
				}
				string nextStr="";
				int pos=0;
				int index1=index;
				for(int i=index-1;i>=0;i--)
				{
					if(content.Text[i]==' ' || content.Text[i]=='\n' || i==0)
					{
						index=i+1;
						break;
					}
				}
				for(int i=0;index+i-1<=content.Text.Length;i++,pos++)
				{
					if(index>0)
						nextStr=content.Text.Substring(index-1,i);
					else
					{
						index=1;
						nextStr="";
					}
					if(nextStr.Length>0)
					{
						if((nextStr[nextStr.Length-1]==' '  || nextStr[nextStr.Length-1]=='\n')&& nextStr.Length>1)
						{
							break;
						}
					}
				}
				nextStr=nextStr.Trim();
				if(nextStr.Length>0)
				{
					content.Select(index-1,pos);
					if(nextStr[0]=='#')
					{
						this.changeGui(false);
					}
					else
					{
						this.changeGui(true);
					}
				}
						
				content.AppendText("");
				content.SelectionStart=index1;
			}
			catch(Exception e1)
			{
				MessageBox.Show(e1.Message);
			}
}

		private void mainForm_Load(object sender, System.EventArgs e)
		{
		//	System.Threading.Thread editor=new System.Threading.Thread();
		}

		private void contents_Click(object sender, System.EventArgs e)
		{
			try
			{
				process1.Start();
			}
			catch(Exception exc)
			{
				MessageBox.Show("The Help file was not found and this message is returned from system: "+exc.Message);
			}
		}
		private void changeGui(bool isTerminal)
		{
			if(isTerminal)
			{
				content.SelectionFont=this.terminalFont;
				content.SelectionColor=this.terminalColor;
			}
			else
			{
				content.SelectionFont=this.nonTerminalFont;
				content.SelectionColor=this.nonTerminalColor;
			}
		}
		private void highLight(StreamReader reader)
		{
			//this.load=true;
			string str=reader.ReadToEnd();
		/*	this.content.Text="";
			bool nextSpace=true;
			for(int i=0;i<str.Length;i++)
			{
			/*	if(i>0)
				  this.content.SelectionStart=i-1;
				else
					this.content.SelectionStart=i;
				if(nextSpace)
				{
					nextSpace = false;
					if(str[i]=='#')
					{
						//this.changeGui(false);
						this.content.Font=this.nonTerminalFont;
                        this.content.ForeColor=this.nonTerminalColor;
					}
					else
					{
						//this.changeGui(true);
						
						this.content.Font=this.terminalFont;
						this.content.ForeColor=this.terminalColor;
					}
				}
				if(str[i]==' ' || str[i]=='\n')
					nextSpace=true;
				/*if(i>0)
				  content.Select(i-1,1);
				content.Text+=str[i];
			}
			this.load=false;*/
			for(int i=0;i<str.Length;i++)
			{
				if(str[i]!='\r')
				   this.content.AppendText(str[i].ToString());
			}
		}

		private void SaveParsTable_Click(object sender, System.EventArgs e)
		{
			if(result.Items.Count!=0)
			{
			    System.Windows.Forms.SaveFileDialog savedlg=new SaveFileDialog();
				savedlg.Filter="(*.prt)|*.prt";
				if(savedlg.ShowDialog()==DialogResult.OK)
				{
				    StreamWriter sw = new StreamWriter(savedlg.FileName);
					foreach( string s in result.Items)
						sw.WriteLine(s);
					sw.Close();
				}
			}
			else
			{
			 MessageBox.Show("Pars Table is Empty");
			}
		}

		private void result_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

         private void MenuAbout_Click(object sender, EventArgs e)
         {
             About ab = new About();
             ab.ShowDialog();
         }

         private void MenuOpenHelp_Click(object sender, EventArgs e)
         {
             Process pr = new Process();
             ProcessStartInfo ps = new ProcessStartInfo(Application.StartupPath + "\\help\\PTMaker.chm");
             pr.StartInfo = ps;
             pr.Start();
         }
	}
}
