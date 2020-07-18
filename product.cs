﻿using DBclassHWado.net;
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

        public Product(string name, string price, string category, string imagePath):this(name, price)
        {
            Image = strToImage(imagePath);
            Category = category;
        }

        public Product(string name, string price)
        {
            Name = name;            
            Price = price;
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
                product_image = value;
            }
        }

        public bool Equals(Product product)
        {
            return Name == product.Name && Price == product.Price;
        }
    }
}
