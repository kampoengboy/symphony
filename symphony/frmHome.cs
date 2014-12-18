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
using System.Data.SqlClient;

namespace symphony
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        public string[] attr;
        public bool state = false;
        public double duration;

        public void changeTime(double dur)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition+= dur;
        }
        List<string> paths = new List<string>();
        private string[] files, path;
        private void button1_Click(object sender, EventArgs e)
        {
            frmPlaylist playlist = new frmPlaylist();
            playlist.ShowDialog();
            if(playlist.state)
            {
                for (int i = 0; i < playlist.list.Count; i++)
                {
                    listBox1.Items.Add(playlist.list[i].Item1);
                    paths.Add(playlist.list[i].Item2);
                }
            }
            //Stream myStream = null;
            //OpenFileDialog open = new OpenFileDialog();
            //open.InitialDirectory = "C:\\";
            //open.Filter = "MP3 Audio File(*.mp3)|*.mp3";
            //open.FilterIndex = 1;
            //open.RestoreDirectory = false;
            //open.Multiselect = true;
            //try
            //{
            //    if (open.ShowDialog() == DialogResult.OK)
            //    {
            //        files = open.SafeFileNames;
            //        path = open.FileNames;
            //        for (int i = 0; i < files.Length; i++)
            //        {
            //            listBox1.Items.Add(files[i]);
            //            paths.Add(path[i]);
            //        }
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error : Could not read file from disk.");
            //}
        }

        private void btnPlayAll_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myPlaylist");
            WMPLib.IWMPMedia media;
            Random a = new Random();
            List<int> x = new List<int>();
            for (int idx = 0; idx < listBox1.Items.Count; idx++)
            {
                int i = a.Next(0, listBox1.Items.Count+1);
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
                        axWindowsMediaPlayer1.URL = paths[i];
                        listBox1.SelectedIndex = i;
                    }
                } 
            }
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
        private void frmHome_Load(object sender, EventArgs e)
        {
            attr = new string[15];
            trackBar2.Value = 109;
            tracklabel.Text = "00:00";
            string pesan,sql;
            //try
            //{
            //    conn = new SqlConnection("Server=localhost; Database=symphony; Integrated Security=SSPI");
            //    conn.Open();
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Database Error");
            //}
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewfileinfo view = new viewfileinfo();
            WMPLib.IWMPMedia media = axWindowsMediaPlayer1.newMedia(paths[listBox1.SelectedIndex]);
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
            view.attr[12] = paths[listBox1.SelectedIndex];
            view.Show();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            jumptotrack jumptrack = new jumptotrack();
            for (int i = 0; i < listBox1.Items.Count;i++)
            {
                jumptrack.track.Add(listBox1.Items[i].ToString());
            }
            jumptrack.ShowDialog();
            if(jumptrack.state)
            {
                listBox1.SelectedIndex = jumptrack.idx;
                axWindowsMediaPlayer1.URL= paths[jumptrack.idx];
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

        private void playfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            timer1.Enabled = false;
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                timer1.Interval = 100;
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < files.Length - 1)
            {
                listBox1.SelectedIndex++;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                timer1.Enabled = false;
            }
            else
            {
                listBox1.SelectedIndex = 0;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                timer1.Enabled = false;
            }  
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                state = true;
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.Location);
                if (listBox1.SelectedIndex != -1)
                {
                        var loc = e.Location;
                        loc.Offset(listBox1.Location);
                        viewfileinfo view = new viewfileinfo();
                        this.contextMenuStrip2.Show(this, loc);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex>0)
            {
                listBox1.SelectedIndex--;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
            }
            else
            {
                listBox1.SelectedIndex=files.Length-1;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < files.Length-1)
            {
                listBox1.SelectedIndex--;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
            }
            else
            {
                listBox1.SelectedIndex = 0;
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (state)
            {
                int m=0, s=0;
                trackBar1.Maximum = Convert.ToInt32(axWindowsMediaPlayer1.currentMedia.duration);
                trackBar1.Value = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                s = trackBar1.Value;
                if(s>=60)
                {
                    m = s / 60;
                    s = s % 60;
                }
                if (s >= 10)
                {
                    if (m >= 10)
                        tracklabel.Text = m + ":" + s.ToString();
                    else
                        tracklabel.Text = "0" + m + ":" + s.ToString();
                }
                else
                {
                    if (m >= 10)
                        tracklabel.Text = m + ":0" + s.ToString();
                    else
                        tracklabel.Text = "0" + m + ":0" + s.ToString();
                }
                axWindowsMediaPlayer1.settings.volume = trackBar2.Value;
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            timer2.Enabled = true;
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer2.Enabled = false;
        }

        private void trackBar1_MouseLeave(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
            state = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            timer1.Enabled = false;
        }        
    }
}
