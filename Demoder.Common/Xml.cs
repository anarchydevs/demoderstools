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
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Demoder.Common
{
	public static class Xml
	{
		public static class Serialize
		{
			/// <summary>
			/// Serializes an object into an already opened stream
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="stream"></param>
			/// <param name="obj"></param>
			public static bool stream<T>(Stream stream, T obj, bool closestream)
			{
				if (stream == null) throw new ArgumentNullException("stream");
				if (obj == null) throw new ArgumentNullException("obj");
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					serializer.Serialize(stream, obj);
					if (closestream) stream.Close();
					return true;
				}
				catch (Exception ex)
				{
					if (closestream && stream != null) stream.Close();
					return false;
				}
			}
			/// <summary>
			/// Serialize a class to a file
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="path"></param>
			/// <param name="obj"></param>
			/// <returns></returns>
			public static bool file<T>(string path, T obj)
			{
				if (path == null) throw new ArgumentNullException("path");
				if (obj == null) throw new ArgumentNullException("obj");
				MemoryStream ms = new MemoryStream();
				FileStream fs = null;
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					serializer.Serialize(ms, obj); //Serialize into memory

					fs = new FileStream(path, FileMode.Create);
					ms.WriteTo(fs);
					if (fs != null) fs.Close();
					if (ms != null) ms.Close();
					return true;
				}
				catch (Exception ex)
				{
					if (fs != null) fs.Close();
					if (ms != null) ms.Close();
					return false;
				}
			}
		}


		public static class Deserialize
		{
			public static T stream<T>(Stream stream, bool closestream)
			{
				if (stream == null) throw new ArgumentNullException("stream");
				if (closestream == null) throw new ArgumentNullException("closestream");
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					T obj = (T)serializer.Deserialize(stream);
					if (stream != null && closestream) stream.Close();
					return obj;
				}
				catch (Exception ex)
				{
					if (stream != null && closestream) stream.Close();
					return default(T);
				}
			}

			public static T file<T>(string path)
			{
				if (path == null) throw new ArgumentNullException("path");
				FileStream stream = null;
				try
				{
					stream = File.OpenRead(path);
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					T obj = (T)serializer.Deserialize(stream);
					if (stream != null) stream.Close();
					return obj;
				}
				catch (Exception ex)
				{
					if (stream != null) stream.Close();
					return default(T);
				}
			}
		}
	}
}