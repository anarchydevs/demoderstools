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

namespace Demoders_Patcher.Windows
{
	public partial class InitializeApplication : Form
	{
		private backgroundWorker_DoWork backgroundWorker = new backgroundWorker_DoWork();
		delegate void StringParameterDelegate(string value);
		delegate void IntParameterDelegate(int value);
		delegate void VoidParameterDelegate();
		delegate void DialogResultDelegate(DialogResult dr);
		private int completedTasks = 0;
		private int totalTasks = 0;
		string originalTitle = "Demoder's Patcher: Loading... ";

		public InitializeApplication()
		{
			InitializeComponent();
			this.Text = this.originalTitle;
			this.DialogResult = DialogResult.OK;
			this.backgroundWorker.QueueEmpty += new EventHandler(this.BackgroundWorker_QueueEmpty);
			this.backgroundWorker.BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
			this.backgroundWorker.WorkComplete += new RunWorkerCompletedEventHandler(this.BackgroundWorker_WorkComplete);
			
			this.backgroundWorker.Enq_LoadCentralUpdateDefinitions(false);
			this.backgroundWorker.Enq_LoadLocalUpdateDefinitions();
			this.backgroundWorker.Enq_CheckPatchStatus();
			this.totalTasks = 3;
		}

		private void BackgroundWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
		{
			this.completedTasks++;
			int percent = Demoder.Common.math.Percent(this.totalTasks, this.completedTasks);
			updateTitle(percent.ToString()+"%");
			updateBar(percent);
			KeyValuePair<bgw_tasktype, object> kvp = (KeyValuePair<bgw_tasktype, object>)e.Result;
			this.textBox_AddText(kvp.Key.ToString());
		}

		private void textBox_AddText(string text)
		{
			this.textBox1.Text += text + "\r\n";
		}

		private void ProgressInfo_Load(object sender, EventArgs e)
		{
			
		}

		void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			int oneTasksPercent = Demoder.Common.math.Percent(this.totalTasks, 1);
			int totalPercent = Demoder.Common.math.Percent(this.totalTasks, this.completedTasks);
			totalPercent += (int)Math.Round((double)oneTasksPercent / (double)100 * (double)totalPercent, 0);
			if (totalPercent <= this.progressBar1.Maximum && totalPercent >= this.progressBar1.Minimum)
			{
				updateBar(totalPercent);
				updateTitle(totalPercent.ToString()+"%");
			}
			string labelText = e.ProgressPercentage.ToString() + "%";
			this.progressBar_perwork.Value = e.ProgressPercentage;

			try
			{
				string text = e.UserState.ToString();
				if (!String.IsNullOrEmpty(text))
					this.textBox1.Text += text + "\r\n";
			}
			catch { }
		}

		private void updateTitle(string text) {
			this.Text = this.originalTitle + text;
		}

		private void updateBar(int value)
		{
			this.progressBar1.Value = value;
		}

		void BackgroundWorker_QueueEmpty(object sender, EventArgs e)
		{
			return;
			if (this.InvokeRequired)
				BeginInvoke(new VoidParameterDelegate(this.Close));
			else
				this.Close();
		}

		private void InitializeApplication_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.completedTasks == this.totalTasks)
				this.DialogResult = DialogResult.OK;
			else
				this.DialogResult = DialogResult.Cancel;
		}
	}
}
