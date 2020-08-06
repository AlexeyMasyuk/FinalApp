using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Alexey Masyuk and Yulia Berkovich
 * Static class to use without decleration.
 * In use from Form3,
 *  Methods to validate all data.

 * File Format and Rules.
 * ---------------- FORMAT ----------------
 * Name*Price*Category*Product Image Path -
 * ----------------------------------------
 * Max 14 chars for one data word (Name, Price, Category),
 * Price must be a Positive Natural Number,
 * Exactly 3 word seperating characters '*',
 * Max image size 420x420
*/

namespace FinalApp
{
    public static class ProductValidator
    {
        /*
         * Outside class called method, Used in case 'Add Product' pressed.
         * check if all fields are filled,
         * image exists and image size is valid (420x420 checked from Product class).
         * return true if all data are filled and valid, 
         * else throws relevant message and return false.
        */
        public static bool boxAddValidation(string name,string category,string price,string imagePath)
        {
            int tmpPrice;
            if (name.Length > 0 && category.Length > 0 && price.Length > 0 && imagePath.Length > 0)
            {
                try
                {
                    if (Int32.TryParse(price, out tmpPrice)) 
                        if (tmpPrice > 0) 
                            if (File.Exists(imagePath))
                            {
                                Product product = new Product(name, price, category, File.ReadAllBytes(imagePath));
                                return true;
                            }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString(), "FileEror", MessageBoxButtons.OK);
                    return false;
                }
                        
            }
            return false;
        }

        /*
         * Within class method, used in 'fileSyntexCheck(string fileString) below. 
         * Checking if data word excced 14 characters or more than 3 seperating characters found,
         * In addition if all image path are copied check if file exists in path.
          * if one condition failed add line to foults list.
        */
        private static bool lineFaultsCheck(int dataLengthFlag, int spaceSignCount,ref List<int> faultLines, char[] path, int i, int j, string[] lines, ref int pathIndex)
        {
            if ((dataLengthFlag > 14 || spaceSignCount > 3) && spaceSignCount != 3)
            {                                               // Condition applied only if data word isn't image path
                faultLines.Add(i);
                return true;
            }
            if (j + 1 == lines[i].Length) // All path are copied
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

        /*
         * Within class method, used in 'fileSyntexCheck(string fileString)' below, 
         * checking if Seperating sign '*' appeared counting it,
         * copyng image path if 3rd space appeared.
        */
        private static void lineSeperatorCheckAndPathCopy(ref int dataLengthFlag, ref int spaceSignCount, ref int j,int i,ref char[] path, ref int pathIndex, string[] lines)
        {
            if (lines[i][j] == '*') // Seperating sign search and count
            {
                spaceSignCount++;
                dataLengthFlag = 0; // Reset chars counter (up to 14 char in every data word)
                j++;
            }
            if (spaceSignCount == 3) // Image Path copy
            {
                if (path == null)
                    path = new char[lines[i].Length - j];
                path[pathIndex++] = lines[i][j];
            }
        }

        /*
         * Within class method, used in 'fileSyntexCheck(string fileString)' below, 
         * Reset all flags and path string when line ended/checked.
        */
        private static void flagsAndPathReset(ref int dataLengthFlag, ref int spaceSignCount, ref char[] path, ref int pathIndex)
        {
            dataLengthFlag = 0;
            spaceSignCount = 0;
            path = null;
            pathIndex = 0;
        }

        /*
         * Within class method, used in 'ConvertToProducts(string fileData)' below, 
         * Main syntex/format checking method.
         * getting all readed characters from text file (fileString),
         * splliting it to lines and checking all contition met.
         * Return list of fault lines
        */
        private static List<int> fileSyntexCheck(string fileString)
        {
            string[] lines = fileString.Split('\n');
            char[] path = null;
            List<int> faultLines = new List<int>();                    // List of all bad formmated lines.
            int dataLengthFlag = 0, spaceSignCount = 0, pathIndex = 0; // Flags.
            for(int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    dataLengthFlag++;
                    lineSeperatorCheckAndPathCopy(ref dataLengthFlag, ref spaceSignCount, ref j, i, ref path, ref pathIndex, lines);
                    if (lineFaultsCheck(dataLengthFlag, spaceSignCount, ref faultLines, path, i, j, lines, ref pathIndex)) 
                        break;
                }
                flagsAndPathReset(ref dataLengthFlag, ref spaceSignCount, ref path, ref pathIndex);
            }
            faultLines.Add(-1); // if all lines correct -1 the only number that will be in list
            return faultLines;
        }

        /*
         * Within class method, used in 'ConvertToProducts(string fileData)' below,
         * Checking if line are empty and deleating '\r', '\n' if appeared.
         * Throws Exeption if line empty.
        */
        private static void lastCharCheck(ref string str)
        {
            if (str.Length > 0)
            {
                if (str[str.Length - 1] == '\r' || str[str.Length - 1] == '\n')
                    str = str.Remove(str.Length - 1);
            }
            else
                throw new Exception("Empty Line");
        }

        /*
         * Within class method, used in 'ConvertToProducts(string fileData)' below,
         * used after syntax check.
         * Method convert readed data from file to array of products'
         * Exeption thrown if image size is not correct (biger then 420x420)         
        */
        private static Product[] ConvertToProducts(string fileData)
        {
            string[] lines = fileData.Split('\n');
            string[] productData = new string[lines.Count()];
            Product[] products = new Product[lines.Count()];
            for (int i = 0; i < products.Length; i++)
            {
                productData = lines[i].Split('*');
                lastCharCheck(ref productData[productData.Length - 1]);
                try
                {
                    products[i] = new Product(productData[0], productData[1], productData[2], File.ReadAllBytes(productData[3]));
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message + " on line " + i.ToString());
                }
            }
            return products;
        }

        /*
         * Within class method, used in 'ConvertToProducts(string fileData)' below,
         * Method convert list containing all wrong formated lines to string'
         * building Exeption message and throwing it.
        */
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

        /*
         * Outside class called method, Used in case 'Add From File' pressed.
         * Reading all text file, checking file format/syntax if syntax okey 
         * converting all readed data to products array.
         * If bad syntax lines founded 'faultLinesToStr(List<int> faultLines)' method 
         * will throw exeption and relevant message will displayed.
        */
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
                    if (badLines[0] != -1)          // bad syntax lines founded if -1 is not on index 0
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

    }
}
