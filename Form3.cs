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

        private void addFromBoxesBtn_Click(object sender, EventArgs e)
        {
            string[] boxData = { nameBox.Text.ToString(), categoryBox.Text.ToString(), priceBox.Text.ToString(), picturePathBox.Text.ToString() };
            if (ProductValidator.boxAddValidation(boxData[0], boxData[1], boxData[2], boxData[3]))
                CON.InsertPicture(boxData[0], boxData[1], boxData[2], File.ReadAllBytes(boxData[3]));
        }

        private static byte[] imageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void addFromFileBtn_Click(object sender, EventArgs e)
        {
            Product[] products = ProductValidator.fileCheck(addFromFilePathBox.Text.ToString());
            if (products != null)
                for (int i = 0; i < products.Length; i++)
                    CON.InsertPicture(products[i].Name, products[i].Price, products[i].Category, imageToByteArray(products[i].Image));

        }
    }
}
