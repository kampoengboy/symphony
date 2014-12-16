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
        public string[] song = new string[2];
        public List<Tuple<string,string> > track = new List<Tuple<string,string> >();
        public int idx;
        private void jumptotrack_Load(object sender, EventArgs e)
        {
            for(int i=0;i<track.Count;i++)
            {
                string[] saLvwItem = new string[2];
                saLvwItem[0] = track[i].Item1;
                saLvwItem[1] = track[i].Item2;
                ListViewItem lvi = new ListViewItem(saLvwItem);
                listView1.Items.Add(lvi);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            idx = listView1.SelectedItems[0].Index;
            state = true;
            this.Close();
        }
    }
}
