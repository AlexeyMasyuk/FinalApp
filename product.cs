using DBclassHWado.net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    public class Product
    {
        private string product_name;
        private string product_category;
        private string product_price;
        private Image product_image;

        public Product(string name, string price, string category, string imagePath)
        {
            Name = name;
            Category = category;
            Price = price;
            Image = strToImage(imagePath);
        }

        private Image strToImage(string imagePath)
        {
            Image image = Image.FromFile(Application.StartupPath + imagePath);
            return image;
        }

        public string Name
        {
            get { return product_name; }
            set
            {
                if (value.Length > 0)
                    product_name = value;
            }
        }

        public string Category
        {
            get { return product_category; }
            set
            {
                if (value.Length > 0)
                    product_category = value;
            }
        }

        public string Price
        {
            get { return product_price; }
            set
            {
                if (value.Length > 0)
                    product_price = value;
            }
        }

        public Image Image
        {
            get { return product_image; }
            set
            {
                product_image = value;
            }
        }

    }
}
