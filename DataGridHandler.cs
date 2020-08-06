using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Alexey Masyuk and Yulia Berkovich
 * class to handle products table
*/

namespace FinalApp
{
    class DataGridHandler
    {
        private DataGridView in_dataGrid;
        private Product[] in_products;
        private Product[] beforeCategorySort;

        /* Constructor */
        public DataGridHandler(Product[] products, DataGridView dataGridView, ComboBox categoryBox)
        {
            DataGrid = dataGridView;
            Products = products;
            titleSet();
            alphSort();
            tableSet();
            ProductsBeforeSort = null;
            categoryBoxFill(categoryBox);
        }

        /*
         * Within class method, used in Constructor.
         *  Method checking all existing category,
         *  adding them in to relevant box that can be sorted by.
        */
        private void categoryBoxFill(ComboBox categoryBox)
        {
            ArrayList categoryList= new ArrayList();
            for (int i = 0; i < Products.Length; i++)
                if (!categoryBox.Items.Contains(Products[i].Category))
                    categoryBox.Items.Add(Products[i].Category);                  
        }

        /* ----------- Get/Set ----------- */
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
        private Product[] ProductsBeforeSort
        {
            get { return beforeCategorySort; }
            set { beforeCategorySort = value; }
        }
        /* ------------------------------- */

        /*
         * Within class method, used in 'titleSet()' below.
         * Add image column to table.
         */
        private void addImgCol_changeTitleCell(int index)
        {
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            DataGrid.Columns.Add(imageCol);
        }

        /*
         * Within class method, used in Constructor.
         * initially sets all table titles.
        */
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

        /*
         * Within class method, used in Constructor or after sort.
         * Filling the table with data stored in Products var.
        */
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

        /*
         * Within class method, used in 'priceSort(string state)' and 'alphSort()'.
         * generate new index from Products that not null
        */
        private int notNullProduct()
        {
            for (int i = 0; i < Products.Length; i++)
                if (Products[i] != null)
                    return i;
            return -1;
        }

        /*
         * Within class method, used in 'fromHighstPrice()' and 'fromLowPrice()' below.
         * Sorting from High or Low price depends on 'state'.
        */
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
                Products[criticalIndex] = null;    // After product copied to new sorted array.
                criticalIndex = notNullProduct();  // generate new index from Products that not null
            }
            return newProducts;
        }

        /*
         * Within class method, used in 'a_zSort()' below.
         * Sorting from A - Z .
        */
        private void alphSort()
        {
            Product[] newProducts = new Product[Products.Length];
            int criticalIndex = 0, newProductIndex = 0;
            for (int j = 0; j < Products.Length; j++)
            {
                for (int i = 0; i < Products.Length; i++)
                {
                    if (Products[i] != null && string.Compare(Products[i].Name, Products[criticalIndex].Name) < 0)
                        criticalIndex = i;
                }
                newProducts[newProductIndex++] = Products[criticalIndex];
                Products[criticalIndex] = null;
                criticalIndex = notNullProduct();
            }
            Products = newProducts;
        }

        /*
         * Within class method, used in 'in_catSort(string category)' below.
         * Method counting appearence of given category.
        */
        private int categoryCount(string category)
        {
            int count = 0;
            for (int i = 0; i < Products.Length; i++)
                if (Products[i].Category == category)
                    count++;
            return count;
        }

        /*
         * Within class method, used in 'categorySort(string category)' below.
         * Sorting table by given category, saving all Products from table and deleting all not chosen category.
         * If 'All' selected retriving all deleted categories.
        */
        private void in_catSort(string category)
        {
            if (category != "All") 
            {
                DataGrid.Rows.Clear();
                int newIndex = 0;
                if (ProductsBeforeSort != null)
                    Products = ProductsBeforeSort;
                else
                    ProductsBeforeSort = Products;
                Product[] newProducts = new Product[categoryCount(category)];
                for (int i = 0; i < Products.Length; i++)
                    if (Products[i].Category == category)
                        newProducts[newIndex++] = Products[i];
                Products = newProducts;
                DataGrid.RowCount = Products.Length;
            }
            else if (category == "All" && ProductsBeforeSort != null) 
            {
                DataGrid.Rows.Clear();
                Products = ProductsBeforeSort;
                ProductsBeforeSort = null;
                DataGrid.RowCount = Products.Length;
            }
        }

        /* ---- Sorting Methods ---- */
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
            alphSort();
            tableSet();
        }
        public void categorySort(string category)
        {
            in_catSort(category);
            tableSet();
        }
        /* ------------------------- */
    }
}
