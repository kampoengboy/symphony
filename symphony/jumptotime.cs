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
        public bool state = false;
        private void button1_Click(object sender, EventArgs e)
        {
            duration = Convert.ToDouble(textBox1.Text);
            state = true;
            this.Close();
        }

        private void jumptotime_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void jumptotime_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
