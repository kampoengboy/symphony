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

namespace symphony
{
    public partial class frmPlaylist : Form
    {
        public frmPlaylist()
        {
            InitializeComponent();
        }
        public bool state = false;
        Tuple<string, string> song;
        public List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        SqlConnection conn = new SqlConnection("Server=localhost; Database=symphony; Integrated Security=SSPI");
        SqlDataAdapter da;
        List<string> id = new List<string>();
        string sql;
        DataSet ds;
        int pos;
        private void frmPlaylist_Load(object sender, EventArgs e)
        {    
            string pesan;
            try
            {
                conn.Open();
                ds = new DataSet(); 
                da = new SqlDataAdapter("SELECT * FROM Playlist",conn);
                da.Fill(ds, "Playlist");
                DataRowCollection dt = ds.Tables["Playlist"].Rows;
                for(int i=0;i<dt.Count;i++)
                {
                    listBox1.Items.Add(dt[i]["Name"].ToString());
                    id.Add(dt[i]["IDPlaylist"].ToString());
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                conn.Open();
                ds = new DataSet();
                string query = "SELECT Name,Location FROM Song WHERE Song.IDPlaylist='" + id[listBox1.SelectedIndex] + "'";
                da = new SqlDataAdapter(query, conn);
                da.Fill(ds, "Song");
                dataGridView1.DataSource = ds.Tables["Song"];
                conn.Close();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                conn.Open();
                ds = new DataSet();
                string query = "SELECT Name,Location FROM Song WHERE Song.IDPlaylist='" + id[listBox1.SelectedIndex] + "'";
                da = new SqlDataAdapter(query, conn);
                da.Fill(ds, "Song");
                DataRowCollection dt = ds.Tables["Song"].Rows;
                for (int i = 0; i < dt.Count; i++)
                {
                    song = new Tuple<string, string>(dt[i]["Name"].ToString(),dt[i]["Location"].ToString());
                    list.Add(song);
                }
                conn.Close();
                state = true;
                this.Close();
            }
        }
    }
}
