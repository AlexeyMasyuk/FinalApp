using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Form2 : Form
    {
        public Product[] products;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            dgv.RowCount = products.Length + 1;
            dgv.ColumnCount = 3;
            tableTitleSet();
            tableSet(products[0].Name, products[0].Price, products[0].Category, products[0].Image, 1);
            tableSet(products[1].Name, products[1].Price, products[1].Category, products[1].Image, 2);
        }

        private void tableSet(string name, string price, string category, Image image, int index )
        {
            dgv[0, index].Value = name;
            dgv[0, index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv[1, index].Value = price;
            dgv[1, index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv[2, index].Value = category;
            dgv[2, index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            Image image1 = Image.FromFile(Application.StartupPath + @"\..\..\DBpics\tvPic.jpg");
            dgv[3, index].Value = image;

        }

        private void tbl()
        {
            DataGridViewColumn col = new DataGridViewColumn();
            col.HeaderText = "Name";
            dgv[0, 0].Value = col;
            col.HeaderText = "Price";
            dgv[1, 0].Value = col;
            col.HeaderText = "Category";
            dgv[2, 0].Value = col;
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "Image";
            dgv.Columns.Add(imageCol);
        }

        private void tableTitleSet()
        {
            dgv[0, 0].Value = "Name";
            dgv[0, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv[1, 0].Value = "Price";
            dgv[1, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv[2, 0].Value = "Category";
            dgv[2, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "Image";
            dgv.Columns.Add(imageCol);
            dgv[3, 0] = new DataGridViewTextBoxCell();
            dgv[3, 0].Value = "Image";
        }
    }
}
