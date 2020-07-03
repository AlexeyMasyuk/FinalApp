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
        private cartHandler cart;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tableSet();
            tableSet(products[0].Name, products[0].Price, products[0].Category, products[0].Image, 0);
            tableSet(products[1].Name, products[1].Price, products[1].Category, products[1].Image, 1);
            cart = new cartHandler(cartList,price);
        }

        private void tableSet(string name, string price, string category, Image image, int index )
        {
            dgv[0, index].Value = name;            
            dgv[1, index].Value = price;
            dgv[2, index].Value = category;
            dgv[3, index].Value = image;
            dgv.RowTemplate.Resizable = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < 3; i++)
                dgv[i, index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void addImgCol_changeTitleCell(int index)
        {
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            dgv.Columns.Add(imageCol);
        }

        private void tableSet()
        {
            dgv.RowCount = products.Length;
            dgv.ColumnCount = 3;
            string[] titles = { "Name", "Price", "Category", "Image" };
            for(int i = 0; i < 4; i++)
            {
                if (i == 3)
                    addImgCol_changeTitleCell(i);
                else
                    dgv[i, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[i].Name = titles[i];
            }
        }

        private void dgv_CellDoubleClick(object sender, MouseEventArgs e)
        {
            cart.add(dgv.Rows[dgv.SelectedCells[0].RowIndex]);
        }

        private void cartList_Click(object sender, EventArgs e)
        {
            if (cartList.SelectedItems[0].Text.ToString() == "X")
                cart.remove(cartList.SelectedItems[0]);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            cart.clear();
        }
    }
}