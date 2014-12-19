using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace symphony
{
    public partial class frmCustomize : Form
    {
        public frmCustomize()
        {
            InitializeComponent();
        }
        public string id;
        SqlConnection conn = new SqlConnection("Server=localhost; Database=symphony; Integrated Security=SSPI");
        SqlDataAdapter da;
        DataSet ds;
        List<string> idsong = new List<string>();
        List<string> paths = new List<string>();
        private string[] files, path;

        private void AddData(string file,string path)
        {
                string id2, query;
                int xx;
                bool state = false;
                da = new SqlDataAdapter("SELECT * FROM Song", conn);
                da.Fill(ds, "Song");
                DataRowCollection dt2 = ds.Tables["Song"].Rows;
                //CHECK FOR THE SPACE ROW
                for (int k = 1; k <= dt2.Count; k++)
                {
                    int nn = k / 10;
                    id2 = dt2[k - 1]["ID_Song"].ToString();
                    if (id2.Substring(id2.Length - (nn + 1), nn + 1) != Convert.ToString(k))
                    {
                        id2 = "S";
                        for (int zero = 1; zero <= 2 - (nn); zero++)
                        {
                            id2 += "0";
                        }
                        id2 += k;
                        idsong.Add(id2);
                        query = "INSERT INTO Song VALUES('" + id2 + "','" + file + "','" + path + "','" + id + "')";
                        da.InsertCommand = new SqlCommand(query, conn);
                        da.InsertCommand.ExecuteNonQuery();
                        state = true;
                        break;
                    }
                }
                //IF THERE IS NO SPACE ROW
                if (!state)
                {
                    xx = dt2.Count+1;
                    id2 = "S";
                    for (int zero = 1; zero <= 2 - (xx / 10); zero++)
                    {
                        id2 += "0";
                    }
                    id2 += xx;
                    idsong.Add(id2);
                    query = "INSERT INTO Song VALUES('" + id2 + "','" + file + "','" + path + "','" + id + "')";
                    da.InsertCommand = new SqlCommand(query, conn);
                    da.InsertCommand.ExecuteNonQuery();
                }
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
                int idx = 0;
                conn.Open();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    files = open.SafeFileNames;
                    path = open.FileNames;
                    for (int i = 0; i < files.Length; i++)
                    {
                        listBox1.Items.Add(files[i]);
                        paths.Add(path[i]);
                        da = new SqlDataAdapter("SELECT * FROM Song", conn);
                        ds = new DataSet();
                        da.Fill(ds, "Song");
                        DataRowCollection dt3 = ds.Tables["Song"].Rows;
                        AddData(files[i], path[i]);
                    }

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Could not read file from disk.");
            }
        }

        private void frmCustomize_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                ds = new DataSet();
                da = new SqlDataAdapter("SELECT Name FROM Playlist WHERE IDPlaylist='"+id+"'", conn);
                da.Fill(ds, "Playlist");
                DataRowCollection dt = ds.Tables["Playlist"].Rows;
                textBox1.Text = dt[0]["Name"].ToString();
                ds = new DataSet();
                da = new SqlDataAdapter("SELECT * FROM Song WHERE IDPlaylist='" + id + "'", conn);
                da.Fill(ds, "Song");
                dt = ds.Tables["Song"].Rows;
                for (int i = 0; i < dt.Count;i++ )
                {
                    listBox1.Items.Add(dt[i]["Name"].ToString());
                    idsong.Add(dt[i]["ID_Song"].ToString());
                    paths.Add(dt[i]["Location"].ToString());
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                conn.Open();
                ds = new DataSet();
                string query = "DELETE FROM Song WHERE ID_Song='"+idsong[listBox1.SelectedIndex]+"'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                idsong.RemoveAt(listBox1.SelectedIndex);
                paths.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);        
                conn.Close();
            }
        }

        private void frmCustomize_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Open();
            ds = new DataSet();
            string query = "UPDATE Playlist SET Name='" + textBox1.Text + "' WHERE IDPlaylist='" + id +"'";
            da.UpdateCommand = new SqlCommand(query, conn);
            da.UpdateCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
