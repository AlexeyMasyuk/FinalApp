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
            /*if(ProductValidator.boxAddValidation(nameBox.Text.ToString(), categoryBox.Text.ToString(), priceBox.Text.ToString(), picturePathBox.Text.ToString()))*/
                /* ADD to DB */
        }

        private void addFromFileBtn_Click(object sender, EventArgs e)
        {
            Product[] products = ProductValidator.fileCheck(addFromFilePathBox.Text.ToString());
           /* if (products != null)*/
               
        }
    }
}
