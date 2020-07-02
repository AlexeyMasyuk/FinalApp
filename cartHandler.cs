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
        private Dictionary<Product, int> amount { get; set; }


        public cartHandler(ListView cartListView, Label label)
        {
            Price = 0;
            Cart = cartListView;
            products = new ArrayList(1);
            amount = new Dictionary<Product, int>();
            cartTitleSet();
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
        }

        private bool exists(Product newProduct)
        {
            if (Products.Contains(newProduct))
                return true;
            return false;
        }

        private void priceHandle(Product product, bool plus)
        {
            int tmpPrice;
            Int32.TryParse(product.Price, out tmpPrice);
            if (plus)
                Price += tmpPrice;
            else
                Price -= tmpPrice;
            
        }

        public void add(DataGridViewRow row)
        {
            Product product = new Product(row.Cells["Name"].Value.ToString(), row.Cells["Price"].Value.ToString());
            if (!exists(product))
            {
                Products.Add(product);
                amount.Add(product, 1);
            }
            else
                amount[product]++;

            cartList.Items.Add(product.Name);
            cartList.Items.Add(product.Price);
            cartList.Items.Add(amount[product].ToString());
            cartList.Items.Add("X");
        }
    }
}
