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
    public partial class AdminUserControl : Form
    {
        MySqlConnection mycon;
        string con = "datasource=localhost;port=3306;username=root;password='';database=cs322_pz";
        DataTable dt = new DataTable();
        public AdminUserControl()
        {
            InitializeComponent();
        }
        private void UserControl_Load(object sender, EventArgs e)
        {
            try
            {
                mycon = new MySqlConnection(con);

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", mycon);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BindingSource bind = new BindingSource();
                bind.DataSource = dt;

                dgvUsers.DataSource = bind;
                adapter.Update(dt);
                dgvUsers.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvUsers.Rows[e.RowIndex];

                Shared.user_id = int.Parse(row.Cells["id"].Value.ToString());

                tbUsername.Text = row.Cells["username"].Value.ToString();
                tbPassword.Text = row.Cells["password"].Value.ToString();
                tbEmail.Text = row.Cells["email"].Value.ToString();
                tbName.Text = row.Cells["name"].Value.ToString();
                tbSurname.Text = row.Cells["surname"].Value.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminPanel adminPanelForm = new AdminPanel();
            adminPanelForm.Show();
            adminPanelForm.FormClosed += (s, args) => this.Close();
        }
        private void AdminUserControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Update
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string email = tbEmail.Text;
            string name = tbName.Text;
            string surname = tbSurname.Text;

            try
            {
                mycon = new MySqlConnection(con);
                mycon.Open();
                string query = "UPDATE user SET username=@username, password=@password, email=@email, name=@name, surname=@surname WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(query, mycon);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@id", Shared.user_id);
                cmd.ExecuteScalar();

                MessageBox.Show("Admin, you have successfully changed a user!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            try
            {
                mycon = new MySqlConnection(con);
                mycon.Open();
                string query = "DELETE FROM user WHERE username=@username";

                MySqlCommand cmd = new MySqlCommand(query, mycon);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteScalar();

                MessageBox.Show("Admin, you have successfully deleted a user!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Refresh
            try
            {
                mycon = new MySqlConnection(con);
                MySqlCommand cmd = new MySqlCommand("select * from user;", mycon);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BindingSource bind = new BindingSource();
                bind.DataSource = dt;

                dgvUsers.DataSource = bind;
                adapter.Update(dt);
                dgvUsers.AutoResizeColumn(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void searchUser(string userToFind)
        {
            string searchQuery = "SELECT * FROM user WHERE username LIKE '%" + userToFind + "%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvUsers.DataSource = table;
        }
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            searchUser(tbSearch.Text);
        }

        private void dgvUsers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
