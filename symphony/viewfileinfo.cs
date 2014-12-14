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
    public partial class viewfileinfo : Form
    {
        public viewfileinfo()
        {
            InitializeComponent();
        }
        public string[] attr = new string[15];
        private void viewfileinfo_Load(object sender, EventArgs e)
        {
            txtLocation.Text = attr[12];
            txtAlbum.Text = attr[5];
            txtAlbumArtist.Text = attr[6];
            txtArtist.Text = attr[4];
            txtBPM.Text = attr[2];
            txtComposer.Text = attr[10];
            txtDisc.Text = attr[1];
            txtPublisher.Text = attr[11];
            txtTitle.Text = attr[3];
            txtTrack.Text = attr[0];
            txtYear.Text = attr[7];
            listbxComment.Text = attr[9];
            cmbGenre.Text = attr[8];
        }
    }
}
