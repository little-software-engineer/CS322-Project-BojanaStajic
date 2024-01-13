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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

        }

        private void AdminPanel_closeForm(Object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminUserControl adminUserControlForm = new AdminUserControl();
            adminUserControlForm.FormClosed += (s, args) => this.Close();
            adminUserControlForm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminBookControl adminBookControlForm = new AdminBookControl();
            adminBookControlForm.FormClosed += (s, args) => this.Close();
            adminBookControlForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login homePanel = new Login();
            homePanel.Show();
            homePanel.FormClosed += (s, args) => this.Close();
        }

        private void AdminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
