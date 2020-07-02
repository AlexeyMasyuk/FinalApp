using System;
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
        private ListView cartList;

        public cartHandler(ListView cartListView)
        {
            Price = 0;
            Cart = cartListView;
            cartTitleSet();
        }

        private int Price
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private ListView Cart
        {
            get { return cartList; }
            set { cartList = value; }
        }

        private void cartTitleSet()
        {
            Cart.Items.Add("Name Price Amount".Replace("\n", "\\s"));
        }


    }
}
