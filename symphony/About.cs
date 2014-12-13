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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            label1.Text = "Symphony";
            label2.Text = "Version 0.1";
            label3.Text = "\u00A9 2014 Symphony. All rights reserved.";
            label4.Text = "Symphony and OthelloX is protected by trademark and other pending or existing intellectual property rights in";
            label5.Text = "Indonesia and other countries/regions";
        }
    }
}
