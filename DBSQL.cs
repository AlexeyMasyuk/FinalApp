/** Alexey Masyuk and Yulia Berkovich **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using FinalApp;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/* Class to handle DB
 * students data
 * citys data
 * names data */

namespace DBclassHWado.net
{
    public class DBSQL : DbAccess
    {
        private static string conString;
        private static DBSQL instance;
        private DBSQL(string conString)
        : base(conString)
        {
        }

        /* initiate DB connection */
        public static DBSQL Instance
        {
            get
            {
                if (instance == null)
                {
                    return new DBSQL(conString);
                }
                return instance;
            }
        }

        public static string ConnectionString
        {
            get { return conString; }
            set
            {
                conString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+value+";Persist Security Info=False;";
            }
        }

        /* Adds students object data to students DB */
        public void InsertPicture(string name, string price, string category, byte[] imagePath)
        {            
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO products (product_name, product_price, product_category, ole_pic) values (@p1, @p2, @p3, @p4)";
            cmd.Parameters.AddWithValue("@p1", name);
            cmd.Parameters.AddWithValue("@p2", price);
            cmd.Parameters.AddWithValue("@p3", category);
            cmd.Parameters.AddWithValue("@p4", imagePath);
            ExecuteSimpleQuery(cmd);
        }

        private bool nameInsert(string name, string email,string table)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO "+ table + " values (@p1, @p2)";
            cmd.Parameters.AddWithValue("@p1", name);
            cmd.Parameters.AddWithValue("@p2", email);
            try { 
                ExecuteSimpleQuery(cmd);
                return true;
            }
            catch { return false; }
        }

        public bool nameCheck(string name, string email, string customer, bool insert)
        {
            int res = 0;
            string[] data = { "suppliers", "supplier_name" };
            if (customer == "cust") 
            {
                data[0] = "customers";
                data[1] = "cus_name";
            }
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(" + data[1] + ") FROM " + data[0] + " WHERE " + data[1] + "='" + name + "'";
            try
            {
                res = ExecuteScalarIntQuery(cmd);
                if (!insert && res > 0)
                    return true;
                else if(!insert && res > 0)
                    return false;
                if (insert && res == 0)
                    if (nameInsert(name, email, data[0]))
                        return true;
                return false;               
            }
            catch 
            {
                return false;
            }
        }

        public void picGet()
        {
            DataSet ds = new DataSet();
            byte[][] picByte=new byte[3][];
            string cmdStr = "SELECT * FROM imageTest";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    picByte[i] = (byte[])dt.Rows[i][1];
            }
            ImageConverter ic = new ImageConverter();
            Image img = (Image)ic.ConvertFrom(OleImageUnwrap.GetImageBytesFromOLEField(picByte[0]));//here the exception comes
            img.Save("fromSQLtest.jpg");
        }

        /* gets how much students records in students DB
         * Returns number of students */
        public int GetProductsNumber()
        {
            int result;
            string cmdStr = "SELECT COUNT (*) FROM products";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                result = ExecuteScalarIntQuery(command);
            }
            return result;
        }



        /* gets students data from students DB 
         Returns students object array */
        public Product[] GetProducts()
        {
            DataSet ds = new DataSet();
            Product[] Products = new Product[GetProductsNumber()];
            string cmdStr = "SELECT * FROM Products";
            using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                ds = GetMultipleQuery(command);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0)
            {
                Products = new Product[dt.Rows.Count];
                for (int i = 0; i < Products.Length; i++)
                {
                    int id;
                    Int32.TryParse(dt.Rows[i][0].ToString(), out id);
                    Products[i] = new Product(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), (byte[])dt.Rows[i][4]);

                }
            }
            return Products;
        }

    }
}
