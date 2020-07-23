using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static bool lastCharCheck(string fileName)
        {
            if (fileName.Substring(fileName.Length - 3) == "txt")
                return true;
            return false;
        }

        private static List<int> fileSyntexCheck(string fileString)
        {
            string[] lines = fileString.Split('\r');
            char[] path = new char[250];
            List<int> faultLines = new List<int>();
            int dataLengthFlag = 0, spaceSignCount = 0, pathIndex = 0;
            for(int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    dataLengthFlag++;
                    if ((dataLengthFlag > 14 || spaceSignCount > 3) && spaceSignCount != 3) 
                    {
                        faultLines.Add(i);
                        break;
                    }
                    if (lines[i][j] == '_')
                    {
                        spaceSignCount++;
                        dataLengthFlag = 0;
                        j++;
                    }
                    if (spaceSignCount == 1)
                    {
                        if (lines[i][j] < '0' || lines[i][j] > '9')
                        {
                            faultLines.Add(i);
                            break;
                        }
                    }
                    if (spaceSignCount == 3)
                    {
                        path[pathIndex++] = lines[i][j];
                    }
                    if (j + 1 == lines[i].Length) 
                    {
                        if (spaceSignCount != 3 || !File.Exists(new string(path))) 
                        {
                            faultLines.Add(i);
                            break;
                        }
                        pathIndex = 0;
                    }
                }
                dataLengthFlag = 0;
                spaceSignCount = 0;
            }
            faultLines.Add(-1);
            return faultLines;
        }

        public static bool fileCheck(string filePath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    List<int> badLines = fileSyntexCheck(line);
                    if (badLines[0] == -1)
                        return true;
                }
                return false;
            }
            catch (IOException e)
            {
                return false;
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
