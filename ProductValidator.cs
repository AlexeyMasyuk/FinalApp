using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalApp
{
    public static class ProductValidator
    {
        public static bool boxAddValidation(string name,string category,string price,string imagePath)
        {
            int tmpPrice;
            if (name.Length > 0 && category.Length > 0 && price.Length > 0 && imagePath.Length > 0)
            {
                if (Int32.TryParse(price, out tmpPrice))
                    if (tmpPrice > 0)
                        if (File.Exists(imagePath))
                            return true;
            }
            return false;
        }

        private static bool lineFaultsCheck(int dataLengthFlag, int spaceSignCount,ref List<int> faultLines, char[] path, int i, int j, string[] lines, ref int pathIndex)
        {
            if ((dataLengthFlag > 14 || spaceSignCount > 3) && spaceSignCount != 3)
            {
                faultLines.Add(i);
                return true;
            }
            if (j + 1 == lines[i].Length)
            {

                if (spaceSignCount != 3 || !File.Exists(new string(path)))
                {
                    faultLines.Add(i);
                    return true;
                }
                pathIndex = 0;
            }
            return false;
        }

        private static void lineSeperatorCheckAndPathCopy(ref int dataLengthFlag, ref int spaceSignCount, ref int j,int i,ref char[] path, ref int pathIndex, string[] lines)
        {
            if (lines[i][j] == '*')
            {
                spaceSignCount++;
                dataLengthFlag = 0;
                j++;
            }
            if (spaceSignCount == 3)
            {
                if (path == null)
                    path = new char[lines[i].Length - j];
                path[pathIndex++] = lines[i][j];
            }
        }

        private static List<int> fileSyntexCheck(string fileString)
        {
            string[] lines = fileString.Split('\r');
            char[] path = null;
            List<int> faultLines = new List<int>();
            int dataLengthFlag = 0, spaceSignCount = 0, pathIndex = 0;
            for(int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    dataLengthFlag++;
                    lineSeperatorCheckAndPathCopy(ref dataLengthFlag, ref spaceSignCount, ref j, i, ref path, ref pathIndex, lines);
                    if (lineFaultsCheck(dataLengthFlag, spaceSignCount, ref faultLines, path, i, j, lines, ref pathIndex)) 
                        break;
                }
                dataLengthFlag = 0;
                spaceSignCount = 0;
                path = null;
                pathIndex = 0;
            }
            faultLines.Add(-1);
            return faultLines;
        }

        private static void lastCharCheck(ref string str)
        {
            if (str[str.Length - 1] == '\r' || str[str.Length - 1] == '\n')
                str = str.Remove(str.Length - 1);
        }

        public static Product[] ConvertToProducts(string fileData)
        {
            string[] lines = fileData.Split('\n');
            string[] productData = new string[lines.Count()];
            Product[] products = new Product[lines.Count()];
            for (int i = 0; i < products.Length; i++)
            {
                productData = lines[i].Split('*');
                lastCharCheck(ref productData[productData.Length - 1]);
                products[i] = new Product(productData[0], productData[1], productData[2], File.ReadAllBytes(productData[3]));
            }
            return products;
        }

        private static void faultLinesToStr(List<int> faultLines)
        {
            StringBuilder faults = new StringBuilder((faultLines.Count - 1) + 2 * (faultLines.Count - 2) + 1);
            char[] signs = { ',', ' ', '.' };
            int listIndex = 0;
            for (int i = 0; i < faultLines.Count - 1; i++) 
            {
                faults.Append(faultLines[i]);
                faults.Append(signs[0]);
                faults.Append(signs[1]);
            }
            faults.Remove(faults.Length - 2, 2);
            faults.Append(signs[2]);
            faults.Insert(0, "The following lines have wrong format\n");
            throw new Exception(faults.ToString());
        }

        public static Product[] fileCheck(string filePath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    Product[] products = null;
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    List<int> badLines = fileSyntexCheck(line);
                    if (badLines[0] != -1)
                        faultLinesToStr(badLines);
                    products = ConvertToProducts(line);
                    return products;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "FileEror", MessageBoxButtons.OK);
                return null;
            }
        }

/*        public static bool fileValidator(string filePath)
        {
            if (filePath.Length > 0)
            {
                if (File.Exists(filePath))
                    if(lastCharCheck(filePath))

            }
        }*/
    }
}
