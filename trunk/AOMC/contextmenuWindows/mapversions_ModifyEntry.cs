using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Demoder.MapCompiler;
using Demoder.MapCompiler.xml;

namespace AOMC.contextmenuWindows
{
	public partial class mapversions_ModifyEntry : Form
	{
		public mapversions_ModifyEntry()
		{
			InitializeComponent();
		}

		internal MapTypeList mapType = MapTypeList.Rubika;

		private void mapversions_ModifyEntry_Load(object sender, EventArgs e)
		{
			this._mapType.Items.Clear();
			foreach (string n in Enum.GetNames(typeof(MapTypeList)))
			{
				this._mapType.Items.Add(n);
				if (n == mapType.ToString())
					this._mapType.SelectedIndex = (this._mapType.Items.Count - 1);
			}
			this._availableLayers.Items.Clear();
			foreach (string layer in Program.Config_Map.GetAllLayerNames()) {
				if (!this._layers.Items.Contains(layer))
					this._availableLayers.Items.Add(layer);
			}
		}

		private void _button_ok_Click(object sender, EventArgs e)
		{
			this.mapType = (MapTypeList)Enum.Parse(typeof(MapTypeList), this._mapType.Text, true);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void _button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}


		private void button_remlayer_Click(object sender, EventArgs e)
		{
			if (this._layers.SelectedIndex != -1)
			{
				string selectedLayer = this._layers.Items[this._layers.SelectedIndex].ToString();
				this._availableLayers.Items.Add(selectedLayer);
				this._layers.Items.Remove(selectedLayer);
			}
		}

		private void button_addlayer_Click(object sender, EventArgs e)
		{
			if (this._availableLayers.SelectedIndex != -1)
			{
				string selectedLayer = this._availableLayers.Items[this._availableLayers.SelectedIndex].ToString();
				this._availableLayers.Items.Remove(selectedLayer);
				this._layers.Items.Add(selectedLayer);
			}
		}
	}
}
