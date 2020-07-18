using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    class DataGridHandler
    {
        private DataGridView in_dataGrid;
        private Product[] in_products;

        public DataGridHandler(Product[] products, DataGridView dataGridView)
        {
            DataGrid = dataGridView;
            Products = products;
            titleSet();
            Products = charSort();
            tableSet();
        }

        private DataGridView DataGrid
        {
            get { return in_dataGrid; }
            set { in_dataGrid = value; }
        }
        private Product[] Products
        {
            get { return in_products; }
            set { in_products = value; }
        }

        private void addImgCol_changeTitleCell(int index)
        {
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            DataGrid.Columns.Add(imageCol);
        }

        private void titleSet()
        {
            DataGrid.RowCount = Products.Length;
            DataGrid.ColumnCount = 3;
            string[] titles = { "Name", "Price", "Category", "Image" };
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                    addImgCol_changeTitleCell(i);
                else
                    DataGrid[i, 0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGrid.Columns[i].Name = titles[i];
            }
        }

        private void tableSet()
        {
            for(int i = 0; i < Products.Length; i++)
            {
                DataGrid[0, i].Value = Products[i].Name;
                DataGrid[1, i].Value = Products[i].Price;
                DataGrid[2, i].Value = Products[i].Category;
                DataGrid[3, i].Value = Products[i].Image;
                DataGrid.RowTemplate.Resizable = DataGridViewTriState.True;
                DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                for (int j = 0; j < 3; j++)
                    DataGrid[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private int notNullProduct()
        {
            for (int i = 0; i < Products.Length; i++)
                if (Products[i] != null)
                    return i;
            return -1;
        }

        private Product[] priceSort(string state)
        {
            Product[] newProducts = new Product[Products.Length];
            int criticalIndex = 0, newProductIndex = 0;
            for(int j = 0; j < Products.Length; j++)
            {
                for (int i = 0; i < Products.Length; i++)
                {
                    if (Products[i] != null)
                    {
                        if (Products[i].Price.CompareTo(Products[criticalIndex].Price) == 1 && state == "high")
                            criticalIndex = i;
                        if (Products[i].Price.CompareTo(Products[criticalIndex].Price) == -1 && state == "low")
                            criticalIndex = i;
                    }
                }
                newProducts[newProductIndex++] = Products[criticalIndex];
                Products[criticalIndex] = null;
                criticalIndex = notNullProduct();
            }
            return newProducts;
        }



        private Product[] charSort()
        {
            Product[] newProducts = new Product[Products.Length];
            CultureInfo culture = new CultureInfo("en-US", false);
            int criticalIndex = 0, newProductIndex = 0;
            for (int j = 0; j < Products.Length; j++)
            {
                for (int i = 0; i < Products.Length; i++)
                {                   
                    if (Products[i] != null)
                        if (Char.ToUpper(Products[i].Name[0], culture).CompareTo(Char.ToUpper(Products[criticalIndex].Name[0], culture)) < 0)
                            criticalIndex = i;
                }
                newProducts[newProductIndex++] = Products[criticalIndex];
                Products[criticalIndex] = null;
                criticalIndex = notNullProduct();
            }
            return newProducts;
        }

        private void hideShowGrid()
        {
            DataGrid.Hide();
            DataGrid.Show();
        }

        public void fromHighstPrice()
        {            
            Products = priceSort("high");
            tableSet();            
        }

        public void fromLowPrice()
        {
            Products = priceSort("low");
            tableSet();
        }

        public void a_zSort()
        {
            Products = charSort();
            tableSet();
        }
    }
}
