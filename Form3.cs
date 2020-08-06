//Alexey Masyuk, Yulia Berkovich, 43/5 
//supplier options form(adding a new product:name,category,price,picture) 

using DBclassHWado.net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    public partial class Form3 : Form
    {
        private DBSQL dataB;

        public DBSQL CON
        {
            get { return dataB; }
            set { dataB = value; }
        }

        public Form3()
        {
            InitializeComponent();
        }

		// filling product information
        private void addFromBoxesBtn_Click(object sender, EventArgs e)
        {
            string[] boxData = { nameBox.Text.ToString(), categoryBox.Text.ToString(), priceBox.Text.ToString(), picturePathBox.Text.ToString() };
            if (ProductValidator.boxAddValidation(boxData[0], boxData[1], boxData[2], boxData[3]))
                CON.InsertPicture(boxData[0], boxData[1], boxData[2], File.ReadAllBytes(boxData[3]));
            else
                MessageBox.Show("Fill all fields", "Empty field", MessageBoxButtons.OK);
        }

		//saving the image to byte array
        private static byte[] imageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

		//adding a new product from file
        private void addFromFileBtn_Click(object sender, EventArgs e)
        {
            Product[] products = ProductValidator.fileCheck(addFromFilePathBox.Text.ToString());
            if (products != null)
                for (int i = 0; i < products.Length; i++)
                    CON.InsertPicture(products[i].Name, products[i].Price, products[i].Category, imageToByteArray(products[i].Image));

        }

		//format 
        private void formatBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Name*Price(Positive Natural Number)*Category*Product Image Path\n" +
                "Max Image Size: 420x420\n" +
                "Max size of Name, Price, Category is 14 characters.", "Format", MessageBoxButtons.OK);
        }
    }
}
