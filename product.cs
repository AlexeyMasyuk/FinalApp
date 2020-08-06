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
    public class Product : IEquatable<Product>
    {
        private string product_name;
        private string product_category;
        private string product_price;
        private Image product_image;

        public Product(string name, string price, string category, byte[] imageBytes):this(name, price)
        {
            Image = strToImage(imageBytes);
            Category = category;
        }

        public Product(string name, string price)
        {
            Name = name;            
            Price = price;
        }

        private Image strToImage(byte[] imageBytes)
        {
            ImageConverter ic = new ImageConverter();
            return (Image)ic.ConvertFrom(OleImageUnwrap.GetImageBytesFromOLEField(imageBytes));
        }

        public string Name
        {
            get { return product_name; }
            set
            {
                product_name = value;
            }
        }

        public string Category
        {
            get { return product_category; }
            set
            {
                product_category = value;
            }
        }

        public string Price
        {
            get { return product_price; }
            set
            {
                product_price = value;
            }
        }

        public Image Image
        {
            get { return product_image; }
            set
            {
                if (value.Size.Width < 440 && value.Size.Height < 440)
                    product_image = value;
                else
                    throw new Exception("Wrong image Size ");
            }
        }

        public bool Equals(Product product)
        {
            return Name == product.Name && Price == product.Price;
        }
    }
}
