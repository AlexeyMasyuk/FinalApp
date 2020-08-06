//Alexey Masyuk, Yulia Berkovich 43/5
// Class for creating a pdf file

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FinalApp
{
    class PDFcreate
    {
        private Document doc;

        /* Get/Set storing document parameter */
        private Document Doc
        {
            get { return doc; }
            set { doc = value; }
        }
        public PDFcreate()
        {
            Doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(nameGenerate(), FileMode.Create));
            //open pdf file
            Doc.Open();
        }

        public PDFcreate(string fileName)
        {
            Doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
            //open pdf file
            Doc.Open();
        }

        /* Function set/writes title to pdf and seting place for image */
        public void SetTitle(string title)
        {
            Font myFont = new Font(Font.FontFamily.COURIER, 18, Font.ITALIC);
            Doc.Add(new Paragraph(title, myFont));
            Doc.Add(new Paragraph("\n\n", myFont));
        }



        /* Function  adding string 2d array to table in pdf file */
        public void SetTable(string[,] table)
        {
            Font myFont = new Font(Font.FontFamily.COURIER, 12, Font.NORMAL);
            PdfPTable myTable = new PdfPTable(table.GetLength(1));
            myTable.HorizontalAlignment = Element.ALIGN_CENTER;


            float[] widthCell = new float[table.GetLength(1)];
            for (int i = 0; i < table.GetLength(1); i++)
                widthCell[i] = 10;
            myTable.SetTotalWidth(widthCell);

            PdfPCell myCell = new PdfPCell();
            myCell.FixedHeight = 20;
            myCell.BorderColor = BaseColor.DARK_GRAY;
            myCell.HorizontalAlignment = Element.ALIGN_CENTER;
            myCell.VerticalAlignment = Element.ALIGN_CENTER;

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    myCell.Phrase = new Phrase(table[i, j].ToString(), myFont);
                    myTable.AddCell(myCell);
                }
            }
            Doc.Add(myTable);
        }

		//creating file name 
        public string nameGenerate()
        {
            if (File.Exists("ToCustomer.pdf"))      
            {
                int i = 1;
                for (; File.Exists("ToCustomer" + i + ".pdf"); i++) ;
                return "ToCustomer" + i + ".pdf";
            }
            return "ToCustomer.pdf";
        }

        /* Close PDF file */
        public void PDFclose()
        {
            Doc.Close();
        }
    }
}
