using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace symphony
{
    public partial class jumptotrack : Form
    {
        public jumptotrack()
        {
            InitializeComponent();
        }
        public bool state = false;
        public List<string> track = new List<string>();
        public int idx;
        private void jumptotrack_Load(object sender, EventArgs e)
        {
            for(int i=0;i<track.Count;i++)
            {
                listBox1.Items.Add(track[i]);
            }
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            idx = listBox1.SelectedIndex;
            state = true;
            this.Close();
        }
    }
}
