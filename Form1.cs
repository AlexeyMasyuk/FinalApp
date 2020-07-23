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
            ProductValidator.fileCheck(Application.StartupPath + @"\..\..\filetest.txt");
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

        private void customerBtn_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Hide();
            using (Form2 form2 = new Form2())
            {
                form2.products = dataB.GetProducts();               
                form2.ShowDialog();
                Visible = true;
            }           
        }

        private void supplierBtn_Click(object sender, EventArgs e)
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
}
