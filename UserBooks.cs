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

namespace CS322___Projektni_zadatak___Bojana___Stajić___4596
{
    public partial class UserBooks : Form
    {
        MySqlConnection mycon;
        string con = "datasource=localhost;port=3306;username=root;password='';database=cs322_pz";
        DataTable dt = new DataTable();

        public UserBooks()
        {
            InitializeComponent();
        }

       

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UserBooks_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(con);
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT books.title, books.isbn, books.author, books.price, books.genre FROM books " +
                "JOIN cart_items ON cart_items.book_id = books.id WHERE user_id=@user_id AND flag=2;", mycon);

            cmd.Parameters.AddWithValue("@user_id", Shared.user_id);
            cmd.ExecuteScalar();

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            mycon = new MySqlConnection(con);
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT books.title, books.isbn, books.author, books.price, books.genre FROM books " +
                "JOIN cart_items ON cart_items.book_id = books.id WHERE user_id=@user_id AND flag=2;", mycon);

            cmd.Parameters.AddWithValue("@user_id", Shared.user_id);
            cmd.ExecuteScalar();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            BindingSource bind = new BindingSource();
            bind.DataSource = dt;

            dgvBooks.DataSource = bind;
            adapter.Update(dt);
            dgvBooks.Columns[0].Visible = true;
            dgvBooks.AutoResizeColumns();

        }

        private void UserBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dgvBooks_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserPanel userPanelForm = new UserPanel();
            userPanelForm.Show();
            userPanelForm.FormClosed += (s, args) => this.Close();
        }
    }

    
    }

