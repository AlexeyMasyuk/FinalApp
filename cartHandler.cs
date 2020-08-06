using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Alexey Masyuk and Yulia Berkovich
 * class to handle products in cart.
*/

namespace FinalApp
{

    class cartHandler
    {
        private int totalPrice;
        private Label priceLabel;
        private ListView cartList;
        private ArrayList products;
        private Dictionary<string, int> amount { get; set; }

        /* Constructor */
        public cartHandler(ListView cartListView, Label label)
        {
            Cart = cartListView;
            priceLabel = label;
            cartSet(cartListView);
        }

        /*
         * Within class method, used in Constructor or after sort.
         * Initialize/Reset Price, Products and amount dictionary.
        */
        private void cartSet(ListView cartListView)
        {
            Price = 0;                 
            Products = new ArrayList(1);
            amount = new Dictionary<string, int>();
            cartTitleSet();
        }

        /* ----------- Get/Set ----------- */
        private ArrayList Products
        {
            get { return products; } 
            set { products = value; }
        }
        private int Price
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        private Label PriceLabel
        {
            get { return priceLabel; }
            set { priceLabel = value; }
        }
        private ListView Cart
        {
            get { return cartList; }
            set { cartList = value; }
        }
        /* ------------------------------- */

        /*
         * Within class method, used in 'cartSet(ListView cartListView)'.
         * initially sets all cart titles.
        */
        private void cartTitleSet()
        {            
            Cart.Items.Add("Name");
            Cart.Items.Add("Price");
            Cart.Items.Add("Amount");
            Cart.Items.Add("Delete");
        }

        /*
         * Within class method, used in 'add(DataGridViewRow row)'.
         * Checking if product exists in cart
        */
        private bool exists(Product newProduct)
        {
            if (amount.ContainsKey(newProduct.Name.ToString()))
                return true;
            return false;
        }

        /*
         * Within class method, used in 'remove(ListViewItem item)' and 'add(DataGridViewRow row)'.
         * Updates price, if 'plus' flag is true then add to total price else decrease from total price,
         * and updates total price label.
        */
        private void priceHandle(string price, bool plus)
        {
            int tmpPrice;
            Int32.TryParse(price, out tmpPrice);
            if (plus)
                Price += tmpPrice;
            else
                Price -= tmpPrice;
            PriceLabel.Text = Price.ToString();
        }

        /*
         * Within class method, used in 'add(DataGridViewRow row)'.
         * Used in first add to Cart
        */
        private void cartListHandle(Product product, int amount)
        {
            ListViewItem item = new ListViewItem(product.Name);
            item.Name = product.Name;
            cartList.Items.Add(item);
            cartList.Items.Add(product.Price + '₪');
            cartList.Items.Add(amount.ToString());
            cartList.Items.Add("X");
        }

        /*
         * Within class method, used in 'remove(ListViewItem item)'.
         * Removes 'Product' from Cart.
        */
        private void removeFromList(int itemIndex)
        {
            for (int i = 0; i < 4; i++)
                cartList.Items.Remove(cartList.Items[itemIndex - i]);
        }

        /*
         * Removes 'Product' from Cart.
         * Updates amount, total price
         * if its the last product (spesific) remove all from cart (Product name, Price, Amount and X)
         * else update amount and in cart price.
        */
        public void remove(ListViewItem item)
        {
            string productName = cartList.Items[item.Index-3].Text.ToString();
            string price = cartList.Items[item.Index - 2].Text.ToString();
            amount[productName]--;
            priceHandle(price.Remove(price.Length - 1, 1), false);
            if (amount[productName] == 0)
            {
                removeFromList(item.Index);
                amount.Remove(productName);
            }
            else
                itemAmount_inCartPrice_Update(productName);
        }

        /*
         *  Within class method, used in 'itemAmount_inCartPrice_Update(string name)'.
         *  Search and return index of product in array by name.
        */
        private int searchByName(string name)
        {
            int i = 0;
            foreach (Product p in Products)
            {
                if (p.Name == name)
                    return i;
                i++;
            }
            return -1;
        }

        /*
         *  Within class method, used in 'add(DataGridViewRow row)' and 'remove(ListViewItem item)'.
         *  Update in cart amount and price of specific product.
        */
        private void itemAmount_inCartPrice_Update(string name)
        {
            ListViewItem[] item = cartList.Items.Find(name, false);
            cartList.Items[item[0].Index + 2].Text = amount[name].ToString();
            int tmpPrice;
            Product p = (Product)Products[searchByName(name)];
            Int32.TryParse(p.Price, out tmpPrice);
            cartList.Items[item[0].Index + 1].Text = (amount[name] * tmpPrice).ToString() + "₪";
        }

        /*
         * Add 'Product' to Cart.
         * Updates amount, total price
         * if its the first product call 'cartListHandle(Product product, int amount)'
         * else update amount and in cart price.
        */
        public void add(DataGridViewRow row)
        {
            Product product = new Product(row.Cells["Name"].Value.ToString(), row.Cells["Price"].Value.ToString());
            if (!exists(product))
            {
                Products.Add(product);
                amount.Add(product.Name.ToString(), 1);
                cartListHandle(product, amount[product.Name]);
            }
            else
            { 
                amount[product.Name]++;
                itemAmount_inCartPrice_Update(product.Name);
            }
            priceHandle(product.Price, true);           
        }

        /*
         * Clear all cart.
        */
        public void clear()
        {
            cartList.Clear();
            cartSet(cartList);
            priceLabel.Text = "";
        }

        /*
         * Creatin PDF with in cart data (Product names, prises and amounts).
        */
        public bool buy_toPDF()
        {
            if (Products.Count != 0)
            {
                string[,] cartToBuy = new string[Cart.Items.Count / 4, 3];   //ta
                int rows = Cart.Items.Count / 4;     //

                for (int i = 0, k = 0; i < rows; i++, k++)
                    for (int j = 0; j < 3; j++)
                        cartToBuy[i, j] = Cart.Items[k++].Text;

                try
                {
                    PDFcreate newPdf = new PDFcreate();
                    newPdf.SetTitle("Dear customer!\nThanks for your purchase!");
                    newPdf.SetTable(cartToBuy);
                    newPdf.SetTitle("Total Price: " + PriceLabel.Text.ToString());
                    newPdf.PDFclose();

                    return true;
                }
                catch
                { return false; }
            }
            return false;
        }
    }
}
