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

namespace MovieArchive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=MovieArchive;Integrated Security=True");
       
        void Movies()
        {
            SqlDataAdapter db = new SqlDataAdapter("Select * from RecommendedMovies", connection);
            DataTable dt = new DataTable();
            db.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Movies();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into RecommendedMovies(Movies,Category,Imdb_Link,Sinemalar_Link,Beyazperde_Link) values (@P1,@P2,@P3,@P4,@P5)",connection);
            command.Parameters.AddWithValue("@P1", txtMovieName.Text);
            command.Parameters.AddWithValue("@P2", cbCategory.SelectedItem);
            command.Parameters.AddWithValue("@P3", txtImdb.Text);
            command.Parameters.AddWithValue("@P4", txtSinemalar.Text);
            command.Parameters.AddWithValue("@P5", txtBeyazperde.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Movie added your list.");
            Movies();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {   //Row Satır    Column Sütun
            int choosen = dataGridView1.SelectedCells[0].RowIndex;
            int choosen2 = dataGridView1.SelectedCells[0].ColumnIndex;
            string link = dataGridView1.Rows[choosen].Cells[choosen2].Value.ToString();

            webBrowser1.Navigate(link);
            
        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This project created by Yavuz Selim Güner.","INFO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            if (selectedIndex > -1)
            {
                dataGridView1.Rows.RemoveAt(selectedIndex);
                dataGridView1.Refresh();
            }
        }
    }
}
