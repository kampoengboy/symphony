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
    public partial class frmView : Form
    {
        public frmView()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=localhost; Database=symphony; Integrated Security=SSPI");
        SqlDataAdapter da;
        DataSet ds;
        List<string> id = new List<string>();
        private void frmView_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                ds = new DataSet();
                da = new SqlDataAdapter("SELECT * FROM Playlist", conn);
                da.Fill(ds, "Playlist");
                DataRowCollection dt = ds.Tables["Playlist"].Rows;
                for (int i = 0; i < dt.Count; i++)
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
                    this.contextMenuStrip1.Show(this, loc);
                }
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomize custom = new frmCustomize();
            custom.id = id[listBox1.SelectedIndex];
            custom.ShowDialog();
            //AFTER CUSTOMIZE
            listBox1.Items.Clear();
            dataGridView1.DataSource = "";
            conn.Open();
            ds = new DataSet();
            da = new SqlDataAdapter("SELECT * FROM Playlist", conn);
            da.Fill(ds, "Playlist");
            DataRowCollection dt = ds.Tables["Playlist"].Rows;
            for (int i = 0; i < dt.Count; i++)
            {
                listBox1.Items.Add(dt[i]["Name"].ToString());
            }
            conn.Close();
        }

        private void deletePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                string query;
                conn.Open();
                SqlCommand cmd;
                ds = new DataSet();
                da = new SqlDataAdapter("SELECT * FROM Song WHERE IDPlaylist='"+id[listBox1.SelectedIndex]+"'", conn);
                da.Fill(ds, "Song");
                DataRowCollection dt = ds.Tables["Song"].Rows;
                for (int i = 0; i < dt.Count;i++ )
                {
                    query = "DELETE FROM Song WHERE ID_Song='" + dt[0]["ID_Song"] + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                query = "DELETE FROM Playlist WHERE IDPlaylist='" + id[listBox1.SelectedIndex] + "'";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                id.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                dataGridView1.DataSource = "";
                conn.Close();
            }
        }
    }
}
