using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace szakvizsga1._2
{
    public partial class kezdolap : Form
    {
        public kezdolap()
        {
            InitializeComponent();
        }

        private void Product_image_Click(object sender, EventArgs e)
        {
            termekek termekek = new termekek();
            termekek.Show();
            this.Hide();
        }

        private void Employee_image_Click(object sender, EventArgs e)
        {
            munkavallalok munkavallalok = new munkavallalok();
            munkavallalok.Show();
        }

        private void kezdolap_Load(object sender, EventArgs e)
        {
            label7.Text = Form2.Rule;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult kijelent;
            kijelent = MessageBox.Show("Biztosan Kijelentkezik?", "Bezárás", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kijelent == DialogResult.OK)
            {
                Form2 bejelentkezes = new Form2();
                bejelentkezes.Show();
                this.Hide();
            }
        }
    }


}
