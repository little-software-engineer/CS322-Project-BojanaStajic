using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CS322___Projektni_zadatak___Bojana___Stajić___4596
{
    public partial class AdminBookControl : Form
    {
        MySqlConnection mycon;
        string con = "datasource=localhost;port=3306;username=root;password='';database=cs322_pz";
        DataTable dt = new DataTable();

        public AdminBookControl()
        {
            InitializeComponent();
        }
        private void AdminBookControl_Load(object sender, EventArgs e)
        {
            try
            {
                mycon = new MySqlConnection(con);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM books;", mycon);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BindingSource bind = new BindingSource();
                bind.DataSource = dt;

                dgvBooks.DataSource = bind;
                adapter.Update(dt);
                dgvBooks.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvBooks.Rows[e.RowIndex];

                Shared.book_id = int.Parse(row.Cells["id"].Value.ToString());

                tbTitle.Text = row.Cells["title"].Value.ToString();
                tbIsbn.Text = row.Cells["isbn"].Value.ToString();
                tbAuthor.Text = row.Cells["author"].Value.ToString();
                tbPrice.Text = row.Cells["price"].Value.ToString();
                tbGenre.Text = row.Cells["genre"].Value.ToString();
            }
        }
        public void searchBook(string bookToFind)
        {
            string searchQuery = "SELECT * FROM books WHERE title LIKE '%" + bookToFind + "%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvBooks.DataSource = table;
        }
       

        
      

     
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminPanel adminPanelForm = new AdminPanel();
            adminPanelForm.Show();
            adminPanelForm.FormClosed += (s, args) => this.Close();
        }

        private void AdminBookControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void btnRemove_Click_1(object sender, EventArgs e)
        {

            string title = tbTitle.Text;
            try
            {
                mycon = new MySqlConnection(con);
                mycon.Open();
                string query = "DELETE FROM books WHERE title=@title";

                MySqlCommand cmd = new MySqlCommand(query, mycon);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.ExecuteScalar();

                MessageBox.Show("Admin, you have successfully deleted a book!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string isbn = tbIsbn.Text;
            string author = tbAuthor.Text;
            string price = tbPrice.Text;
            string genre = tbGenre.Text;

            try
            {
                mycon = new MySqlConnection(con);
                mycon.Open();
                string query = "UPDATE books SET title=@title, isbn=@isbn, author=@author, price=@price, genre=@genre WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(query, mycon);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@id", Shared.book_id);
                cmd.ExecuteScalar();

                MessageBox.Show("Admin, you have successfully changed a book!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbIsbn_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSearch_TextChanged_1(object sender, EventArgs e)
        {
            searchBook(tbSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string title = tbTitle.Text;
            string isbn = tbIsbn.Text;
            string author = tbAuthor.Text;
            string price = tbPrice.Text;
            string genre = tbGenre.Text;

            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(isbn) ||
                    string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(price) ||
                    string.IsNullOrWhiteSpace(genre))
                {
                    MessageBox.Show("All fields must be filled!");
                }
                else
                {
                    mycon = new MySqlConnection(con);
                    mycon.Open();
                    string query = "INSERT INTO books(title, isbn, author, price, genre) VALUES(@title, @isbn, @author, @price, @genre)";

                    MySqlCommand cmd = new MySqlCommand(query, mycon);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@genre", genre);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Admin, you have successfully added a new book!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mycon = new MySqlConnection(con);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM books", mycon);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BindingSource bind = new BindingSource();
                bind.DataSource = dt;

                dgvBooks.DataSource = bind;
                adapter.Update(dt);
                dgvBooks.AutoResizeColumn(0);

                tbTitle.Text = "";
                tbIsbn.Text = "";
                tbAuthor.Text = "";
                tbPrice.Text = "";
                tbGenre.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
