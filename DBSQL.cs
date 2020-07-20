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
        public void InsertStudent()
        {
            var pic = File.ReadAllBytes(Application.StartupPath + @"\..\..\mouse.jpg");
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO imageTest (picture) values (@p1)";
            cmd.Parameters.AddWithValue("@p1", pic);
            ExecuteSimpleQuery(cmd);

/*            string cmdStr = "INSERT INTO Students (id, studentName, studentCity) VALUES(@id, @studentName, @studentCity)";
        using (OleDbCommand command = new OleDbCommand(cmdStr))
            {
                if (Item.Id == -1)
                {
                    command.Parameters.AddWithValue("@id", GetStudentNumber());
                }
                else
                {
                    command.Parameters.AddWithValue("@id", Item.Id);
                }
                command.Parameters.AddWithValue("@studentName", Item.FirstName);
                command.Parameters.AddWithValue("@studentCity", Item.CityName);
                base.ExecuteSimpleQuery(command);
            }*/
        }

        /* gets how much students records in students DB
         * Returns number of students */
        public int GetStudentNumber()
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
            Product[] Products = new Product[GetStudentNumber()];
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
                    Products[i] = new Product(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString());

                }
            }
            return Products;
        }

    }
}
