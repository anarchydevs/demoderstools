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
using System.Text;
using System.Xml.Serialization;

namespace Demoders_Patcher.DataClasses
{
	/// <summary>
	/// Class describing the local version of an updatedefinition.
	/// </summary>
	public class UpdateDefinitions
	{
		/// <summary>
		/// UNIX timestamp of when this definition was last syncronized.
		/// </summary>
		[XmlAttribute("timestamp")]
		public Int64 TimeStamp = 0; 
		[XmlElement("UpdateDefinition")]
		public List<UpdateDefinition> Definitions = new List<UpdateDefinition>();

        public static UpdateDefinitions operator + (UpdateDefinitions uds1, UpdateDefinitions uds2) {
            foreach (UpdateDefinition ud2 in uds2.Definitions)
            {
                bool add = true;
                foreach (UpdateDefinition ud1 in uds1.Definitions)
                {
                    if (ud1.GUID == ud2.GUID)
                    {
                        add=false;
                        break;
                    }
                }
                if (add)
                    uds1.Definitions.Add(ud2);
            }
            return uds1;
        }
	}
}
