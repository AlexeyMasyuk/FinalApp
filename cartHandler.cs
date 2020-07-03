using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{

    class cartHandler
    {
        private int totalPrice;
        private Label priceLabel;
        private ListView cartList;
        private ArrayList products;
        private Dictionary<string, int> amount { get; set; }

        private void cartSet(ListView cartListView, Label label)
        {
            Price = 0;
            priceLabel = label;
            Cart = cartListView;
            products = new ArrayList(1);
            amount = new Dictionary<string, int>();
            cartTitleSet();
        }

        public cartHandler(ListView cartListView, Label label)
        {
            cartSet(cartListView, label);
        }

        private ArrayList Products
        {
            get { return products; }         
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

        private void cartTitleSet()
        {            
            Cart.Items.Add("Name");
            Cart.Items.Add("Price");
            Cart.Items.Add("Amount");
            Cart.Items.Add("Delete");
        }

        private bool exists(Product newProduct)
        {
            if (amount.ContainsKey(newProduct.Name.ToString()))
                return true;
            return false;
        }

        private void priceHandle(string price, bool plus)
        {
            int tmpPrice;
            Int32.TryParse(price, out tmpPrice);
            if (plus)
                Price += tmpPrice;
            else
                Price -= tmpPrice;
            priceLabel.Text = Price.ToString();
        }

        private void cartListHandle(Product product, int amount)
        {
            ListViewItem item = new ListViewItem(product.Name);
            item.Name = product.Name;
            cartList.Items.Add(item);
            cartList.Items.Add(product.Price + '₪');
            cartList.Items.Add(amount.ToString());
            cartList.Items.Add("X");
        }

        private void removeFromList(int itemIndex)
        {
            for (int i = 0; i < 4; i++)
                cartList.Items.Remove(cartList.Items[itemIndex - i]);
        }

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
                cartList.Items[item.Index - 1].Text = amount[productName].ToString();           
        }

        private void itemAmountUpdate(string name)
        {
            ListViewItem[] item = cartList.Items.Find(name, false);
            cartList.Items[item[0].Index + 2].Text = amount[name].ToString();
        }

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
                itemAmountUpdate(product.Name);
            }
            priceHandle(product.Price, true);           
        }

        public void clear()
        {
            cartList.Clear();
            cartSet(cartList, priceLabel);
            priceLabel.Text = "";
        }
    }
}
