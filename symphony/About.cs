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
            label4.Text = "Symphony is protected by trademark and other pending or existing intellectual property rights in";
            label5.Text = "Indonesia and other countries/regions";
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox1.Width - 3, pictureBox1.Height - 3);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;

            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox2.Width - 3, pictureBox2.Height - 3);
            rg = new Region(gp);
            pictureBox2.Region = rg;

            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox3.Width - 3, pictureBox3.Height - 3);
            rg = new Region(gp);
            pictureBox3.Region = rg;

            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox4.Width - 3, pictureBox4.Height - 3);
            rg = new Region(gp);
            pictureBox4.Region = rg;
        }
    }
}
