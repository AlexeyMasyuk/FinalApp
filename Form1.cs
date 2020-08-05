using DBclassHWado.net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    public partial class Form1 : Form
    {
        private DBSQL dataB;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dbPath = Application.StartupPath + @"\..\..\shop.accdb";
            if (File.Exists(dbPath))
            {
                DBSQL.ConnectionString = dbPath;
                dataB = DBSQL.Instance;
            }
            else     // if DB file not exist
            {
                MessageBox.Show("DataBase" + dbPath + " not found");
                Application.Exit();
            }
        }

        private bool nameCheck(string cust)
        {
            if (nameBox.TextLength > 0)
                if (dataB.nameCheck(nameBox.Text.ToString(), "", cust, false))
                    return true;
            return false;
        }

        private void customerBtn_Click(object sender, EventArgs e)
        {
            if (nameCheck("cust"))
            {
                Form1.ActiveForm.Hide();
                using (Form2 form2 = new Form2())
                {
                    form2.products = dataB.GetProducts();
                    form2.ShowDialog();
                    Visible = true;
                }
            }
        }

        private void supplierBtn_Click(object sender, EventArgs e)
        {
            if (nameCheck("supp"))
            {
                Form1.ActiveForm.Hide();
                using (Form3 form3 = new Form3())
                {
                    form3.CON = dataB;
                    form3.ShowDialog();
                    Visible = true;
                }
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Hide();
            using (Form4 form4 = new Form4())
            {
                form4.CON = dataB;
                form4.ShowDialog();
                Visible = true;
            }
        }
    }
}
