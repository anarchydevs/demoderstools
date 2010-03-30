/*
MIT Licence
Copyright (c) 2010 Demoder <demoder@flw.nu> (project: https://sourceforge.net/projects/demoderstools/)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Demoder.Common;
using Demoder.MapCompiler;
using Demoder.MapCompiler.xml;
using Demoder.MapCompiler.Events;

namespace AOMC
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		#region Compiler
		#region Backgroundworker thread
		private void _bw_Compiler_DoWork(object sender, DoWorkEventArgs e)
		{
			//Make a copy of compiler configuration that we pass along.
			CompilerConfig cc = new CompilerConfig();
			cc.MaxSlicerThreads = Program.Config_Compiler.MaxSlicerThreads;
			cc.MaxWorkerThreads = Program.Config_Compiler.MaxWorkerThreads;
			cc.singlethreaded = Program.Config_Compiler.singlethreaded;
			//Make a copy of map configuration that we pass along.
			MapConfig mc = Program.Config_Map.Copy();

			Compiler compiler = new Compiler(cc);
			compiler.eventDebug += new DebugEventHandler(this.handleDebugEvent);
			compiler.eventImageLoader += new StatusReportEventHandler(this.handleImageLoaderEvent);
			compiler.eventImageSlicer += new StatusReportEventHandler(this.handleImageSlicerEvent);
			compiler.eventWorker += new StatusReportEventHandler(this.handleWorkerEvent);
			compiler.eventAssembler += new StatusReportEventHandler(this.handleAssemblerEvent);
			compiler.Compile(mc);
			compiler.ClearEvents();
		}

		#region worker events
		private void handleDebugEvent(Compiler compiler, DebugEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp;
			kvp = new KeyValuePair<BWReportProgress, string>(BWReportProgress.Debug, String.Format("[{0}] {1}", e.Source, e.Message));
			this._bw_Compiler.ReportProgress(0, kvp);
		}

		private void handleImageLoaderEvent(Compiler compiler, StatusReportEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp;
			kvp = new KeyValuePair<BWReportProgress, string>(BWReportProgress.ImageLoader, e.Message);
			this._bw_Compiler.ReportProgress(e.Percent, kvp);
		}

		private void handleImageSlicerEvent(Compiler compiler, StatusReportEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp;
			kvp = new KeyValuePair<BWReportProgress, string>(BWReportProgress.ImageSlicer, e.Message);
			this._bw_Compiler.ReportProgress(e.Percent, kvp);
		}

		private void handleWorkerEvent(Compiler compiler, StatusReportEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp;
			kvp = new KeyValuePair<BWReportProgress, string>(BWReportProgress.Worker, e.Message);
			this._bw_Compiler.ReportProgress(e.Percent, kvp);
		}

		private void handleAssemblerEvent(Compiler compiler, StatusReportEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp;
			kvp = new KeyValuePair<BWReportProgress, string>(BWReportProgress.Assembler, e.Message);
			this._bw_Compiler.ReportProgress(e.Percent, kvp);
		}

		#endregion worker events
		#endregion background worker thread

		public enum BWReportProgress
		{
			Debug,
			ImageLoader,
			ImageSlicer,
			Worker,
			Assembler
		}

		#region Application thread
		//Worker done
		private void _bw_Compiler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.button_docompile.Enabled = true;
		}

		/// <summary>
		/// Handle all messages from the worker thread.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _bw_Compiler_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			KeyValuePair<BWReportProgress, string> kvp = (KeyValuePair<BWReportProgress, string>)e.UserState;
			switch (kvp.Key)
			{
				case BWReportProgress.Debug:
					this._compiler_debugmessages.AppendText((string)kvp.Value + "\r\n");
					break;
				case BWReportProgress.ImageLoader:
					this.progressBar_imageloader.Value = e.ProgressPercentage;
					this.statuslabel_imageloader.Text = kvp.Value;
					break;
				case BWReportProgress.ImageSlicer:
					this.progressBar_imageslicer.Value = e.ProgressPercentage;
					this.statuslabel_imageslicer.Text = kvp.Value;
					break;
				case BWReportProgress.Worker:
					this.progressBar_worker.Value = e.ProgressPercentage;
					this.statuslabel_worker.Text = kvp.Value;
					break;
				case BWReportProgress.Assembler:
					this.progressBar_assembler.Value = e.ProgressPercentage;
					this.statuslabel_assembler.Text = kvp.Value;
					break;
			}
		}
		#endregion application thread


		private void button_docompile_Click(object sender, EventArgs e)
		{
			this.button_docompile.Enabled = false;
			this._compiler_debugmessages.Text = "";
			this.progressBar_assembler.Value = 0;
			this.progressBar_worker.Value = 0;
			this.progressBar_imageloader.Value = 0;
			this.progressBar_imageslicer.Value = 0;
			if (!this._bw_Compiler.IsBusy)
			{
				this._bw_Compiler.RunWorkerAsync();
			}
		}
		#endregion background worker


		#region Map configuration interface


		#endregion

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompilerOptions co = new CompilerOptions();
			DialogResult dr = co.ShowDialog();
			if (dr == DialogResult.OK)
				Xml.Serialize.file<CompilerConfig>(string.Format("{0}compiler_config.xml", Program.ConfigPath), Program.Config_Compiler);
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			_map_assemblymethod.Items.Clear();

			foreach (string am in Enum.GetNames(typeof(MapConfig.AssemblyMethod)))
			{
				_map_assemblymethod.Items.Add(am);
			}
			if (_map_assemblymethod.Items.Count > 0)
				_map_assemblymethod.SelectedIndex = 0;

			this.LoadMapConfigValues();
			Program.Config_Map_Changed = false;
			this._HelperBox.Text = Properties.Resources.help_MapInfo;
		}

		private void LoadMapConfigValues()
		{
			this._map_name.Text = Program.Config_Map.Name;
			//Version
			this._map_version_major.Value = Program.Config_Map.Version.Major;
			this._map_version_minor.Value = Program.Config_Map.Version.Minor;
			this._map_version_build.Value = Program.Config_Map.Version.Build;

			this._map_subdirectory.Text = Program.Config_Map.MapDir;
			this._map_assemblymethod.SelectedIndex = _map_assemblymethod.Items.IndexOf(Program.Config_Map.Assembler.ToString());
			this._map_texturesize.Value = Program.Config_Map.TextureSize;

			//Images
			this._imagelist.Items.Clear();
			foreach (LoadImage li in Program.Config_Map.Images)
			{
				ListViewItem lvi = new ListViewItem(new string[] { li.name, li.path });
				this._imagelist.Items.Add(lvi);
			}
			//Worker tasks
			this._WorkerTasks.Items.Clear();
			foreach (WorkTask wt in Program.Config_Map.WorkerTasks)
			{
				List<string> workentries = new List<string>(wt.workentries.Count);
				foreach (string wl in wt.workentries)
					workentries.Add(string.Format("{0}", wl));

				this._WorkerTasks.Items.Add(new ListViewItem(new string[] { wt.workname, wt.maprect, string.Join(", ", workentries.ToArray()) }));
			}

			//Map versions
			this._MapVersions.Items.Clear();
			foreach (TxtFile tf in Program.Config_Map.TxtFiles)
			{
				this._MapVersions.Items.Add(new ListViewItem(new string[] { tf.File, tf.Type.ToString(), tf.Name, tf.CoordsFile, string.Join(", ",tf.Layers.ToArray()) }));
			}
			Program.Config_Map_Changed = true;
		}

		#region _images context menu
		private void images_Contextmenu_Opening(object sender, CancelEventArgs e)
		{
			images_Contextmenu.Items[images_Contextmenu.Items.IndexOfKey("imagesContextMenu_Add")].Enabled = true;
			if (_imagelist.SelectedItems.Count >= 1)
			{
				images_Contextmenu.Items[images_Contextmenu.Items.IndexOfKey("imagesContextMenu_Edit")].Enabled = true;
				images_Contextmenu.Items[images_Contextmenu.Items.IndexOfKey("imagesContextMenu_Remove")].Enabled = true;
			}
			else
			{
				images_Contextmenu.Items[images_Contextmenu.Items.IndexOfKey("imagesContextMenu_Edit")].Enabled = false;
				images_Contextmenu.Items[images_Contextmenu.Items.IndexOfKey("imagesContextMenu_Remove")].Enabled = false;
			}

		}
		private void imagesContextMenu_Add_Click(object sender, EventArgs e)
		{
			contextmenuWindows.imagelistModifyEntry ime = new contextmenuWindows.imagelistModifyEntry();
			ime.Text = "Add image...";
			ime._ok.Text = "Add";
			DialogResult dr = ime.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					foreach (LoadImage li in Program.Config_Map.Images)
					{
						if (li.path == ime._path.Text)
						{
							MessageBox.Show("Image path already in list", string.Format("Name: {0}", li.name), MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						else if (li.name == ime._name.Text)
						{
							MessageBox.Show("Image name already in list", string.Format("Name: {0}", li.name), MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}

					Program.Config_Map.Images.Add(new LoadImage(ime._name.Text, ime._path.Text));
					this.LoadMapConfigValues();
					break;
			}
		}
		private void imagesContextMenu_Edit_Click(object sender, EventArgs e)
		{
			contextmenuWindows.imagelistModifyEntry ime = new contextmenuWindows.imagelistModifyEntry();
			foreach (ListViewItem lvi in _imagelist.SelectedItems)
			{
				ime.Text = "Edit image...";
				ime._ok.Text = "Apply";
				ime._name.Text = lvi.Text;
				ime._path.Text = lvi.SubItems[1].Text;
				bool showagain;
				do
				{
					showagain = false;
					DialogResult dr = ime.ShowDialog();
					switch (dr)
					{
						case DialogResult.OK:
							foreach (LoadImage li in Program.Config_Map.Images)
							{
								if (li.name == ime._name.Text && ime._name.Text != lvi.Text)
								{
									MessageBox.Show("Image name already in list", string.Format("Name: {0}", li.name), MessageBoxButtons.OK, MessageBoxIcon.Error);
									showagain = true;
									break;
								}
								if (li.path == ime._path.Text && ime._path.Text != lvi.SubItems[1].Text)
								{
									MessageBox.Show("Image path already in list", string.Format("Name: {0}", li.name), MessageBoxButtons.OK, MessageBoxIcon.Error);
									showagain = true;
									break;
								}
							}
							if (!showagain)
							{
								foreach (LoadImage li in Program.Config_Map.Images)
									if (li.name == lvi.Text)
									{
										Program.Config_Map.Images.Remove(li);
										break;
									}
								Program.Config_Map.Images.Add(new LoadImage(ime._name.Text, ime._path.Text));
							}
							break;
					}
				} while (showagain);
			}
			this.LoadMapConfigValues();
		}
		private void imagesContextMenu_Remove_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in _imagelist.SelectedItems)
			{
				foreach (LoadImage li in Program.Config_Map.Images)
					if (li.name == lvi.Text)
					{
						Program.Config_Map.Images.Remove(li);
						break;
					}
			}
			this.LoadMapConfigValues();
		}
		#endregion

		#region _worker taks context menu

		private void workerTasks_ContextMenu_Opening(object sender, CancelEventArgs e)
		{
			workerTasks_ContextMenu.Items[workerTasks_ContextMenu.Items.IndexOfKey("workerTasks_ContextMenu_Add")].Enabled = true;
			if (_WorkerTasks.SelectedItems.Count >= 1)
			{
				workerTasks_ContextMenu.Items[workerTasks_ContextMenu.Items.IndexOfKey("workerTasks_ContextMenu_Edit")].Enabled = true;
				workerTasks_ContextMenu.Items[workerTasks_ContextMenu.Items.IndexOfKey("workerTasks_ContextMenu_Remove")].Enabled = true;
			}
			else
			{
				workerTasks_ContextMenu.Items[workerTasks_ContextMenu.Items.IndexOfKey("workerTasks_ContextMenu_Edit")].Enabled = false;
				workerTasks_ContextMenu.Items[workerTasks_ContextMenu.Items.IndexOfKey("workerTasks_ContextMenu_Remove")].Enabled = false;
			}
		}

		private void workerTasks_ContextMenu_Add_Click(object sender, EventArgs e)
		{
			contextmenuWindows.workertaskModifyEntry wtme = new AOMC.contextmenuWindows.workertaskModifyEntry();
			wtme.images = Program.Config_Map.Images;
			wtme.Text = "Add work task";
			wtme.button_ok.Text = "Add";
			bool error;
			do
			{
				error = false;
				DialogResult dr = wtme.ShowDialog();

				switch (dr)
				{
					case DialogResult.OK:
						if (Program.Config_Map.ContainsWorkTask(wtme.workerTask.workname))
						{
							MessageBox.Show(string.Format("Work name already exists: {0}", wtme.workerTask.workname), "Duplicate work name", MessageBoxButtons.OK, MessageBoxIcon.Error);
							error = true;
							continue;
						}
						Program.Config_Map.WorkerTasks.Add(wtme.workerTask);
						break;
				}
			} while (error);
		}

		private void workerTasks_ContextMenu_Edit_Click(object sender, EventArgs e)
		{
			if (this._WorkerTasks.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._WorkerTasks.SelectedItems)
				{
					contextmenuWindows.workertaskModifyEntry wtme = new AOMC.contextmenuWindows.workertaskModifyEntry();
					wtme.images = Program.Config_Map.Images;
					WorkTask oldtask = Program.Config_Map.GetWorkTask(lvi.Text);
					//Don't reference, in case there is an error and the user decides to abort.
					wtme.workerTask.maprect = oldtask.maprect;
					wtme.workerTask.workname = oldtask.workname;
					foreach (string wl in oldtask.workentries)
					{
						wtme.workerTask.workentries.Add(wl);
					}
					//End of preventing referencing.
					wtme.Text = "Edit work task";
					wtme.button_ok.Text = "Apply";
					bool error;
					do
					{
						error = false;
						DialogResult dr = wtme.ShowDialog();

						switch (dr)
						{
							case DialogResult.OK:
								if ((wtme.workerTask.workname != oldtask.workname) 
									&& Program.Config_Map.ContainsWorkTask(wtme.workerTask.workname))
								{
									MessageBox.Show(string.Format("Work name already exists: {0}", wtme.workerTask.workname), "Duplicate work name", MessageBoxButtons.OK, MessageBoxIcon.Error);
									error = true;
									continue;
								}
								Program.Config_Map.ReplaceWorkTask(oldtask.workname, wtme.workerTask);
								break;
						}
					} while (error);
				}
			}
			this.LoadMapConfigValues();
		}

		private void workerTasks_ContextMenu_Remove_Click(object sender, EventArgs e)
		{
			if (this._WorkerTasks.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._WorkerTasks.SelectedItems)
				{
					Program.Config_Map.RemoveWorkTask(lvi.Text);
				}
				this.LoadMapConfigValues();
			}

		}
		
		#endregion

		#region Map Versions context menu
		private void mapVersions_ContextMenu_Opening(object sender, CancelEventArgs e)
		{
			mapVersions_ContextMenu.Items[mapVersions_ContextMenu.Items.IndexOfKey("mapVersions_ContextMenu_Add")].Enabled = true;
			if (_MapVersions.SelectedItems.Count >= 1)
			{
				mapVersions_ContextMenu.Items[mapVersions_ContextMenu.Items.IndexOfKey("mapVersions_ContextMenu_Edit")].Enabled = true;
				mapVersions_ContextMenu.Items[mapVersions_ContextMenu.Items.IndexOfKey("mapVersions_ContextMenu_Remove")].Enabled = true;
			}
			else
			{
				mapVersions_ContextMenu.Items[mapVersions_ContextMenu.Items.IndexOfKey("mapVersions_ContextMenu_Edit")].Enabled = false;
				mapVersions_ContextMenu.Items[mapVersions_ContextMenu.Items.IndexOfKey("mapVersions_ContextMenu_Remove")].Enabled = false;
			}
		}
		private void mapVersions_ContextMenu_Add_Click(object sender, EventArgs e)
		{
			contextmenuWindows.mapversions_ModifyEntry ilme = new AOMC.contextmenuWindows.mapversions_ModifyEntry();
				TxtFile txt = new TxtFile();
				//Add layers
				ilme._layers.Items.Clear();
				foreach (string l in txt.Layers)
					ilme._layers.Items.Add(l);
				ilme._button_ok.Text = "Apply";
				bool error;
				do
				{
					error = false;
					DialogResult dr = ilme.ShowDialog();
					switch (dr)
					{
						case DialogResult.OK:
							if (Program.Config_Map.ContainsTxtFile(ilme._file.Text))
							{
								MessageBox.Show(string.Format("Text file already exists: {0}", ilme._file.Text), "Duplicate text file", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error = true;
								continue;
							}
							txt.CoordsFile = ilme._coordsfile.Text;
							txt.File = ilme._file.Text;
							txt.Name = ilme._name.Text;
							txt.Layers = new List<string>();
							txt.Type = ilme.mapType;
							foreach (ListViewItem lvi2 in ilme._layers.Items)
								txt.Layers.Add(lvi2.Text);

							break;
					}
					Program.Config_Map.TxtFiles.Add(txt);
				} while (error);
		}

		private void mapVersions_ContextMenu_Edit_Click(object sender, EventArgs e)
		{
			if (this._MapVersions.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._MapVersions.SelectedItems)
				{
					contextmenuWindows.mapversions_ModifyEntry ilme = new AOMC.contextmenuWindows.mapversions_ModifyEntry();
					TxtFile txt = Program.Config_Map.GetTxtFile(lvi.Text);
					if (txt != null)
					{
						ilme._name.Text = txt.Name;
						ilme._file.Text = txt.File;
						ilme._coordsfile.Text = txt.CoordsFile;
						ilme.mapType = txt.Type;
						//Add layers
						ilme._layers.Items.Clear();
						foreach (string l in txt.Layers)
							ilme._layers.Items.Add(l);
						ilme._button_ok.Text = "Apply";
						bool error;
						do
						{
							error = false;
							DialogResult dr = ilme.ShowDialog();
							switch (dr)
							{
								case DialogResult.OK:
									if (ilme._file.Text.ToLower() != txt.File.ToLower()
										&& Program.Config_Map.ContainsTxtFile(ilme._file.Text))
									{
										MessageBox.Show(string.Format("Text file already exists: {0}", ilme._file.Text), "Duplicate text file", MessageBoxButtons.OK, MessageBoxIcon.Error);
										error = true;
										continue;
									}
									//txt is referenced, so values will be replaced automatically.
									txt.CoordsFile = ilme._coordsfile.Text;
									txt.File = ilme._file.Text;
									txt.Name = ilme._name.Text;
									txt.Layers=new List<string>();
									txt.Type = ilme.mapType;
									foreach (string lvi2 in ilme._layers.Items)
										txt.Layers.Add(lvi2);
									break;
							}
						} while (error);
						this.LoadMapConfigValues();
					}
				}
			}
		}

		private void mapVersions_ContextMenu_Remove_Click(object sender, EventArgs e)
		{
			if (this._MapVersions.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._MapVersions.SelectedItems)
				{
					Program.Config_Map.RemoveTxtFile(lvi.Text);
				}
				this.LoadMapConfigValues();
			}
		}

		#endregion

		#region File menu
		//new
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Program.Config_Map_Changed)
				this.AskToSave();
			Program.Config_Map = new MapConfig();
			this.LoadMapConfigValues();
			Program.Config_Map_Changed = false;
		}
		//open
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Program.Config_Map_Changed)
				this.AskToSave();
			DialogResult dr = _OpenMapConfig.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					Program.Config_Map = Demoder.Common.Xml.Deserialize.file<MapConfig>(_OpenMapConfig.FileName);
					this.LoadMapConfigValues();
					Program.Config_Map_Changed = false;
					break;
			}
		}
				
		//save
		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveMapConfigAs(null);
		}
		//Save as
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveMapConfig();
		}
		//exit
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		#endregion		

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox ab = new AboutBox();
			ab.ShowDialog();
		}

		#region help system
		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			int index = ((TabControl)sender).SelectedIndex;
			switch (((TabControl)sender).TabPages[index].Name)
			{
				case "tabPage_MapInfo":
					this._HelperBox.Text = Properties.Resources.help_MapInfo;
					break;
				case "tabPage_Images":
					this._HelperBox.Text = Properties.Resources.help_Images;
					break;
				case "tabPage_WorkerTasks":
					this._HelperBox.Text = Properties.Resources.help_Workertasks;
					break;
				case "tabPage_MapVersions":
					this._HelperBox.Text = Properties.Resources.help_Mapversions;
					break;
				case "tabPage_Compile":
					this._HelperBox.Text = Properties.Resources.help_Compile;
					break;
			}
		}
		#endregion

		private void _ConfigChanged(object sender, EventArgs e)
		{
			Program.Config_Map_Changed = true;
		}

		private DialogResult SaveMapConfig()
		{
			DialogResult dr = DialogResult.Ignore;
			if (Program.MapConfigSavePath == string.Empty)
			{
				dr = this.SaveMapConfigAs(null);
			}
			else
			{
				Demoder.Common.Xml.Serialize.file<MapConfig>(Program.MapConfigSavePath, Program.Config_Map);
				Program.Config_Map_Changed = false;
			}
			return dr;
		}

		private DialogResult SaveMapConfigAs(string path)
		{
			if (path == null)
			{
				DialogResult dr = this._SaveMapConfig.ShowDialog();
				switch (dr)
				{
					case DialogResult.OK:
						Program.MapConfigSavePath = this._SaveMapConfig.FileName;
						Demoder.Common.Xml.Serialize.file<MapConfig>(Program.MapConfigSavePath, Program.Config_Map);
						Program.Config_Map_Changed = false;
						break;
				}
				return dr;
			}
			else
			{
				try
				{
					Demoder.Common.Xml.Serialize.file<MapConfig>(path, Program.Config_Map);
				}
				catch { }
				return DialogResult.OK;
			}
		}

		private DialogResult AskToSave()
		{
			DialogResult dr = MessageBox.Show("There are unsaved changes. Do you want to save them now?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			switch (dr)
			{
				case DialogResult.Yes:
					dr = this.SaveMapConfig();
					break;
			}
			return dr;
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Program.Config_Map_Changed)
			{
				switch (e.CloseReason)
				{

					case CloseReason.WindowsShutDown:
						this.SaveMapConfigAs(Program.ConfigPath + "sysshutdown_mapconfig.xml");
						break;
					case CloseReason.FormOwnerClosing:
					case CloseReason.UserClosing:
					case CloseReason.TaskManagerClosing:
					case CloseReason.ApplicationExitCall:
					case CloseReason.None:
					default:
						DialogResult dr = this.AskToSave();
						switch (dr)
						{
							case DialogResult.Cancel:
								e.Cancel = true;
								break;
						}
						break;
				}
			}
		}
	}
}
