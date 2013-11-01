using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
namespace DMatlabIDE
{
	/// <summary>
	/// Summary description for MachineSettings.
	/// </summary>
	public class MachineSettings : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView processorslist;
		private System.Windows.Forms.ColumnHeader IpAddress;
		private System.Windows.Forms.TextBox addipaddress;
		private System.Windows.Forms.TextBox addport;
		private System.Windows.Forms.Button b_add;
		private System.Windows.Forms.Button b_remove;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown matmemsize;
		private System.Windows.Forms.NumericUpDown threadstacksize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button b_ok;
		private System.Windows.Forms.Button b_cancel;
		private System.Windows.Forms.ColumnHeader Port;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListView deviceslist;
		private System.Windows.Forms.Button b_rmdevice;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button adddevice;
		private System.Windows.Forms.TextBox adddport;
		private System.Windows.Forms.TextBox adddname;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MachineSettings()
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
				if(components != null)
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
			this.processorslist = new System.Windows.Forms.ListView();
			this.IpAddress = new System.Windows.Forms.ColumnHeader();
			this.Port = new System.Windows.Forms.ColumnHeader();
			this.addipaddress = new System.Windows.Forms.TextBox();
			this.addport = new System.Windows.Forms.TextBox();
			this.b_add = new System.Windows.Forms.Button();
			this.b_remove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.matmemsize = new System.Windows.Forms.NumericUpDown();
			this.threadstacksize = new System.Windows.Forms.NumericUpDown();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.b_ok = new System.Windows.Forms.Button();
			this.b_cancel = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.adddport = new System.Windows.Forms.TextBox();
			this.adddname = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.adddevice = new System.Windows.Forms.Button();
			this.deviceslist = new System.Windows.Forms.ListView();
			this.b_rmdevice = new System.Windows.Forms.Button();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this.matmemsize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.threadstacksize)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// processorslist
			// 
			this.processorslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.IpAddress,
																							 this.Port});
			this.processorslist.FullRowSelect = true;
			this.processorslist.Location = new System.Drawing.Point(8, 8);
			this.processorslist.Name = "processorslist";
			this.processorslist.Size = new System.Drawing.Size(240, 104);
			this.processorslist.TabIndex = 0;
			this.processorslist.View = System.Windows.Forms.View.Details;
			this.processorslist.SelectedIndexChanged += new System.EventHandler(this.processorslist_SelectedIndexChanged);
			// 
			// IpAddress
			// 
			this.IpAddress.Text = "Ip Address";
			this.IpAddress.Width = 143;
			// 
			// Port
			// 
			this.Port.Text = "Port";
			// 
			// addipaddress
			// 
			this.addipaddress.Location = new System.Drawing.Point(67, 6);
			this.addipaddress.Name = "addipaddress";
			this.addipaddress.Size = new System.Drawing.Size(109, 20);
			this.addipaddress.TabIndex = 3;
			this.addipaddress.Text = "";
			// 
			// addport
			// 
			this.addport.Location = new System.Drawing.Point(217, 6);
			this.addport.Name = "addport";
			this.addport.Size = new System.Drawing.Size(64, 20);
			this.addport.TabIndex = 4;
			this.addport.Text = "";
			// 
			// b_add
			// 
			this.b_add.Location = new System.Drawing.Point(293, 6);
			this.b_add.Name = "b_add";
			this.b_add.Size = new System.Drawing.Size(56, 20);
			this.b_add.TabIndex = 5;
			this.b_add.Text = "Add";
			this.b_add.Click += new System.EventHandler(this.b_add_Click);
			// 
			// b_remove
			// 
			this.b_remove.Location = new System.Drawing.Point(264, 24);
			this.b_remove.Name = "b_remove";
			this.b_remove.Size = new System.Drawing.Size(72, 72);
			this.b_remove.TabIndex = 6;
			this.b_remove.Text = "Remove";
			this.b_remove.Click += new System.EventHandler(this.b_remove_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 20);
			this.label1.TabIndex = 7;
			this.label1.Text = "Matrix Memory Size";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(183, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 20);
			this.label2.TabIndex = 8;
			this.label2.Text = "Thread Stack Size";
			// 
			// matmemsize
			// 
			this.matmemsize.Location = new System.Drawing.Point(111, 12);
			this.matmemsize.Maximum = new System.Decimal(new int[] {
																	   500,
																	   0,
																	   0,
																	   0});
			this.matmemsize.Minimum = new System.Decimal(new int[] {
																	   10,
																	   0,
																	   0,
																	   0});
			this.matmemsize.Name = "matmemsize";
			this.matmemsize.Size = new System.Drawing.Size(64, 20);
			this.matmemsize.TabIndex = 9;
			this.matmemsize.Value = new System.Decimal(new int[] {
																	 10,
																	 0,
																	 0,
																	 0});
			// 
			// threadstacksize
			// 
			this.threadstacksize.Location = new System.Drawing.Point(287, 12);
			this.threadstacksize.Maximum = new System.Decimal(new int[] {
																			200,
																			0,
																			0,
																			0});
			this.threadstacksize.Minimum = new System.Decimal(new int[] {
																			20,
																			0,
																			0,
																			0});
			this.threadstacksize.Name = "threadstacksize";
			this.threadstacksize.Size = new System.Drawing.Size(64, 20);
			this.threadstacksize.TabIndex = 10;
			this.threadstacksize.Value = new System.Decimal(new int[] {
																		  21,
																		  0,
																		  0,
																		  0});
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.matmemsize);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.threadstacksize);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(352, 40);
			this.panel1.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(186, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Port";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.TabIndex = 13;
			this.label4.Text = "Ip Address";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.addport);
			this.panel2.Controls.Add(this.addipaddress);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.b_add);
			this.panel2.Location = new System.Drawing.Point(0, 112);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(352, 32);
			this.panel2.TabIndex = 14;
			// 
			// b_ok
			// 
			this.b_ok.Location = new System.Drawing.Point(192, 376);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(72, 20);
			this.b_ok.TabIndex = 15;
			this.b_ok.Text = "OK";
			this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
			// 
			// b_cancel
			// 
			this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_cancel.Location = new System.Drawing.Point(272, 376);
			this.b_cancel.Name = "b_cancel";
			this.b_cancel.Size = new System.Drawing.Size(72, 20);
			this.b_cancel.TabIndex = 16;
			this.b_cancel.Text = "Cancel";
			this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Controls.Add(this.processorslist);
			this.panel3.Controls.Add(this.b_remove);
			this.panel3.Location = new System.Drawing.Point(8, 56);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(360, 152);
			this.panel3.TabIndex = 17;
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Controls.Add(this.deviceslist);
			this.panel4.Controls.Add(this.b_rmdevice);
			this.panel4.Location = new System.Drawing.Point(8, 216);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(360, 152);
			this.panel4.TabIndex = 18;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.adddport);
			this.panel5.Controls.Add(this.adddname);
			this.panel5.Controls.Add(this.label5);
			this.panel5.Controls.Add(this.label6);
			this.panel5.Controls.Add(this.adddevice);
			this.panel5.Location = new System.Drawing.Point(0, 112);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(352, 32);
			this.panel5.TabIndex = 14;
			// 
			// adddport
			// 
			this.adddport.Location = new System.Drawing.Point(224, 6);
			this.adddport.Name = "adddport";
			this.adddport.Size = new System.Drawing.Size(48, 20);
			this.adddport.TabIndex = 4;
			this.adddport.Text = "";
			// 
			// adddname
			// 
			this.adddname.Location = new System.Drawing.Point(80, 6);
			this.adddname.Name = "adddname";
			this.adddname.Size = new System.Drawing.Size(96, 20);
			this.adddname.TabIndex = 3;
			this.adddname.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "Device Name";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(192, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Port";
			// 
			// adddevice
			// 
			this.adddevice.Location = new System.Drawing.Point(293, 6);
			this.adddevice.Name = "adddevice";
			this.adddevice.Size = new System.Drawing.Size(56, 20);
			this.adddevice.TabIndex = 5;
			this.adddevice.Text = "Add";
			this.adddevice.Click += new System.EventHandler(this.adddevice_Click);
			// 
			// deviceslist
			// 
			this.deviceslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.columnHeader1,
																						  this.columnHeader2});
			this.deviceslist.FullRowSelect = true;
			this.deviceslist.Location = new System.Drawing.Point(8, 8);
			this.deviceslist.Name = "deviceslist";
			this.deviceslist.Size = new System.Drawing.Size(240, 104);
			this.deviceslist.TabIndex = 0;
			this.deviceslist.View = System.Windows.Forms.View.Details;
			this.deviceslist.SelectedIndexChanged += new System.EventHandler(this.deviceslist_SelectedIndexChanged);
			// 
			// b_rmdevice
			// 
			this.b_rmdevice.Location = new System.Drawing.Point(264, 24);
			this.b_rmdevice.Name = "b_rmdevice";
			this.b_rmdevice.Size = new System.Drawing.Size(72, 72);
			this.b_rmdevice.TabIndex = 6;
			this.b_rmdevice.Text = "Remove";
			this.b_rmdevice.Click += new System.EventHandler(this.b_rmdevice_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Device Name";
			this.columnHeader1.Width = 143;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Port";
			// 
			// MachineSettings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(374, 404);
			this.ControlBox = false;
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.b_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MachineSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "LocalMachine Settings";
			this.Load += new System.EventHandler(this.MachineSettings_Load);
			((System.ComponentModel.ISupportInitialize)(this.matmemsize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.threadstacksize)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void MachineSettings_Load(object sender, System.EventArgs e)
		{
			LoadConfigs(Application.StartupPath + "\\Config.xml");	
		}
		private void LoadConfigs(string filename){
			
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
	
//			MessageBox.Show( doc.SelectSingleNode("Configurations").NodeType.ToString());
			try
			{
				matmemsize.Value = int.Parse((doc.SelectSingleNode("Configurations/MatrixMemorySize").InnerText));
			}
			catch(Exception e)
			{
				matmemsize.Value=20;
				MessageBox.Show("There are some errors in config file.","Settings",
					MessageBoxButtons.OK,MessageBoxIcon.Information);		
			}

			try
			{
				threadstacksize.Value = int.Parse((doc.SelectSingleNode("Configurations/ThreadStackSize").InnerText));
			}
			catch(Exception e)
			{
				threadstacksize.Value=20;
				MessageBox.Show("There are some errors in config file.","Settings",
					MessageBoxButtons.OK,MessageBoxIcon.Information);		
			}

			XmlNodeList prs = doc.SelectNodes("Configurations/RemoteProcessor");

			processorslist.Items.Clear();
			foreach(XmlNode tmpnode in prs){
				try
				{
					ListViewItem tmpitm = processorslist.Items.Add(
											tmpnode.SelectSingleNode("IPAddress").InnerText);
					tmpitm.SubItems.Add(tmpnode.SelectSingleNode("Port").InnerText);
				}
				catch(Exception e){
					MessageBox.Show(e.Message ,"Settings",
								MessageBoxButtons.OK,MessageBoxIcon.Information);		
				}
			}

		
			prs = doc.SelectNodes("Configurations/ExternalDevice");

			deviceslist.Items.Clear();
			foreach(XmlNode tmpnode in prs)
			{
				try
				{
					ListViewItem tmpitm = deviceslist.Items.Add(
						tmpnode.SelectSingleNode("Name").InnerText);
					tmpitm.SubItems.Add(tmpnode.SelectSingleNode("Port").InnerText);
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message ,"Settings",
						MessageBoxButtons.OK,MessageBoxIcon.Information);		
				}
			}
		}

		private void b_cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void b_add_Click(object sender, System.EventArgs e)
		{
			addport.Text =addport.Text.Trim();
			addipaddress.Text =addipaddress.Text.Trim();
			if(addport.Text!= "" && addipaddress.Text!="")
			{
				try
				{
					IPAddress.Parse(addipaddress.Text);
					addport.Text= uint.Parse(addport.Text).ToString();

					ListViewItem tmp= processorslist.Items.Add(addipaddress.Text);
					tmp.SubItems.Add(addport.Text);
				}
				catch(Exception exc)
				{
					MessageBox.Show("Invalid IpAddress or port number.","Settings",MessageBoxButtons.OK,MessageBoxIcon.Error);			
				}
			}
		}

		private void b_remove_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem itm  in processorslist.SelectedItems){
				processorslist.Items.Remove(itm);
			}
		}

		private void b_ok_Click(object sender, System.EventArgs e)
		{
			
			string s;

			s = "<Configurations>\n";
			s +="<MatrixMemorySize>" + matmemsize.Value.ToString() + "</MatrixMemorySize>\n";
			s +="<ThreadStackSize>" + threadstacksize.Value.ToString() + "</ThreadStackSize>\n";

			foreach(ListViewItem itm in processorslist.Items){
				s +="<RemoteProcessor><IPAddress>" + itm.Text + "</IPAddress>";
				s +="<Port>" + itm.SubItems[1].Text + "</Port></RemoteProcessor>\n";
			}

			foreach(ListViewItem itm in deviceslist.Items)
			{
				s +="<ExternalDevice><Name>" + itm.Text + "</Name>";
				s +="<Port>" + itm.SubItems[1].Text + "</Port></ExternalDevice>\n";
			}
			s += "</Configurations>";
			
			//FileStream fs = File.Open(,FileMode.Create,FileAccess.Write);
			StreamWriter stm = new StreamWriter(Application.StartupPath + "\\Config.xml",false);
			stm.Write(s);
			stm.Close();
			
			this.Close();
		}

		private void processorslist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(processorslist.SelectedItems.Count>0){
				addipaddress.Text = processorslist.SelectedItems[0].Text;			
				addport.Text = processorslist.SelectedItems[0].SubItems[1].Text;			
			}
		}

		private void deviceslist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(deviceslist.SelectedItems.Count>0)
			{
				adddname.Text =deviceslist.SelectedItems[0].Text;			
				adddport.Text = deviceslist.SelectedItems[0].SubItems[1].Text;			
			}
		}

		private void adddevice_Click(object sender, System.EventArgs e)
		{
			adddport.Text =adddport.Text.Trim();
			adddname.Text =adddname.Text.Trim();
			if(adddport.Text!= "" && adddname.Text!="")
			{
				
				adddport.Text= uint.Parse(adddport.Text).ToString();

				ListViewItem tmp= deviceslist.Items.Add(adddname.Text);
				tmp.SubItems.Add(adddport.Text);
			}
		}

		private void b_rmdevice_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem itm  in deviceslist.SelectedItems)
			{
				deviceslist.Items.Remove(itm);
			}
		}
	}
}
