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
        public string[] attr;
        public bool isJump = false;
        public double duration;
        public void changeTime(double dur)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition+= dur;
        }
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
                int i = a.Next(0, listView1.Items.Count+1);
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
                } 
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(listView1.SelectedItems[0].SubItems[1].Text);
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myPlaylist");
            for (int idx = listView1.SelectedItems[0].Index; idx < listView1.Items.Count; idx++)
            {
                  media = axWindowsMediaPlayer1.newMedia(listView1.Items[idx].SubItems[1].Text);
                  playlist.appendItem(media);
                  axWindowsMediaPlayer1.currentPlaylist = playlist;
                  axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            viewfileinfo view = new viewfileinfo();
            view.attr[0] = media.getItemInfo("Track").ToString();
            view.attr[1] = media.getItemInfo("Disc").ToString();
            view.attr[2] = media.getItemInfo("BPM").ToString();
            view.attr[3] = media.name.ToString();
            view.attr[4] = media.getItemInfo("Artist").ToString();
            view.attr[5] = media.getItemInfo("Album").ToString();
            view.attr[6] = media.getItemInfo("Album Artist").ToString();
            view.attr[7] = media.getItemInfo("Year").ToString();
            view.attr[8] = media.getItemInfo("Genre").ToString();
            view.attr[9] = media.getItemInfo("Comment").ToString();
            view.attr[10] = media.getItemInfo("Composer").ToString();
            view.attr[11] = media.getItemInfo("Publisher").ToString();
            view.attr[12] = listView1.SelectedItems[0].SubItems[1].Text;
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

        private void customizePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomize customize = new frmCustomize();
            customize.Show();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = listView1.HitTest(e.X, e.Y);
                if (hitTestInfo.Item != null)
                {
                    var loc = e.Location;
                    loc.Offset(listView1.Location);
                    viewfileinfo view = new viewfileinfo();
                    WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(listView1.SelectedItems[0].SubItems[1].Text);
                    view.attr[0] = media.getItemInfo("Track").ToString();
                    view.attr[1] = media.getItemInfo("Disc").ToString();
                    view.attr[2] = media.getItemInfo("BPM").ToString();
                    view.attr[3] = media.name.ToString();
                    view.attr[4] = media.getItemInfo("Artist").ToString();
                    view.attr[5] = media.getItemInfo("Album").ToString();
                    view.attr[6] = media.getItemInfo("Album Artist").ToString();
                    view.attr[7] = media.getItemInfo("Year").ToString();
                    view.attr[8] = media.getItemInfo("Genre").ToString();
                    view.attr[9] = media.getItemInfo("Comment").ToString();
                    view.attr[10] = media.getItemInfo("Composer").ToString();
                    view.attr[11] = media.getItemInfo("Publisher").ToString();
                    view.attr[12] = listView1.SelectedItems[0].SubItems[1].Text;
                    this.contextMenuStrip2.Show(this, loc);
                }
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            attr = new string[15];
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewfileinfo view = new viewfileinfo();
            WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(listView1.SelectedItems[0].SubItems[1].Text);
            view.attr[0] = media.getItemInfo("Track").ToString();
            view.attr[1] = media.getItemInfo("Disc").ToString();
            view.attr[2] = media.getItemInfo("BPM").ToString();
            view.attr[3] = media.name.ToString();
            view.attr[4] = media.getItemInfo("Artist").ToString();
            view.attr[5] = media.getItemInfo("Album").ToString();
            view.attr[6] = media.getItemInfo("Album Artist").ToString();
            view.attr[7] = media.getItemInfo("Year").ToString();
            view.attr[8] = media.getItemInfo("Genre").ToString();
            view.attr[9] = media.getItemInfo("Comment").ToString();
            view.attr[10] = media.getItemInfo("Composer").ToString();
            view.attr[11] = media.getItemInfo("Publisher").ToString();
            view.attr[12] = listView1.SelectedItems[0].SubItems[1].Text;
            view.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            jumptotrack jumptrack = new jumptotrack();
            Tuple<string, string> song;
            for (int i = 0; i < listView1.Items.Count;i++ )
            {
                song = new Tuple<string, string>(listView1.Items[i].SubItems[0].Text, listView1.Items[i].SubItems[1].Text); 
                jumptrack.track.Add(song);
            }
            jumptrack.ShowDialog();
            if(jumptrack.state)
            {
                WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(listView1.Items[jumptrack.idx].SubItems[1].Text);
                WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myPlaylist");
                for (int idx = jumptrack.idx; idx < listView1.Items.Count; idx++)
                {
                    media = axWindowsMediaPlayer1.newMedia(listView1.Items[idx].SubItems[1].Text);
                    playlist.appendItem(media);
                    axWindowsMediaPlayer1.currentPlaylist = playlist;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
        }

        private void jumpToTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            jumptotime jumptime = new jumptotime();
            duration = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            jumptime.ShowDialog();
            if(jumptime.state)
            {
                changeTime(jumptime.duration);
            }
        }

        
    }
}
