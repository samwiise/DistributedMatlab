using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;


namespace ASMControls
{
	/// <summary>
	/// Summary description for FolderAndFileView.
	/// </summary>
	public delegate void OpenFile(object sender,OpenFileEventArg e);

	public class FolderAndFileView : System.Windows.Forms.UserControl
	{
//********************************************
		const int DRIVE_UNKNOWN     = 0;
		const int DRIVE_NO_ROOT_DIR = 1;
		const int DRIVE_REMOVABLE   = 2;
		const int DRIVE_FIXED       = 3;
		const int DRIVE_REMOTE      = 4;
		const int DRIVE_CDROM       = 5;
		const int DRIVE_RAMDISK     = 6; 

		[DllImport("Kernel32.dll")]
		static extern int GetDriveType(string path);
//************************************************


		private System.Windows.Forms.TreeView Folders;
		private System.Windows.Forms.Splitter FolderFileSeperator;
		private System.Windows.Forms.ListView Files;
		private System.Windows.Forms.ImageList Icons;
		private System.ComponentModel.IContainer components;
		
		private string filter="*.*";
		public event OpenFile OpenSelectedFile;

		private enum ImageListConstants{
			CLOSEDFOLDER,OPENEDFOLDER,FLOPPYDRIVE,FIXEDDRIVE,NETWORKDRIVE,CDROM,RAMDRIVE,
			FILE,DESKTOP,DOCUMENTS
		}

		public FolderAndFileView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			// TODO: Add any initialization after the InitializeComponent call

			TreeNode tmpnd = Folders.Nodes.Add("Desktop");
			tmpnd.ImageIndex = (int)ImageListConstants.DESKTOP;
			tmpnd.SelectedImageIndex = (int)ImageListConstants.DESKTOP;
			
			tmpnd = Folders.Nodes.Add("My Documents");
			tmpnd.ImageIndex = (int)ImageListConstants.DOCUMENTS;
			tmpnd.SelectedImageIndex = (int)ImageListConstants.DOCUMENTS;
			
			string[] drives = Directory.GetLogicalDrives();

			foreach(string drv in drives){
			
				tmpnd = Folders.Nodes.Add( drv.Replace('\\',' ').Trim());
				switch(GetDriveType(drv)){
				
					case DRIVE_FIXED:
						tmpnd.ImageIndex = (int)ImageListConstants.FIXEDDRIVE;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.FIXEDDRIVE;
						break;

					case DRIVE_CDROM :
						tmpnd.ImageIndex = (int)ImageListConstants.CDROM;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.CDROM;
						break;

					case DRIVE_REMOVABLE:
						tmpnd.ImageIndex = (int)ImageListConstants.FLOPPYDRIVE;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.FLOPPYDRIVE;
						break;
					case DRIVE_REMOTE:
						tmpnd.ImageIndex = (int)ImageListConstants.NETWORKDRIVE;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.NETWORKDRIVE;
						break;
					case DRIVE_RAMDISK:
						tmpnd.ImageIndex = (int)ImageListConstants.RAMDRIVE;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.RAMDRIVE;
						break;
					
					default:
						tmpnd.ImageIndex = (int)ImageListConstants.FIXEDDRIVE;
						tmpnd.SelectedImageIndex = (int)ImageListConstants.FIXEDDRIVE;
						break;
				}
			
			}

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FolderAndFileView));
			this.Folders = new System.Windows.Forms.TreeView();
			this.Icons = new System.Windows.Forms.ImageList(this.components);
			this.FolderFileSeperator = new System.Windows.Forms.Splitter();
			this.Files = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// Folders
			// 
			this.Folders.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Folders.Dock = System.Windows.Forms.DockStyle.Top;
			this.Folders.ImageIndex = 5;
			this.Folders.ImageList = this.Icons;
			this.Folders.Location = new System.Drawing.Point(0, 0);
			this.Folders.Name = "Folders";
			this.Folders.SelectedImageIndex = 1;
			this.Folders.Size = new System.Drawing.Size(264, 152);
			this.Folders.TabIndex = 0;
			this.Folders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Folders_AfterSelect);
			// 
			// Icons
			// 
			this.Icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.Icons.ImageSize = new System.Drawing.Size(16, 16);
			this.Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Icons.ImageStream")));
			this.Icons.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// FolderFileSeperator
			// 
			this.FolderFileSeperator.Dock = System.Windows.Forms.DockStyle.Top;
			this.FolderFileSeperator.Location = new System.Drawing.Point(0, 152);
			this.FolderFileSeperator.Name = "FolderFileSeperator";
			this.FolderFileSeperator.Size = new System.Drawing.Size(264, 5);
			this.FolderFileSeperator.TabIndex = 1;
			this.FolderFileSeperator.TabStop = false;
			// 
			// Files
			// 
			this.Files.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Files.HideSelection = false;
			this.Files.Location = new System.Drawing.Point(0, 157);
			this.Files.MultiSelect = false;
			this.Files.Name = "Files";
			this.Files.Size = new System.Drawing.Size(264, 163);
			this.Files.SmallImageList = this.Icons;
			this.Files.TabIndex = 2;
			this.Files.View = System.Windows.Forms.View.List;
			this.Files.DoubleClick += new System.EventHandler(this.Files_DoubleClick);
			// 
			// FolderAndFileView
			// 
			this.Controls.Add(this.Files);
			this.Controls.Add(this.FolderFileSeperator);
			this.Controls.Add(this.Folders);
			this.Name = "FolderAndFileView";
			this.Size = new System.Drawing.Size(264, 320);
			this.ResumeLayout(false);

		}
		#endregion
		private string NormalizePath(string path){
			
			string[] nds = 	path.Split(new char[]{'\\'});
			
			if(nds[0]=="Desktop")
				nds[0] = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			else if(nds[0] == "My Documents")
				nds[0] = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			
			return string.Join("\\",nds);
		}
		private void Folders_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			string pth =  NormalizePath(e.Node.FullPath);
			
			DirectoryInfo tmp = new DirectoryInfo(pth+"\\");
			e.Node.Nodes.Clear();			
			try
			{
				DirectoryInfo[] subdir = tmp.GetDirectories();
				foreach(DirectoryInfo tmpd in subdir)
				{
					TreeNode tmpnode = e.Node.Nodes.Add(tmpd.Name);
					tmpnode.ImageIndex = (int)ImageListConstants.CLOSEDFOLDER;
					tmpnode.SelectedImageIndex = (int)ImageListConstants.OPENEDFOLDER;
				}
				Files.Clear();
			
			foreach(FileInfo tmpf in tmp.GetFiles(filter))
				{
					ListViewItem tmpitm =  Files.Items.Add(tmpf.Name,(int)ImageListConstants.FILE);
					tmpitm.Tag = tmpf.FullName;
				}
				//e.Node.Expand();
			}
			catch(Exception exc)
			{
				System.Windows.Forms.MessageBox.Show(exc.Message,"Folder Explorer",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);			
			}
		}

		private void Files_DoubleClick(object sender, System.EventArgs e)
		{
			if(Files.SelectedItems.Count>0 && OpenSelectedFile!=null){
				OpenFileEventArg tmp = new OpenFileEventArg();
				tmp.filename = Files.SelectedItems[0].Tag.ToString();
				OpenSelectedFile(this,tmp);
			}
		}

		public string Filter{
			
			get{
				return filter;
			}
			set{
				filter=value;
			}
		}
	}
	public class OpenFileEventArg:EventArgs
	{
		
		public string filename;
	}
}
