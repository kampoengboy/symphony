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
    public partial class frmCreatePlaylist : Form
    {
        public frmCreatePlaylist()
        {
            InitializeComponent();
        }
        List<string> paths = new List<string>();
        private string[] files, path;
        SqlConnection conn;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
                    files = open.SafeFileNames;
                    path = open.FileNames;
                    for (int i = 0; i < files.Length; i++)
                    {
                        listBox1.Items.Add(files[i]);
                        paths.Add(path[i]);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Could not read file from disk.");
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
                    this.contextMenuStrip1.Show(this, loc);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            paths.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            //FolderBrowserDialog open = new FolderBrowserDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                conn = new SqlConnection("Server=localhost; Database=symphony; Integrated Security=SSPI");
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Playlist", conn);
                da.Fill(ds, "Playlist");
                bool sta = false;
                DataRowCollection dt = ds.Tables["Playlist"].Rows;
                for(int i=1;i<=dt.Count;i++)
                {
                    int n=i/10;
                    string id = dt[i-1]["IDPlaylist"].ToString();
                    if(id.Substring(id.Length-(n+1),n+1)!=Convert.ToString(i))
                    {
                        id = "P";
                        for (int zero = 1; zero <= 2 - (n);zero++ )
                        {
                            id += "0";
                        }
                        id += i;
                       query = "Insert Into Playlist Values('" + id + "','" + textBox1.Text + "')";
                       da = new SqlDataAdapter();
                       da.InsertCommand = new SqlCommand(query, conn);
                       da.InsertCommand.ExecuteNonQuery();
                       for(int j=0;j<listBox1.Items.Count;j++)
                       {
                           string id2;
                            bool state = false;
                            da = new SqlDataAdapter("SELECT * FROM Song", conn);
                            da.Fill(ds, "Song");
                            DataRowCollection dt2 = ds.Tables["Song"].Rows;
                            for (int k = 1; k <= dt2.Count;k++)
                            {
                                int nn = k / 10;
                                id2 = dt2[k-1]["ID_Song"].ToString();
                                if(id2.Substring(id2.Length-(nn+1),nn+1)!=Convert.ToString(k))
                                {
                                    id2 = "S";
                                    for (int zero = 1; zero <= 2 - (nn); zero++)
                                    {
                                        id2 += "0";
                                    }
                                    id2 += k;
                                    query = "INSERT INTO Song VALUES('" + id2 + "','" + listBox1.Items[j] + "','" + paths[j] + "','" + id + "')";
                                    da = new SqlDataAdapter();
                                    da.InsertCommand = new SqlCommand(query, conn);
                                    da.InsertCommand.ExecuteNonQuery();
                                    state = true;
                                    break;
                                }
                            }
                            if(!state)
                            {
                                int xx = dt2.Count+1;
                                id2 = "S";
                                for (int zero = 1; zero <= 2 - (xx/10); zero++)
                                {
                                    id2 += "0";
                                }
                                id2 += xx;
                                query = "INSERT INTO Song VALUES('" + id2 + "','" + listBox1.Items[j] + "','" + paths[j] + "','"+ id+"')";
                                da = new SqlDataAdapter();
                                da.InsertCommand = new SqlCommand(query, conn);
                                da.InsertCommand.ExecuteNonQuery();
                            }
                       }
                       sta = true;
                       break;
                    }
                }
                if(!sta)
                {
                    string id,id2;
                    int xx = dt.Count + 1;
                    id = "P";
                    for (int zero = 1; zero <= 2 - (xx/10); zero++)
                    {
                        id += "0";
                    }
                    id += xx;
                    query = "Insert Into Playlist Values('" + id + "','" + textBox1.Text + "')";
                    da.InsertCommand = new SqlCommand(query, conn);
                    da.InsertCommand.ExecuteNonQuery();
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        bool state = false;
                        da = new SqlDataAdapter("SELECT * FROM Song", conn);
                        da.Fill(ds, "Song");
                        DataRowCollection dt2 = ds.Tables["Song"].Rows;
                        for (int k = 1; k <= dt2.Count; k++)
                        {
                            int nn = k / 10;
                            id2 = dt2[k-1]["ID_Song"].ToString();
                            if (id2.Substring(id2.Length - (nn + 1), nn + 1) != Convert.ToString(k))
                            {
                                id2 = "S";
                                for (int zero = 1; zero <= 2 - (nn); zero++)
                                {
                                    id2 += "0";
                                }
                                id2 += k;
                                query = "INSERT INTO Song VALUES('" + id2 + "','" + listBox1.Items[j] + "','" + paths[j] + "','" + id + "')";
                                da.InsertCommand = new SqlCommand(query, conn);
                                da.InsertCommand.ExecuteNonQuery();
                                state = true;
                                break;
                            }
                        }
                        if (!state)
                        {
                            xx = dt2.Count+1;
                            id2 = "S";
                            for (int zero = 1; zero <= 2 - (xx / 10); zero++)
                            {
                                id2 += "0";
                            }
                            id2 += xx;
                            query = "INSERT INTO Song VALUES('" + id2 + "','" + listBox1.Items[j] + "','" + paths[j] + "','"+ id+"')";
                            da.InsertCommand = new SqlCommand(query, conn);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                    }
                }
                conn.Close();
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error");
            }
        }

    }
}
