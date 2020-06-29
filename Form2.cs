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
            dgv[1, index].Value = price;
            dgv[2, index].Value = category;
            dgv[3, index].Value = image;
            for(int i=0;i<3;i++)
                dgv[i, index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void addImgCol_changeTitleCell(int index)
        {
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            dgv.Columns.Add(imageCol);
            dgv[index, 0] = new DataGridViewTextBoxCell();
            dgv[index, 0].Value = "Image";
        }

        private void tableTitleSet()
        {
            string[] titles = { "Name", "Price", "Category", "Image" };
            for(int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    addImgCol_changeTitleCell(i);
                    break;
                }
                dgv[i, 0].Value = titles[i];
                dgv[i, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}
