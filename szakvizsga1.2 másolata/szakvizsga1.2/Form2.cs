using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using DGVPrinterHelper;

namespace szakvizsga1._2
{
    public partial class Form2 : Form
    {
        Timer t = new Timer();

        public static string Rule;
        public Form2()
        {
            InitializeComponent();
        }

             private void Form2_Load(object sender, EventArgs e)
        {
            //timer interval
            t.Interval = 1000;  //in milliseconds

            t.Tick += new EventHandler(this.t_Tick);

            //start timer when form loads
            t.Start();  //this will use t_Tick() method
        }

        private void t_Tick(object sender, EventArgs e)
        {

            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            //update label

            label3.Text = time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection kapcsolat = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adatbazis.accdb");
            OleDbCommand loginparancs = new OleDbCommand();
            int i = 0;

            if ((textBox1.Text == string.Empty || textBox2.Text == string.Empty))
            {
                MessageBox.Show("Ne hagyja üresen a mező(ke)t!");
            }

            try
            {

                //------------------------------------------------------------------

                loginparancs = new OleDbCommand("SELECT count(*) FROM Munkavallalok WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", kapcsolat);

                //-------------------------------------------------------------------
                Rule = textBox1.Text;

                if (kapcsolat.State == ConnectionState.Closed)
                {
                    kapcsolat.Open();
                    i = (int)loginparancs.ExecuteScalar();
                }
                kapcsolat.Close();
                if (i > 0)
                {
                    kezdolap form = new kezdolap();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("A felhasználónév vagy a jelszó helytelen");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }

            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.ToString());
            }
            //https://www.youtube.com/watch?v=YEsjHrIOdos
            //flaticon.com

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
