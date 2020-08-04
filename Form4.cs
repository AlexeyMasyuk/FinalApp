using DBclassHWado.net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    public partial class Form4 : Form
    {
        private DBSQL dataB;

        public DBSQL CON
        {
            get { return dataB; }
            set { dataB = value; }
        }

        public Form4()
        {
            InitializeComponent();
        }

        private string fieldsFillCheck()
        {
            if (choiseBox.SelectedItem != null)
                if (choiseBox.SelectedItem.ToString().Length > 0)
                    if (nameBox.Text.ToString().Length > 0)
                        if (emailBox.Text.ToString().Length > 0)
                        {
                            if (choiseBox.SelectedItem.ToString() == "Supplier")
                                return "sup";
                            return "cust";
                        }
            return null;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string cust = fieldsFillCheck();
            if (cust != null)
                CON.nameCheck(nameBox.Text.ToString(), cust);
        }
    }
}
