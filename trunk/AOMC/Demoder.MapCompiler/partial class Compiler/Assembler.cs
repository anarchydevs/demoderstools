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
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using Demoder.MapCompiler.xml;
using Demoder.Common;

/* This file contains the Assembler part of the compiler */

namespace Demoder.MapCompiler
{
	public partial class Compiler
	{
		/// <summary>
		/// This assembles the final map.
		/// </summary>
		private void __threaded_Assembler()
		{
			this.debug("A", "Started");

			/*
			 * To assemble a map you need to know the following:
			 * - Which file is layer X located in
			 * - What is layer X's offset in that file
			 * - What relative file positions does each layer contain?
			 */
			byte[] headerimg_bytes;
			if (true)
			{
				if (this._CompilerConfig.IncludeHeader)
				{
					System.Reflection.AssemblyName callerassembly = System.Reflection.Assembly.GetEntryAssembly().GetName();
					System.Reflection.AssemblyName myassembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();

					string[] headerstring = new string[] {
					string.Format("Compiled with {0} v{1}",
						callerassembly.Name,
						callerassembly.Version),
					string.Format("    using {0} v{1}",
						myassembly.Name,
						myassembly.Version),
					string.Format("Map name: {0}", this._MapConfig.Name),
					string.Format("Compilation time: {0}-{1}-{2} {3}:{4}:{5}", 
						DateTime.UtcNow.Year, 
						DateTime.UtcNow.Month, 
						DateTime.UtcNow.Day, 
						DateTime.UtcNow.Hour, 
						DateTime.UtcNow.Minute, 
						DateTime.UtcNow.Second),
					"***** DECOMPILINGINFORMATION *****",
					"Please use AO-Skintool",
					"http://www.gridstream.org/forums/viewtopic.php?t=5217",
					"or AO-Basher to extract the images from this file."
					};
					Bitmap headerimg = new Bitmap(640, (int)System.Math.Ceiling((double)(10 * headerstring.Length)), PixelFormat.Format24bppRgb); //Image to add to the beginning of every binfile
					Graphics g = Graphics.FromImage(headerimg);
					for (int i = 0; i < headerstring.Length; i++)
						g.DrawString(headerstring[i], new Font(FontFamily.GenericMonospace, 8, FontStyle.Regular), Brushes.White, 2, (int)System.Math.Ceiling((double)(9 * i)));
					g.Dispose();

					/* Header section of the .bin file should always be 8192 bytes.
					 * This is to enable compatibility with file-replace patching by
					 * ensuring the first image slice always starts at the same byte. */
					int headerSize = 8192;
					MemoryStream ms_headerimg = new MemoryStream(headerSize);
					headerimg.Save(ms_headerimg, ImageFormat.Png);

					if (ms_headerimg.Length > headerSize) //If the header size is too large, remove header
						ms_headerimg = new MemoryStream(headerSize);

					while (ms_headerimg.Length < headerSize) //If the header size is too small, fill remainder with 0-bytes.
						ms_headerimg.WriteByte(0);


					headerimg_bytes = ms_headerimg.ToArray();
				}
				else { headerimg_bytes = null; }
			}

#warning fixme: This is NOT optimized for a multi-binfile enviorenment.
			//multibinfile: Grab what it has, spew out the binfiles as they become available.
			//Single binfile: Process like now.
			this.reportAssemblerStatus(0, "Started.");
			lock (this._Data_TxtFiles)
			{
				foreach (TxtFile task in this._Data_TxtFiles.Txts)
				{
					//Check if we have everything we need to do this work.
					foreach (string layer in task.Layers)
					{
						bool WaitForIt;
						do
						{
							WaitForIt = true; //Default to wait
							lock (this._Data_WorkerResults)
							{
								foreach (KeyValuePair<string, WorkerResult> kvp in this._Data_WorkerResults)
								{
									if (kvp.Value.LayerSliceInfo.ContainsKey(layer))
									{ //We have this layer. Don't wait for it.
										WaitForIt = false;
										continue;
									}
								}
							}
							if (WaitForIt) Thread.Sleep(100); //Sleep 100ms before we try again.
						} while (WaitForIt);
					}
				}
			}
			this.reportAssemblerStatus(10, "Haver everything we need.");
			this.debug("A", "Have everything we need.");
			//We have everything we need now.
			//Assemble based on work list.


			/*
			 * Move binfile into list of binfiles. (append to whatever is already there)
			 * If appending, bump FilePos values.
			 * 
			 */

			// Contains all the info you need about each layer.
			// It's to be used by the textfile generator.
			Dictionary<string, LayerInfo> layerInfo = new Dictionary<string, LayerInfo>();

			this.debug("A", "Preparing binfiles.");
			//Prepare the binfiles and layers.

			#region combined
			for (int i = 0; i < this._MapConfig.WorkerTasks.Count; i++)
			{
				WorkTask wtask = this._MapConfig.WorkerTasks[i];
				string binname=string.Empty;
				switch (this._MapConfig.Assembler)
				{
					case MapConfig.AssemblyMethod.Single:
						binname = this._MapConfig.BinName;
						break;
					case MapConfig.AssemblyMethod.Multi:
						binname = wtask.workname;
						break;
				}
				lock (this._Data_Binfiles)
					if (!this._Data_Binfiles.ContainsKey(binname))
					{ //If this binfile doesn't exist in the list, add it.
						this._Data_Binfiles.Add(binname,new MemoryStream());
						if (headerimg_bytes != null) //If we have a header image
							this._Data_Binfiles[binname].Write(headerimg_bytes, 0, headerimg_bytes.Length);
					}

				MemoryStream binfile = this._Data_Binfiles[binname]; //Temporary memorystream for binfile.
				long BinOffset = binfile.Length;

				lock (this._Data_WorkerResults)
				{
					WorkerResult wresult = this._Data_WorkerResults[wtask.workname]; //Worker result
					lock (this._Data_Binfiles)
						binfile.Write(wresult.Data, 0, wresult.Data.Length);
					//Data written to binfile stream... Now process file positions.
					foreach (KeyValuePair<string, List<long>> kvp in wresult.LayerPositionInfo)
					{
						List<long> filepos = new List<long>();
						foreach (long fpos in kvp.Value)
						{
							filepos.Add(fpos + BinOffset);
						}
						LayerInfo li = new LayerInfo(
							binname,
							filepos,
							wresult.LayerSliceInfo[kvp.Key],
							wresult.MapRect);
						layerInfo.Add(kvp.Key, li);
					}
				}
			}
			#endregion

			this.reportAssemblerStatus(75, "Binfiles prepared.");
					
			this.debug("A", string.Format("Writing {0} binfiles.", this._Data_Binfiles.Count));
			if (!Directory.Exists(this._MapConfig.OutputDirectory)) Directory.CreateDirectory(this._MapConfig.OutputDirectory);
			//Write the bin files.
			foreach (KeyValuePair<string, MemoryStream> kvp in this._Data_Binfiles)
			{
				try
				{
					FileStream fs = File.Open(this._MapConfig.OutputDirectory + Path.DirectorySeparatorChar + kvp.Key + ".bin", FileMode.Create);
					byte[] b = kvp.Value.ToArray();
					kvp.Value.Close();
					fs.Write(b, 0, b.Length);
					fs.Close();
					b = null;
					this._Data_Binfiles[kvp.Key].Dispose();
				}
				catch { }
			}
			this.reportAssemblerStatus(90, "Binfiles written to disk.");
			this.debug("A", string.Format("Writing {0} text files.", this._Data_TxtFiles.Count()));
			//Write the text files.
			TxtFile tf = null;
			FormatString sf;
			if (true)
			{
				Dictionary<string, object> dict = new Dictionary<string, object>();
				dict.Add("name", this._MapConfig.Name);
				dict.Add("binname", this._MapConfig.BinName);
				dict.Add("shortname", this._MapConfig.BinName); //Backwards compatibility added in 1.1
				dict.Add("version", this._MapConfig.Version.ToString());
				dict.Add("subfolder", this._MapConfig.MapDir);
				sf = new FormatString(dict);
			}
			do
			{
				if (this._Data_TxtFiles.Count() > 0)
					tf = this._Data_TxtFiles.Dequeue();
				else tf = null;
				if (tf != null)
				{
					if (!tf.CoordsFile.ToLower().EndsWith(".xml"))
						tf.CoordsFile += ".xml";
					string txtfile = String.Format("Name \"{0}\"\r\nType {1}\r\nCoordsFile {2}\r\n",
						sf.Format(tf.Name),
						tf.Type,
						sf.Format(tf.CoordsFile));
					foreach (string layer in tf.Layers)
					{
						LayerInfo li = layerInfo[layer];
						txtfile += string.Format("\r\nFile {0}{1}.bin", this._MapConfig.MapDir, li.InBinfile);
						txtfile += string.Format("\r\nTextureSize {0}\r\n", this._MapConfig.TextureSize);
						txtfile += string.Format("\r\nSize {0} {1}", (li.Sliceinfo.Width * this._MapConfig.TextureSize), (li.Sliceinfo.Height * this._MapConfig.TextureSize));
						txtfile += string.Format("\r\nTiles {0} {1}", li.Sliceinfo.Width, li.Sliceinfo.Height);
						txtfile += string.Format("\r\nMapRect {0}\r\n", li.MapRect);
						foreach (long filepos in li.FilePos)
						{
							txtfile += string.Format("FilePos {0}\r\n", filepos);
						}
					}
					if (!tf.File.ToLower().EndsWith(".txt")) //If the txtfile doesn't contain .txt, add it.
						tf.File += ".txt";
					File.WriteAllText(this._MapConfig.OutputDirectory + Path.DirectorySeparatorChar + sf.Format(tf.File), txtfile);
				}
			} while (tf != null);
			DateTime dt2 = DateTime.Now;
			this.reportAssemblerStatus(100, "Done.");
			this.debug("A", String.Format("Stopped after {0} seconds.", (dt2 - this.WorkerEndTime).TotalSeconds));
			this.reportComplete_Assembler();
			this._MRE_Assembler_Done.Set();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}
	}
}
