using System;
//using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using System.Data;
using System.IO;
using DMatlab;
using System.Diagnostics;

namespace DMatlabIDE
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private string currentfile="";
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MainMenu MyMenu;
		private System.Windows.Forms.MenuItem FileMenu;
		private System.Windows.Forms.MenuItem EditMenu;
		private System.Windows.Forms.ToolBar MyToolbar;
		private System.Windows.Forms.StatusBar MyStatusBar;
		private System.Windows.Forms.Panel ExplorerPanel;
		private System.Windows.Forms.Label ExpLabel;
		private System.Windows.Forms.Panel FileFolderPanel;
		private ASMControls.FolderAndFileView MyExplorer;
		private System.Windows.Forms.Splitter ExpEditorSplitter;
		private System.Windows.Forms.Panel CmpPanel;
		private System.Windows.Forms.RichTextBox MyTextEditor;
		private System.Windows.Forms.MenuItem File_New;
		private System.Windows.Forms.MenuItem File_Open;
		private System.Windows.Forms.MenuItem File_Close;
		private System.Windows.Forms.MenuItem File_Exit;
		private System.Windows.Forms.OpenFileDialog MyOpenDialog;
		private System.Windows.Forms.SaveFileDialog MySaveDialog;
		private System.Windows.Forms.MenuItem File_Save;
		private System.Windows.Forms.MenuItem File_SaveAS;
		private System.Windows.Forms.StatusBarPanel LineNumber;
		private System.Windows.Forms.MenuItem Run_Menu;
		private System.Windows.Forms.MenuItem Run_Run;
		private System.Windows.Forms.MenuItem Run_Compile;
		private System.Windows.Forms.MenuItem Run_Execute;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem Run_Settings;
		private System.Windows.Forms.StatusBarPanel CompileStatus;
		private System.Windows.Forms.MenuItem Edit_Undo;
		private System.Windows.Forms.MenuItem Edit_Redo;
		private System.Windows.Forms.MenuItem Edit_Cut;
		private System.Windows.Forms.MenuItem Edit_Copy;
		private System.Windows.Forms.MenuItem Edit_Paste;
		private System.Windows.Forms.MenuItem Edit_SelectAll;
		private System.Windows.Forms.MenuItem File_LoadCompiled;
		private System.Windows.Forms.ImageList toolbaricons;
		private System.Windows.Forms.ToolBarButton TB_New;
		private System.Windows.Forms.ToolBarButton TB_Open;
		private System.Windows.Forms.ToolBarButton TB_Save;
		private System.Windows.Forms.ToolBarButton TB_Sep1;
		private System.Windows.Forms.ToolBarButton TB_Undo;
		private System.Windows.Forms.ToolBarButton TB_Redo;
		private System.Windows.Forms.ToolBarButton TB_Sep2;
		private System.Windows.Forms.ToolBarButton TB_Copy;
		private System.Windows.Forms.ToolBarButton TB_Paste;
		private System.Windows.Forms.ToolBarButton TB_Cut;
		private System.Windows.Forms.ImageList smallicons;
		private System.Windows.Forms.ToolBarButton TB_Sep3;
		private System.Windows.Forms.ToolBarButton TB_Run;
		private System.Windows.Forms.ToolBarButton TB_Compile;
		private System.Windows.Forms.MenuItem Help_Menu;
		private System.Windows.Forms.MenuItem Help_About;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.MyMenu = new System.Windows.Forms.MainMenu();
			this.FileMenu = new System.Windows.Forms.MenuItem();
			this.File_New = new System.Windows.Forms.MenuItem();
			this.File_Open = new System.Windows.Forms.MenuItem();
			this.File_Save = new System.Windows.Forms.MenuItem();
			this.File_SaveAS = new System.Windows.Forms.MenuItem();
			this.File_Close = new System.Windows.Forms.MenuItem();
			this.File_LoadCompiled = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.File_Exit = new System.Windows.Forms.MenuItem();
			this.EditMenu = new System.Windows.Forms.MenuItem();
			this.Edit_Undo = new System.Windows.Forms.MenuItem();
			this.Edit_Redo = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.Edit_Cut = new System.Windows.Forms.MenuItem();
			this.Edit_Copy = new System.Windows.Forms.MenuItem();
			this.Edit_Paste = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.Edit_SelectAll = new System.Windows.Forms.MenuItem();
			this.Run_Menu = new System.Windows.Forms.MenuItem();
			this.Run_Run = new System.Windows.Forms.MenuItem();
			this.Run_Compile = new System.Windows.Forms.MenuItem();
			this.Run_Execute = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.Run_Settings = new System.Windows.Forms.MenuItem();
			this.MyToolbar = new System.Windows.Forms.ToolBar();
			this.TB_New = new System.Windows.Forms.ToolBarButton();
			this.TB_Open = new System.Windows.Forms.ToolBarButton();
			this.TB_Save = new System.Windows.Forms.ToolBarButton();
			this.TB_Sep1 = new System.Windows.Forms.ToolBarButton();
			this.TB_Undo = new System.Windows.Forms.ToolBarButton();
			this.TB_Redo = new System.Windows.Forms.ToolBarButton();
			this.TB_Sep2 = new System.Windows.Forms.ToolBarButton();
			this.TB_Cut = new System.Windows.Forms.ToolBarButton();
			this.TB_Copy = new System.Windows.Forms.ToolBarButton();
			this.TB_Paste = new System.Windows.Forms.ToolBarButton();
			this.TB_Sep3 = new System.Windows.Forms.ToolBarButton();
			this.TB_Run = new System.Windows.Forms.ToolBarButton();
			this.TB_Compile = new System.Windows.Forms.ToolBarButton();
			this.toolbaricons = new System.Windows.Forms.ImageList(this.components);
			this.MyStatusBar = new System.Windows.Forms.StatusBar();
			this.CompileStatus = new System.Windows.Forms.StatusBarPanel();
			this.LineNumber = new System.Windows.Forms.StatusBarPanel();
			this.ExplorerPanel = new System.Windows.Forms.Panel();
			this.FileFolderPanel = new System.Windows.Forms.Panel();
			this.MyExplorer = new ASMControls.FolderAndFileView();
			this.ExpLabel = new System.Windows.Forms.Label();
			this.ExpEditorSplitter = new System.Windows.Forms.Splitter();
			this.CmpPanel = new System.Windows.Forms.Panel();
			this.MyTextEditor = new System.Windows.Forms.RichTextBox();
			this.MyOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.MySaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.smallicons = new System.Windows.Forms.ImageList(this.components);
			this.Help_Menu = new System.Windows.Forms.MenuItem();
			this.Help_About = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.CompileStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LineNumber)).BeginInit();
			this.ExplorerPanel.SuspendLayout();
			this.FileFolderPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MyMenu
			// 
			this.MyMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.FileMenu,
																				   this.EditMenu,
																				   this.Run_Menu,
																				   this.Help_Menu});
			// 
			// FileMenu
			// 
			this.FileMenu.Index = 0;
			this.FileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.File_New,
																					 this.File_Open,
																					 this.File_Save,
																					 this.File_SaveAS,
																					 this.File_Close,
																					 this.File_LoadCompiled,
																					 this.menuItem6,
																					 this.File_Exit});
			this.FileMenu.Text = "File";
			// 
			// File_New
			// 
			this.File_New.Index = 0;
			this.File_New.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.File_New.Text = "New";
			this.File_New.Click += new System.EventHandler(this.File_New_Click);
			// 
			// File_Open
			// 
			this.File_Open.Index = 1;
			this.File_Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.File_Open.Text = "Open";
			this.File_Open.Click += new System.EventHandler(this.File_Open_Click);
			// 
			// File_Save
			// 
			this.File_Save.Index = 2;
			this.File_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.File_Save.Text = "Save";
			this.File_Save.Click += new System.EventHandler(this.File_Save_Click);
			// 
			// File_SaveAS
			// 
			this.File_SaveAS.Index = 3;
			this.File_SaveAS.Text = "Save As";
			this.File_SaveAS.Click += new System.EventHandler(this.File_SaveAS_Click);
			// 
			// File_Close
			// 
			this.File_Close.Index = 4;
			this.File_Close.Text = "Close";
			this.File_Close.Click += new System.EventHandler(this.File_Close_Click);
			// 
			// File_LoadCompiled
			// 
			this.File_LoadCompiled.Index = 5;
			this.File_LoadCompiled.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.File_LoadCompiled.Text = "Load Compiled File..";
			this.File_LoadCompiled.Click += new System.EventHandler(this.File_LoadCompiled_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.Text = "-";
			// 
			// File_Exit
			// 
			this.File_Exit.Index = 7;
			this.File_Exit.Text = "Exit";
			this.File_Exit.Click += new System.EventHandler(this.File_Exit_Click);
			// 
			// EditMenu
			// 
			this.EditMenu.Index = 1;
			this.EditMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.Edit_Undo,
																					 this.Edit_Redo,
																					 this.menuItem9,
																					 this.Edit_Cut,
																					 this.Edit_Copy,
																					 this.Edit_Paste,
																					 this.menuItem13,
																					 this.Edit_SelectAll});
			this.EditMenu.Text = "Edit";
			this.EditMenu.Popup += new System.EventHandler(this.EditMenu_Popup);
			// 
			// Edit_Undo
			// 
			this.Edit_Undo.Index = 0;
			this.Edit_Undo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.Edit_Undo.Text = "Undo";
			this.Edit_Undo.Click += new System.EventHandler(this.Edit_Undo_Click);
			// 
			// Edit_Redo
			// 
			this.Edit_Redo.Index = 1;
			this.Edit_Redo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
			this.Edit_Redo.Text = "Redo";
			this.Edit_Redo.Click += new System.EventHandler(this.Edit_Redo_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "-";
			// 
			// Edit_Cut
			// 
			this.Edit_Cut.Index = 3;
			this.Edit_Cut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.Edit_Cut.Text = "Cut";
			this.Edit_Cut.Click += new System.EventHandler(this.Edit_Cut_Click);
			// 
			// Edit_Copy
			// 
			this.Edit_Copy.Index = 4;
			this.Edit_Copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.Edit_Copy.Text = "Copy";
			this.Edit_Copy.Click += new System.EventHandler(this.Edit_Copy_Click);
			// 
			// Edit_Paste
			// 
			this.Edit_Paste.Index = 5;
			this.Edit_Paste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.Edit_Paste.Text = "Paste";
			this.Edit_Paste.Click += new System.EventHandler(this.Edit_Paste_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 6;
			this.menuItem13.Text = "-";
			// 
			// Edit_SelectAll
			// 
			this.Edit_SelectAll.Index = 7;
			this.Edit_SelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.Edit_SelectAll.Text = "Select All";
			this.Edit_SelectAll.Click += new System.EventHandler(this.Edit_SelectAll_Click);
			// 
			// Run_Menu
			// 
			this.Run_Menu.Index = 2;
			this.Run_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.Run_Run,
																					 this.Run_Compile,
																					 this.Run_Execute,
																					 this.menuItem1,
																					 this.Run_Settings});
			this.Run_Menu.Text = "Run";
			// 
			// Run_Run
			// 
			this.Run_Run.Index = 0;
			this.Run_Run.Shortcut = System.Windows.Forms.Shortcut.CtrlF9;
			this.Run_Run.Text = "Run";
			this.Run_Run.Click += new System.EventHandler(this.Run_Run_Click);
			// 
			// Run_Compile
			// 
			this.Run_Compile.Index = 1;
			this.Run_Compile.Shortcut = System.Windows.Forms.Shortcut.CtrlF8;
			this.Run_Compile.Text = "Compile";
			this.Run_Compile.Click += new System.EventHandler(this.Run_Compile_Click);
			// 
			// Run_Execute
			// 
			this.Run_Execute.Index = 2;
			this.Run_Execute.Shortcut = System.Windows.Forms.Shortcut.CtrlF7;
			this.Run_Execute.Text = "Execute";
			this.Run_Execute.Click += new System.EventHandler(this.Run_Execute_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			// 
			// Run_Settings
			// 
			this.Run_Settings.Index = 4;
			this.Run_Settings.Text = "Settings";
			this.Run_Settings.Click += new System.EventHandler(this.Run_Settings_Click);
			// 
			// MyToolbar
			// 
			this.MyToolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.MyToolbar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.TB_New,
																						 this.TB_Open,
																						 this.TB_Save,
																						 this.TB_Sep1,
																						 this.TB_Undo,
																						 this.TB_Redo,
																						 this.TB_Sep2,
																						 this.TB_Cut,
																						 this.TB_Copy,
																						 this.TB_Paste,
																						 this.TB_Sep3,
																						 this.TB_Run,
																						 this.TB_Compile});
			this.MyToolbar.ButtonSize = new System.Drawing.Size(24, 24);
			this.MyToolbar.DropDownArrows = true;
			this.MyToolbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MyToolbar.ImageList = this.toolbaricons;
			this.MyToolbar.Location = new System.Drawing.Point(4, 4);
			this.MyToolbar.Name = "MyToolbar";
			this.MyToolbar.ShowToolTips = true;
			this.MyToolbar.Size = new System.Drawing.Size(680, 36);
			this.MyToolbar.TabIndex = 0;
			this.MyToolbar.Wrappable = false;
			this.MyToolbar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.MyToolbar_ButtonClick);
			// 
			// TB_New
			// 
			this.TB_New.ImageIndex = 0;
			this.TB_New.Tag = "New";
			this.TB_New.ToolTipText = "New File";
			// 
			// TB_Open
			// 
			this.TB_Open.ImageIndex = 1;
			this.TB_Open.Tag = "Open";
			this.TB_Open.ToolTipText = "Open Existing File";
			// 
			// TB_Save
			// 
			this.TB_Save.ImageIndex = 2;
			this.TB_Save.Tag = "Save";
			this.TB_Save.ToolTipText = "Save File";
			// 
			// TB_Sep1
			// 
			this.TB_Sep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// TB_Undo
			// 
			this.TB_Undo.ImageIndex = 3;
			this.TB_Undo.Tag = "Undo";
			this.TB_Undo.ToolTipText = "Undo";
			// 
			// TB_Redo
			// 
			this.TB_Redo.ImageIndex = 4;
			this.TB_Redo.Tag = "Redo";
			this.TB_Redo.ToolTipText = "Redo";
			// 
			// TB_Sep2
			// 
			this.TB_Sep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// TB_Cut
			// 
			this.TB_Cut.ImageIndex = 7;
			this.TB_Cut.Tag = "Cut";
			this.TB_Cut.ToolTipText = "Cut";
			// 
			// TB_Copy
			// 
			this.TB_Copy.ImageIndex = 5;
			this.TB_Copy.Tag = "Copy";
			this.TB_Copy.ToolTipText = "Copy";
			// 
			// TB_Paste
			// 
			this.TB_Paste.ImageIndex = 6;
			this.TB_Paste.Tag = "Paste";
			this.TB_Paste.ToolTipText = "Paste";
			// 
			// TB_Sep3
			// 
			this.TB_Sep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// TB_Run
			// 
			this.TB_Run.ImageIndex = 8;
			this.TB_Run.Tag = "Run";
			this.TB_Run.ToolTipText = "Run";
			// 
			// TB_Compile
			// 
			this.TB_Compile.ImageIndex = 9;
			this.TB_Compile.Tag = "Compile";
			this.TB_Compile.ToolTipText = "Compile";
			// 
			// toolbaricons
			// 
			this.toolbaricons.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.toolbaricons.ImageSize = new System.Drawing.Size(23, 24);
			this.toolbaricons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbaricons.ImageStream")));
			this.toolbaricons.TransparentColor = System.Drawing.Color.Black;
			// 
			// MyStatusBar
			// 
			this.MyStatusBar.Location = new System.Drawing.Point(4, 412);
			this.MyStatusBar.Name = "MyStatusBar";
			this.MyStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						   this.CompileStatus,
																						   this.LineNumber});
			this.MyStatusBar.ShowPanels = true;
			this.MyStatusBar.Size = new System.Drawing.Size(680, 22);
			this.MyStatusBar.SizingGrip = false;
			this.MyStatusBar.TabIndex = 1;
			this.MyStatusBar.Text = "statusBar1";
			// 
			// CompileStatus
			// 
			this.CompileStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.CompileStatus.Width = 580;
			// 
			// LineNumber
			// 
			this.LineNumber.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.LineNumber.Text = "Line : 1";
			// 
			// ExplorerPanel
			// 
			this.ExplorerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ExplorerPanel.Controls.Add(this.FileFolderPanel);
			this.ExplorerPanel.Controls.Add(this.ExpLabel);
			this.ExplorerPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ExplorerPanel.Location = new System.Drawing.Point(4, 40);
			this.ExplorerPanel.Name = "ExplorerPanel";
			this.ExplorerPanel.Size = new System.Drawing.Size(168, 372);
			this.ExplorerPanel.TabIndex = 2;
			// 
			// FileFolderPanel
			// 
			this.FileFolderPanel.Controls.Add(this.MyExplorer);
			this.FileFolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FileFolderPanel.Location = new System.Drawing.Point(0, 23);
			this.FileFolderPanel.Name = "FileFolderPanel";
			this.FileFolderPanel.Size = new System.Drawing.Size(164, 345);
			this.FileFolderPanel.TabIndex = 1;
			// 
			// MyExplorer
			// 
			this.MyExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MyExplorer.Filter = "*.mlp";
			this.MyExplorer.Location = new System.Drawing.Point(0, 0);
			this.MyExplorer.Name = "MyExplorer";
			this.MyExplorer.Size = new System.Drawing.Size(164, 345);
			this.MyExplorer.TabIndex = 0;
			this.MyExplorer.OpenSelectedFile += new ASMControls.OpenFile(this.MyExplorer_OpenSelectedFile);
			// 
			// ExpLabel
			// 
			this.ExpLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ExpLabel.Location = new System.Drawing.Point(0, 0);
			this.ExpLabel.Name = "ExpLabel";
			this.ExpLabel.Size = new System.Drawing.Size(164, 23);
			this.ExpLabel.TabIndex = 0;
			this.ExpLabel.Text = "   File Explorer";
			this.ExpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ExpEditorSplitter
			// 
			this.ExpEditorSplitter.Location = new System.Drawing.Point(172, 40);
			this.ExpEditorSplitter.Name = "ExpEditorSplitter";
			this.ExpEditorSplitter.Size = new System.Drawing.Size(4, 372);
			this.ExpEditorSplitter.TabIndex = 3;
			this.ExpEditorSplitter.TabStop = false;
			// 
			// CmpPanel
			// 
			this.CmpPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.CmpPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.CmpPanel.Location = new System.Drawing.Point(176, 40);
			this.CmpPanel.Name = "CmpPanel";
			this.CmpPanel.Size = new System.Drawing.Size(508, 24);
			this.CmpPanel.TabIndex = 5;
			// 
			// MyTextEditor
			// 
			this.MyTextEditor.AcceptsTab = true;
			this.MyTextEditor.AutoWordSelection = true;
			this.MyTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MyTextEditor.Location = new System.Drawing.Point(176, 64);
			this.MyTextEditor.Name = "MyTextEditor";
			this.MyTextEditor.Size = new System.Drawing.Size(508, 348);
			this.MyTextEditor.TabIndex = 6;
			this.MyTextEditor.Text = "\n/* This \n\t\tis the \n\t\t\t\tcommented block\n*/\n\nif ( a == 10){\n\t\n\tPrint \"DRdfsdfsdF\";" +
				" \t//This is the Commented Line\n\n}else {\n\n\tPrint \"%RTTRRT\";\n\n}\n";
			this.MyTextEditor.WordWrap = false;
			this.MyTextEditor.SelectionChanged += new System.EventHandler(this.MyTextEditor_SelectionChanged);
			// 
			// MyOpenDialog
			// 
			this.MyOpenDialog.DefaultExt = "*.mlp";
			this.MyOpenDialog.Filter = "Matlab Program Files(*.mlp)|*.mlp|Matlab Compiled File(*.imc)|*.imc";
			this.MyOpenDialog.Title = "Open Matlab Program";
			// 
			// MySaveDialog
			// 
			this.MySaveDialog.DefaultExt = "*.mlp";
			this.MySaveDialog.Filter = "Matlab Program Files|*.mlp";
			this.MySaveDialog.Title = "Save Matlab Program File";
			// 
			// smallicons
			// 
			this.smallicons.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.smallicons.ImageSize = new System.Drawing.Size(16, 16);
			this.smallicons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallicons.ImageStream")));
			this.smallicons.TransparentColor = System.Drawing.Color.Silver;
			// 
			// Help_Menu
			// 
			this.Help_Menu.Index = 3;
			this.Help_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Help_About});
			this.Help_Menu.Text = "Help";
			// 
			// Help_About
			// 
			this.Help_About.Index = 0;
			this.Help_About.Text = "About";
			this.Help_About.Click += new System.EventHandler(this.Help_About_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 438);
			this.Controls.Add(this.MyTextEditor);
			this.Controls.Add(this.CmpPanel);
			this.Controls.Add(this.ExpEditorSplitter);
			this.Controls.Add(this.ExplorerPanel);
			this.Controls.Add(this.MyStatusBar);
			this.Controls.Add(this.MyToolbar);
			this.DockPadding.All = 4;
			this.Menu = this.MyMenu;
			this.Name = "MainForm";
			this.Text = "Distributed Matlab";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.CompileStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LineNumber)).EndInit();
			this.ExplorerPanel.ResumeLayout(false);
			this.FileFolderPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

//		private void rt1_SelectionChanged(object sender, System.EventArgs e)
//		{
//			this.Text =  rt1.GetLineFromCharIndex(rt1.SelectionStart).ToString();
//			
//		}
		private void Form1_Load(object sender, System.EventArgs e)
		{
			//int a=5,b;
			//for(MessageBox.Show("4554");true && false;);
			

			CurrentFile="";

			//for(int a=1;a<10;a++)
			//{
				//int b=a;
			//}

			UpdateToolbar();
		}

		private void MyToolbar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//System.Windows.Forms.MessageBox.Show(MyTextEditor.Rtf);
			//e.Button.Enabled = false;
			switch(e.Button.Tag.ToString()){
			
				case "New":
					File_New_Click(this,e);
					break;

				case "Open":
					File_Open_Click(this,e);
					break;
				case "Save":
					File_Save_Click(this,e);
					break;
				case "Copy":
					Edit_Copy_Click(this,e);
					break;
				case "Cut":
					Edit_Cut_Click(this,e);
					break;
				case "Paste":
					Edit_Paste_Click(this,e);
					break;
				case "Undo":
					Edit_Undo_Click(this,e);
					break;
				case "Redo":
					Edit_Redo_Click(this,e);
					break;
				case "Run":
					Run_Run_Click(this,e);
					break;
				case "Compile":
					Run_Compile_Click(this,e);
					break;
			
			}
			UpdateToolbar();
		}

		private void MyExplorer_OpenSelectedFile(object sender, ASMControls.OpenFileEventArg e)
		{
			MyTextEditor.LoadFile(e.filename,System.Windows.Forms.RichTextBoxStreamType.PlainText);
			CurrentFile=e.filename;
		}

		private void File_New_Click(object sender, System.EventArgs e)
		{
			CurrentFile = "";
			MyTextEditor.Clear();
		}

		private void File_Open_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.DialogResult rslt= MyOpenDialog.ShowDialog();
			if(rslt == System.Windows.Forms.DialogResult.OK){
				if(Path.GetExtension(MyOpenDialog.FileName).ToUpper()==".IMC")
				{
					Process.Start(Application.StartupPath + "\\LocalMachine.exe","\"" + MyOpenDialog.FileName + "\"" );
					MyOpenDialog.FilterIndex =1;
				}
				else
				{
					MyTextEditor.LoadFile(MyOpenDialog.FileName,System.Windows.Forms.RichTextBoxStreamType.PlainText);
					CurrentFile=MyOpenDialog.FileName;
					//FileInfo finfo = new FileInfo(currentfile);
					//System.Windows.Forms.MessageBox.Show(finfo.Extension);
				}
			}
		}

		private void File_Save_Click(object sender, System.EventArgs e)
		{
			if(CurrentFile==""){
				System.Windows.Forms.DialogResult rslt= MySaveDialog.ShowDialog();
				if(rslt == System.Windows.Forms.DialogResult.OK)
				{
					MyTextEditor.SaveFile(MySaveDialog.FileName,System.Windows.Forms.RichTextBoxStreamType.PlainText);
					CurrentFile=MySaveDialog.FileName;
				}
				
			}
			else
			{
				MyTextEditor.SaveFile(CurrentFile,System.Windows.Forms.RichTextBoxStreamType.PlainText);
			}
		}

		private void File_Close_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Clear();
			CurrentFile = "";
			
		}

		private void File_SaveAS_Click(object sender, System.EventArgs e)
		{
			MySaveDialog.FileName = CurrentFile;
			System.Windows.Forms.DialogResult rslt= MySaveDialog.ShowDialog();
			if(rslt == System.Windows.Forms.DialogResult.OK)
			{
				MyTextEditor.SaveFile(MySaveDialog.FileName,System.Windows.Forms.RichTextBoxStreamType.PlainText);
				CurrentFile=MySaveDialog.FileName;
			}
		}

		private void File_Exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void MyTextEditor_SelectionChanged(object sender, System.EventArgs e)
		{
			MyStatusBar.Panels[1].Text = "Line : " + (MyTextEditor.GetLineFromCharIndex(MyTextEditor.SelectionStart)+1).ToString();
			UpdateToolbar();
		}

		private void Run_Compile_Click(object sender, System.EventArgs e)
		{
			Compile();			
		}
		private bool Compile(){
		
			CompileStatus.Text = "Compiling...";
			if(Parser.Parse(MyTextEditor.Text.ToCharArray()))
			{
				
				try
				{
					SyntaxTree 	syntaxtree = SyntaxAnalyzer.GetTree(Parser.Tokens);				
					ArrayList InstructionCode = new ArrayList();
					NumberAndIdentifierTable nids = new NumberAndIdentifierTable();

					CodeGenerator.GenerateCode(InstructionCode,nids,syntaxtree);

					if(CurrentFile=="")
						CodeGenerator.SaveFile(InstructionCode,nids,Application.StartupPath+"\\Tempfile.imc");
					else
					{
						FileInfo finfo = new FileInfo(CurrentFile);
						CodeGenerator.SaveFile(
							InstructionCode,nids,Path.ChangeExtension(CurrentFile,"imc"));
					}
					CompileStatus.Text = "Compilation process completed successfully.";
					return true;
				}
				catch(Exception exc)
				{
					
					MyTextEditor.Select((int)SyntaxAnalyzer.CurrentToken.startpointer,
						(int)(SyntaxAnalyzer.CurrentToken.endpointer - SyntaxAnalyzer.CurrentToken.startpointer));
					CompileStatus.Text = "Error occured, compilation failed.";
				}
				
			}
			else{
				foreach(Token tkn in Parser.Tokens){
					if(tkn.TType == TokenType.ERROR){
						MyTextEditor.Select((int)tkn.startpointer,
							(int)(tkn.endpointer - tkn.startpointer));
						CompileStatus.Text = "Error occured, compilation failed.";
						break;
					}				
				}
			}
			
			
			return false;
		}
		public string CurrentFile{
			get{
				return currentfile;
			}		
			set{
				currentfile = value;
				UpdateEditor();
			}
		}
		private void UpdateEditor()
		{
			if(currentfile=="")
				this.Text =  "Distributed Matlab";
			else
				this.Text =  "Distributed Matlab - " + currentfile;
		}

		private void Run_Execute_Click(object sender, System.EventArgs e)
		{
		
			string outpfile;
			if(CurrentFile=="")
				outpfile =Application.StartupPath + "\\Tempfile.imc";
			else
				outpfile = Path.ChangeExtension(CurrentFile,"imc");


			if(File.Exists(outpfile))
			{
				Execute(outpfile);						
			}
			else{
				DialogResult rst =System.Windows.Forms.MessageBox.Show("Executable file is not present. Do you want to compile.",
					"Distributed Matlab",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1);
				if(rst==DialogResult.Yes){
					if(Compile()){
						Execute(outpfile);					
					}
				}
			}
		}

		private void Execute(string outpfile){
			CompileStatus.Text = "Running....";
			Process tmp =  Process.Start(Application.StartupPath + @"\LocalMachine.exe","\"" + outpfile + "\"" );
			tmp.WaitForExit();
			CompileStatus.Text = "Process exited with " + tmp.ExitCode.ToString() + " code.";
			tmp.Close();
			this.Focus();
		}
		

		private void Run_Run_Click(object sender, System.EventArgs e)
		{
			if(Compile())
				if(CurrentFile=="")
					Execute(Application.StartupPath + "\\Tempfile.imc");
				else
					Execute(Path.ChangeExtension(CurrentFile,"imc"));
		}

		private void Run_Settings_Click(object sender, System.EventArgs e)
		{
			MachineSettings frm = new MachineSettings();
			frm.ShowDialog(this);
		}

		private void EditMenu_Popup(object sender, System.EventArgs e)
		{
			//MessageBox.Show("dffD");
			if(MyTextEditor.CanUndo)
				Edit_Undo.Enabled=true;
			else
				Edit_Undo.Enabled=false;

			if(MyTextEditor.CanRedo)
				Edit_Redo.Enabled=true;
			else
				Edit_Redo.Enabled=false;
			
			IDataObject data = Clipboard.GetDataObject();
			if (data.GetDataPresent(DataFormats.Text))
				Edit_Paste.Enabled = true;
			else
				Edit_Paste.Enabled = false;

			if(MyTextEditor.SelectedText =="")
			{
				Edit_Copy.Enabled =false;
				Edit_Cut.Enabled =false;
			}
			else{
				Edit_Copy.Enabled =true;
				Edit_Cut.Enabled =true;
			}

			if(MyTextEditor.Text != "")
				Edit_SelectAll.Enabled =true;
			else
				Edit_SelectAll.Enabled =false;
		}

		private void Edit_Undo_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Undo();
			UpdateToolbar();
		}

		private void Edit_Redo_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Redo();
			UpdateToolbar();
		}

		private void Edit_Cut_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Cut();
			UpdateToolbar();
		}

		private void Edit_Copy_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Copy();
			UpdateToolbar();
		}

		private void Edit_Paste_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.Paste(DataFormats.GetFormat(DataFormats.Text));
			UpdateToolbar();
		}

		private void File_LoadCompiled_Click(object sender, System.EventArgs e)
		{
			MyOpenDialog.FilterIndex =2;
			File_Open_Click(this,e);
		}
		
/*		private void button1_Click(object sender, System.EventArgs e)
		{
			string[] s =  Directory.GetLogicalDrives();
			
			string a="";
			foreach(string b in s)
			{
				DirectoryInfo tt = new DirectoryInfo(b);
				a=a+tt.Name+ " -- " + tt.Attributes.ToString() + "\r\n";
			}
			System.Windows.Forms.MessageBox.Show(a);

		}*/

		private void UpdateToolbar()
		{
			if(MyTextEditor.CanUndo)
				TB_Undo.Enabled=true;
			else
				TB_Undo.Enabled=false;

			if(MyTextEditor.CanRedo)
				TB_Redo.Enabled=true;
			else
				TB_Redo.Enabled=false;
			
			IDataObject data = Clipboard.GetDataObject();
			if (data.GetDataPresent(DataFormats.Text))
				TB_Paste.Enabled = true;
			else
				TB_Paste.Enabled = false;

			if(MyTextEditor.SelectedText =="")
			{
				TB_Copy.Enabled =false;
				TB_Cut.Enabled =false;
			}
			else
			{
				TB_Copy.Enabled =true;
				TB_Cut.Enabled =true;
			}
			
		}

		private void Edit_SelectAll_Click(object sender, System.EventArgs e)
		{
			MyTextEditor.SelectAll();
		}

		private void Help_About_Click(object sender, System.EventArgs e)
		{
			frmabout tmpfrm = new frmabout ();
			tmpfrm.ShowDialog(this);
		}

	}
}
