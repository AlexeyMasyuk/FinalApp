//Alexey Masyuk, Yulia Berkovich, 43/5 

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
using Prism.Services.Dialogs;

namespace FinalApp
{
    public partial class Form2 : Form
    {
        public Product[] products;  // products for creating data grid view
        private cartHandler cart;   //selected objects for purchase(for filling list view)
        private DataGridHandler dataGrid;

        private Product[] Products
        {
            get { return products; }
        }

        private cartHandler Cart
        {
            get { return cart; }
            set { cart = value; }
        }

        private DataGridHandler DataGrid
        {
            get { return dataGrid; }
            set { dataGrid = value; }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataGrid = new DataGridHandler(Products, dgv, categoryShowBox);
            Cart = new cartHandler(cartList,price);
        }

		//Adding selected items to list view
        private void dgv_CellDoubleClick(object sender, MouseEventArgs e)
        {
            Cart.add(dgv.Rows[dgv.SelectedCells[0].RowIndex]);
        }

		//Remove the selected item from list view
        private void cartList_Click(object sender, EventArgs e)
        {
            if (cartList.SelectedItems[0].Text.ToString() == "X")
                Cart.remove(cartList.SelectedItems[0]);
        }

		//Clear list view
        private void clearBtn_Click(object sender, EventArgs e)
        {
            Cart.clear();
        }

		//Sorting by price(from low to high or high to low)
        private void sortBtn_Click(object sender, EventArgs e)
        {
            if (sortBox.SelectedIndex >= 0)
            {
                if (sortBox.SelectedItem.ToString() == "Low Price")
                    DataGrid.fromLowPrice();
                else if (sortBox.SelectedItem.ToString() == "High Price")
                    DataGrid.fromHighstPrice();
                else
                    DataGrid.a_zSort();
            }
        }

		//Sorting by category
        private void catShowBtn_Click(object sender, EventArgs e)
        {
            if (categoryShowBox.SelectedIndex >= 0)
                DataGrid.categorySort(categoryShowBox.SelectedItem.ToString());
        }

        //sending a file pdf(selected products from the list) on button click
        private void buyBtn_Click(object sender, EventArgs e)
        {
            if (Cart.buy_toPDF())   //if pdf file is created, the user can choose to exit the application or continue shopping
            {
                System.Windows.Forms.DialogResult dr;
                dr = MessageBox.Show("PayCheck created.\nDo you want to exit?.", "Done", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Environment.Exit(1); //exit application
                }
            }

        }
    }
}