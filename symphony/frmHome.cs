using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace symphony
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }
        public string[] files, paths;
        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "MP3 Audio File(*.mp3)|*.mp3";
            open.FilterIndex = 1;
            open.RestoreDirectory = false;
            open.Multiselect = true;
            try
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = open.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameandPath = open.FileNames;
                            string[] filename = open.SafeFileNames;
                            for (int i = 0; i < open.SafeFileNames.Count(); i++)
                            {
                                string[] saLvwItem = new string[2];

                                saLvwItem[0] = filename[i];
                                saLvwItem[1] = fileNameandPath[i];

                                ListViewItem lvi = new ListViewItem(saLvwItem);

                                listView1.Items.Add(lvi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Could not read file from disk.");
            }
        }

        private void btnPlayAll_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myPlaylist");
            WMPLib.IWMPMedia media;
            Random a = new Random();
            List<int> x = new List<int>();
            for (int idx = 0; idx < listView1.Items.Count; idx++)
            {
                int i = a.Next(0, listView1.Items.Count);
                if (x.Count == 0)
                {
                    x.Add(i);
                }
                else
                {
                    x.Sort();
                    if (x.BinarySearch(i) != -1)
                    {
                        continue;
                    }
                    else
                    {
                        x.Add(i);
                        media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[1].Text);
                        playlist.appendItem(media);
                        axWindowsMediaPlayer1.currentPlaylist = playlist;
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                    //for(int dobel = 0; dobel < x.Count; dobel++)
                    //{
                    //    if (i == x[dobel])
                    //    {
                    //        cek = false;
                    //        break;
                    //    }
                    //}
                    //if (cek)
                    //{
                    //    x.Add(i);
                    //    media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[1].Text);
                    //    playlist.appendItem(media);
                    //    axWindowsMediaPlayer1.currentPlaylist = playlist;
                    //    axWindowsMediaPlayer1.Ctlcontrols.play();
                    //}
                } 
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.URL = listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void symphonyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBrowser browser = new frmBrowser();
            browser.Show();
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void newPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreatePlaylist createplaylist = new frmCreatePlaylist();
            createplaylist.Show();
        }
    }
}
