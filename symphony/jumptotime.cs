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
        public double duration;
        private void button1_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            home.duration = Convert.ToDouble(textBox1.Text);
            home.isJump = true;
            home.changeTime();
        }

        private void jumptotime_Load(object sender, EventArgs e)
        {
            textBox1.Text = duration.ToString();
        }
    }
}
