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
using System.IO;
using System.Text;
using Demoder.Patcher.DataClasses;
using Demoder.Common;
using Demoder.Patcher.Events;

namespace Demoder.Patcher
{
	public class DoPatch
	{
		#region events
		public event DownloadStatusReportEventHandler eventDownloadStatusReport;
		#endregion
		#region members
		private PatchServer _patchServer = null;
		private DirectoryInfo _temporaryDirectory = null;
		//Track status
		private bool _haveRun_PatchDistributions = false;
		private bool _patchSuccess = false;
		private bool _haveRun_InstallPatchedDistributions = false;
		#endregion
		public DoPatch(PatchServer PatchServer) 
			: this(PatchServer, 
			new DirectoryInfo(
				string.Format(
					"{1}{0}Demoder.Patcher", 
					Path.DirectorySeparatorChar, 
					Path.GetTempPath())))
		{
		}
		public DoPatch(PatchServer PatchServer, DirectoryInfo TemporaryDirectory)
		{
			this._patchServer = PatchServer;
			this._temporaryDirectory = TemporaryDirectory;
		}

		public bool PatchDistributions(DirectoryInfo BaseDir)
		{
			if (this._haveRun_PatchDistributions) throw new InvalidOperationException(".PatchDistributions() have already been run");
			this._haveRun_PatchDistributions = true;
			bool success = true;
			foreach (Distribution remoteDist in this._patchServer.Distributions)
			{
				DirectoryInfo tmpdir = new DirectoryInfo(this._temporaryDirectory.FullName +
				Path.DirectorySeparatorChar +
				remoteDist.DistType.ToString() +
				Path.DirectorySeparatorChar +
				remoteDist.DistributionName);

				UpdateDistribution ud = new UpdateDistribution(BaseDir, remoteDist, this._patchServer.download_locations);
				ud.eventDownloadStatusReport += new DownloadStatusReportEventHandler(this._reportDownloadStatus);
                ud.Start(this._temporaryDirectory);
				if (!ud.Success)
				{
					success = false;
					break;
				}
			}
			return success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="BaseInstallPath">For anything related to Anarchy Online, this is the path to the directory containing Anarchy.exe</param>
		public void InstallPatchedDistributions(DirectoryInfo BaseInstallPath)
		{
			//if (!this._haveRun_PatchDistributions) throw new InvalidOperationException(".PatchDistributions() must be run before .InstallPatchedDistributions()");
			//if (!this._patchSuccess) throw new InvalidOperationException("Patching must be successfull before installing the patches");
			if (this._haveRun_InstallPatchedDistributions) throw new InvalidOperationException(".InstallPatchedDistributions() have already been run");
			this._haveRun_InstallPatchedDistributions = true;
			
			foreach (Distribution remoteDist in this._patchServer.Distributions)
			{
				foreach (DistributionIndex.Dir dir in remoteDist.Directories)
				{
					dir.Sanitize();
					if (Distribution.IsBannedDirectory(remoteDist.DistType, dir.name)) continue;

					//Directory storing this distributions files
					DirectoryInfo srcdir = new DirectoryInfo(this._temporaryDirectory.FullName +
					Path.DirectorySeparatorChar +
					remoteDist.DistType.ToString() +
					Path.DirectorySeparatorChar +
					remoteDist.DistributionName +
					Path.DirectorySeparatorChar +
					dir.name);

					//Directory we're going to install to
					string dstDir = string.Format(
						"{1}{0}{2}",
						Path.DirectorySeparatorChar,
						Distribution.GetRelativeBaseDir(remoteDist.DistType, BaseInstallPath.FullName),
						dir.name);
					if (remoteDist.DistType != Distribution.DistributionType.Other)
					{
						Directory.Delete(dstDir, true);
					}
					srcdir.MoveTo(dstDir);
				}
			}
		}

		#region event handlers
		/// <summary>
		/// Relay download status event reports.
		/// </summary>
		/// <param name="e"></param>
		private void _reportDownloadStatus(DownloadStatusReportEventArgs e)
		{
			if (this.eventDownloadStatusReport != null)
				lock (this.eventDownloadStatusReport)
					this.eventDownloadStatusReport(e);
		}
		#endregion
	}
}
