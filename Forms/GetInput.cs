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
    public partial class GetInput : Form
    {
        public string gameName;
        public GetInput(string label)
        {
            InitializeComponent();
            this.label1.Text = label;
        }

        private void confirmation_Click(object sender, EventArgs e)
        {
            gameName = label1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
