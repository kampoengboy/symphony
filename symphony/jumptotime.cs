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
    public partial class jumptotime : Form
    {
        public jumptotime()
        {
            InitializeComponent();
        }
        public int duration;
        int m, s;
        public bool state = false;
        private void button1_Click(object sender, EventArgs e)
        {
            string xx = textBox1.Text,mtmp,stmp;
            mtmp = stmp = "";
            bool left = true;
            for (int i = 0; i < xx.Length; i++)
            {
                if (xx[i] == ':')
                {
                    left = false;
                    continue;
                }
                if (left)
                    mtmp += xx[i];
                else
                    stmp += xx[i];
            }
            int m = Convert.ToInt32(mtmp);
            int s = Convert.ToInt32(stmp);
            duration = (m * 60) + s;
            state = true;
            this.Close();
        }

        private void jumptotime_Load(object sender, EventArgs e)
        {
            m = duration / 60;
            s = duration % 60;
            if(m<10)
            {
                if(s<10)
                    textBox1.Text = "0" + m + ":0" + s;
                else
                    textBox1.Text = "0" + m + ":" + s;
            }
            else
            {
                if (s < 10)
                    textBox1.Text = m + ":0" + s;
                else
                    textBox1.Text = m + ":" + s;
            }
        }

        private void jumptotime_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
