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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CS322___Projektni_zadatak___Bojana___Stajić___4596
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeForm = new Home();
            homeForm.Show();
            homeForm.FormClosed += (s, args) => this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {

                string username = tbUsername.Text;
                string password = tbPassword.Text;
                if (string.IsNullOrWhiteSpace(username) ||
                   string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("All fields must be filled to register!");
                }
                else
                {
                    string mycon = "datasource=localhost;port=3306;username=root;password='';database=cs322_pz";
                    MySqlConnection con = new MySqlConnection(mycon);

                    con.Open();
                    string query = "SELECT * FROM user WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataReader myReader;

                    myReader = cmd.ExecuteReader();
                    int count = 0;
                    string flag = string.Empty;

                    while (myReader.Read())
                    {
                        Shared.user_id = int.Parse(myReader["id"].ToString());
                        count += 1;
                        flag = myReader["flag"].ToString();
                    }
                    if (count == 1)
                    {
                        if (flag == "0")
                        {
                            this.Hide();
                            AdminPanel adminForm = new AdminPanel();
                            adminForm.FormClosed += (s, args) => this.Close();
                            adminForm.Show();
                        }
                        else if (flag == "1")
                        {
                            this.Hide();
                            UserPanel userForm = new UserPanel();
                            userForm.FormClosed += (s, args) => this.Close();
                            userForm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password! Please Try Again.");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
