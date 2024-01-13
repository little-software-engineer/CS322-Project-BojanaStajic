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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CS322___Projektni_zadatak___Bojana___Stajić___4596
{
    public partial class UserPanel : Form
    {
        MySqlConnection mycon;
        string con = "datasource=localhost;port=3306;username=root;password='';database=cs322_pz";
        DataTable dt = new DataTable();
     

        public UserPanel()
        {
            InitializeComponent();
        }

        private void UserPanel_Load(object sender, EventArgs e)
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
                dgvBooks.Columns[0].Visible = false;
                dgvBooks.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

      
      
        public void searchBook(string bookToFind)
        {
            string searchQuery = "SELECT * FROM books WHERE title LIKE '%" + bookToFind + "%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvBooks.DataSource = table;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            searchBook(tbSearch.Text);
        }

        

        private void UserPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tbBookToAdd_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvBooks.Rows[e.RowIndex];

                book_id = int.Parse(row.Cells["id"].Value.ToString());
                tbBookToAdd.Text = row.Cells["title"].Value.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cart cartForm = new Cart();
            cartForm.Show();
            cartForm.FormClosed += (s, args) => this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (book_id == 0)
                {
                    MessageBox.Show("Please select a book in order to buy it.");
                }
                else
                {
                    mycon = new MySqlConnection(con);
                    mycon.Open();
                    string query = "INSERT INTO cart_items(book_id, user_id) VALUES(@book_id,@user_id)";

                    MySqlCommand cmd = new MySqlCommand(query, mycon);
                    cmd.Parameters.AddWithValue("@book_id", book_id);
                    cmd.Parameters.AddWithValue("@user_id", Shared.user_id);
                    cmd.ExecuteScalar();

                    MessageBox.Show("User! You have successfully added a book to your cart!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserBooks userBooksForm = new UserBooks();
            userBooksForm.Show();
            userBooksForm.FormClosed += (s, args) => this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login homePanel = new Login();
            homePanel.Show();
            homePanel.FormClosed += (s, args) => this.Close();
        }
    }
}
