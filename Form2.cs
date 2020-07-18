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
        private DataGridHandler dataGrid;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGrid = new DataGridHandler(products, dgv);
            cart = new cartHandler(cartList,price);
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

        private void sortBtn_Click(object sender, EventArgs e)
        {
            if (sortBox.SelectedItem.ToString() == "Low Price")
                dataGrid.fromLowPrice();
            else if (sortBox.SelectedItem.ToString() == "High Price")
                dataGrid.fromHighstPrice();
            else
                dataGrid.a_zSort();
        }
    }
}