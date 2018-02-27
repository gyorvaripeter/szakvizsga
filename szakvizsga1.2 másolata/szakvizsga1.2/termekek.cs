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
    public partial class termekek : Form
    {
        OleDbConnection kapcsolat = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Adatbazis.accdb");
        OleDbCommand parancs;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();

        public termekek()
        {
            InitializeComponent();
            // kapcsolat = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\Adatbazis.accdb");

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Term_ID";
            dataGridView1.Columns[0].CellTemplate.ValueType = typeof(int);
            dataGridView1.Columns[1].Name = "Termek";
            dataGridView1.Columns[2].Name = "Leiras";
            dataGridView1.Columns[3].Name = "Kategoria";
            dataGridView1.Columns[4].Name = "Mennyiseg";
            dataGridView1.Columns[5].Name = "Egysegar";
            //  dataGridView1.Columns[6].Name = "Attachments";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
           
        }
        private void termekek_Load(object sender, EventArgs e)
        {

            /* kapcsolat.Open();
             parancs =  new OleDbCommand();
             parancs.Connection = kapcsolat;
             string osszes_lekerdezese = "SELECT * from Termekek";
             parancs.CommandText = osszes_lekerdezese;
             adapter = new OleDbDataAdapter(parancs);

             adapter.Fill(dt);
             dataGridView1.DataSource = dt;*/

            //----------------------------------------------------





            //-----------------------------------------------------
            /*
                        DataGridViewColumn column1 = dataGridView1.Columns[0];
                        column1.Width = 60;
                        column1.HeaderText = "ID";            
                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        column2.Width = 260;
                        column2.HeaderText = "Termék";
                        DataGridViewColumn column3 = dataGridView1.Columns[2];
                        column3.Visible = false;
                        DataGridViewColumn column4 = dataGridView1.Columns[3];
                        column4.Width = 90;
                        column4.HeaderText = "Kategória";
                        DataGridViewColumn column5 = dataGridView1.Columns[4];
                        column5.Width = 100;
                        column5.HeaderText = "Mennyiség";
                        DataGridViewColumn column6 = dataGridView1.Columns[5];
                        column6.Width = 90;
                        column6.HeaderText = "Egységár";
                        DataGridViewColumn column7 = dataGridView1.Columns[6];
                        column7.Visible = false;

                        //-----------------------------------------------------
                       /* comboBox1.Items.Add("CPU"); comboBox1.Items.Add("MAINBOARD"); comboBox1.Items.Add("SOUNDCARD"); comboBox1.Items.Add("RAM");
                        comboBox1.Items.Add("TABLET"); comboBox1.Items.Add("PHONE");
                        */

            //  kapcsolat.Close();

        }

        //-------------------------------------------------------------

        private void add(string product, string description, string category, string amount, string price)
        {
            string sql = "INSERT INTO Termekek(Termek, Leiras, Kategoria, Mennyiseg, Egysegar) VALUES(@pro,@desc,@cat,@amo,@uni)";
            parancs = new OleDbCommand(sql, kapcsolat);

            //
            //parancs.Parameters.AddWithValue("@ID", id);
            parancs.Parameters.AddWithValue("@pro", product);
            parancs.Parameters.AddWithValue("@desc", description);
            parancs.Parameters.AddWithValue("@cat", category);
            parancs.Parameters.AddWithValue("@amo", amount);
            parancs.Parameters.AddWithValue("@uni", price);

            //--------------kapcsolat megnyitás
            try
            {
                kapcsolat.Open();
                if (parancs.ExecuteNonQuery() > 0)
                {

                    clearTxts();
                    MessageBox.Show("Sikeres hozzáadás");


                }
                retrieve();
                kapcsolat.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kapcsolat.Close();
            }

        }

        // adatok feltöltése
        private void FELTOLTES(int id, string product, string description, string category, string amount, string price)
        {
            dataGridView1.Rows.Add(id, product, description, category, amount, price);
        }
        //--------------------------------------------------------------


        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM Termekek";
            parancs = new OleDbCommand(sql, kapcsolat);

            try
            {
                kapcsolat.Open();
                adapter = new OleDbDataAdapter(parancs);
                adapter.Fill(dt);


                //Loop THRU DT
                foreach (DataRow row in dt.Rows)
                {
                    FELTOLTES((int)row[0], row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());
                }
                kapcsolat.Close();
                dt.Rows.Clear();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kapcsolat.Close();

            }
        }

        //------------------------------------------------------------------

        private void update(int id, string product, string description, string category, string amount, string price)
        {
            //SQL STMT  "IPHONE XYZ", Termekek.Leiras = "Lol", Termekek.Kategoria = "Bencebuzi", Termekek.Mennyiseg = "69", Termekek.Egysegar = 500
            // WHERE(((Termekek.Term_ID) = 20));

            //string sql = "UPDATE Termekek SET Termekek.Termek ='" + product + "',Leiras='" + description + "',Kategoria='" + category + "',Mennyiseg='" + amount + "',Egysegar='" + price + "' WHERE Term_ID='" + id + "';";
            string sql = "UPDATE Termekek SET Termekek.Termek = '"+product+"', Termekek.Leiras = '"+description+"', Termekek.Kategoria = '"+category+"', Termekek.Mennyiseg = '"+amount+"', Termekek.Egysegar = '"+price+"' WHERE Termekek.Term_ID = '"+id+"';";
            parancs = new OleDbCommand(sql, kapcsolat);

            //OPEN CON,UPDATE,RETRIEVE DGVIEW
            try
            {
                kapcsolat.Open();
                adapter = new OleDbDataAdapter(parancs);

                adapter.UpdateCommand = kapcsolat.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Sikeresen Frissítve!");
                }

                kapcsolat.Close();

                //REFRESH
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kapcsolat.Close();
            }

        }
        private void delete(int id)
        {
            //SQL STMT
            string sql = "DELETE FROM Termekek WHERE Term_ID=" + id;
            parancs = new OleDbCommand(sql, kapcsolat);

            //'OPEN CON,EXECUTE DELETE,CLOSE CON
            try
            {
                kapcsolat.Open();
                adapter = new OleDbDataAdapter(parancs);

                adapter.DeleteCommand = kapcsolat.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;

                //PROMPT FOR CONFIRMATION
                if (MessageBox.Show("Biztosan törlöd az adatot?", "TÖRLÉS", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (parancs.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sikeresen Törölted!");
                    }
                }

                kapcsolat.Close();

                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kapcsolat.Close();
            }
        }

        private void clearTxts()
        {
            
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
            
            
            string id2 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(id2);
            textBox1.Text = id.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            
            


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = 0;
            index = dataGridView1.CurrentCell.RowIndex;

            int ID = Convert.ToInt32(dataGridView1.Rows[index].Cells["Term_ID"].Value);
            ID = Convert.ToInt32(textBox1.Text);

            string termekek = Convert.ToString(dataGridView1.Rows[index].Cells["Termek"].Value);
            textBox2.Text = termekek;

            string kategoria = Convert.ToString(dataGridView1.Rows[index].Cells["Kategoria"].Value);
            textBox3.Text = kategoria;

            string leiras = Convert.ToString(dataGridView1.Rows[index].Cells["Leiras"].Value);
            textBox4.Text = leiras;

            string mennyiseg = Convert.ToString(dataGridView1.Rows[index].Cells["Mennyiseg"].Value);
            textBox5.Text = mennyiseg;

            string egysegar = Convert.ToString(dataGridView1.Rows[index].Cells["Egysegar"].Value);
            textBox6.Text = egysegar;
        }

        private void Hozzaad_Click(object sender, EventArgs e)
        {
            add(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            retrieve();
        }

        private void eltavolit_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            delete(id);
            clearTxts();
            retrieve();
        }

        private void szerkeszt_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            int id = Convert.ToInt32(selected);

            update( id,textBox2.Text, textBox4.Text, textBox3.Text,  textBox5.Text, textBox6.Text);
            retrieve();
        }

        private void frissit_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            kezdolap kezdolap = new kezdolap();
            kezdolap.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Termék összesítő";
            printer.SubTitle = string.Format("Dátum: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Szakvizsga2018@Győrvári Péter";
            printer.FooterSpacing = 15;
            printer.PrintDialogSettings.ShowNetwork.ToString();
            printer.PrintDataGridView(dataGridView1);
        }
    }
}
