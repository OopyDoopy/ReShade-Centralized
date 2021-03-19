using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    public partial class WindowsStoreAppSelector : Form
    {
        public string selection;

        List<string> originalOptions;
        public WindowsStoreAppSelector(List<string> options)
        {
            InitializeComponent();
            originalOptions = options;
            foreach (string entry in options)
            {
                listBox1.Items.Add(entry);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            selection = listBox1.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            List<string> newListValues = new List<string>();
            //search original list given for searched text
            foreach (string item in originalOptions)
            {
                if (item.ToLower().Contains(searchBox.Text.ToLower()))
                {
                    newListValues.Add(item);
                }
            }
            //remove all options from listbox
            listBox1.Items.Clear();
            //populate listbox with matched items
            foreach (string item in newListValues)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}
