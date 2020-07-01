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
        private int cartSum;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dgv.RowCount = products.Length ;
            dgv.ColumnCount = 3;
            tableTitleSet();
            tableSet(products[0].Name, products[0].Price, products[0].Category, products[0].Image, 0);
            tableSet(products[1].Name, products[1].Price, products[1].Category, products[1].Image, 1);
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

        private void tableTitleSet()
        {
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

        private void cartPriceUpdate(string numStr, bool plus)
        {
            int tmp = 0;
            Int32.TryParse(numStr, out tmp);
            if(plus)
                cartSum += tmp;
            else
                cartSum -= tmp;
            price.Text = cartSum.ToString();
        }

        private void addToCart(string selectedPrice, int selectedrowindex)
        {
            cartList.Items.Add((cartList.Items.Count / 3 + 1).ToString() + ". " + dgv.Rows[selectedrowindex].Cells["Name"].Value.ToString());
            cartList.Items.Add(selectedPrice + "₪");
            cartList.Items.Add("X");
        }

        private void dgv_CellDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedRowIndex = dgv.SelectedCells[0].RowIndex;
            string selected = dgv.Rows[selectedRowIndex].Cells["Price"].Value.ToString();
            cartPriceUpdate(selected, true);
            addToCart(selected, selectedRowIndex);
        }

        private void removeFromCart()
        {
            cartList.Items.RemoveAt(cartList.SelectedItems[0].Index - 2);
            cartList.Items.RemoveAt(cartList.SelectedItems[0].Index - 1);
            cartList.Items.Remove(cartList.SelectedItems[0]);
        }

        private void cartList_Click(object sender, EventArgs e)
        {
            if (cartList.SelectedItems[0].Text.ToString() == "X")
            {
                int count = cartList.Items[cartList.SelectedItems[0].Index - 1].Text.ToString().Count() - 1;
                string numStr = cartList.Items[cartList.SelectedItems[0].Index - 1].Text.ToString().Remove(count, 1);
                cartPriceUpdate(numStr, false);
                removeFromCart();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            cartList.Items.Clear();
            cartSum = 0;
            price.Text = "";
        }
    }
}