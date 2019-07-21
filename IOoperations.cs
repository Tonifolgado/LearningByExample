using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class IOoperations
    {
        #region Reading

        void readFileIntoString(string filepath)
        {
            //Read an entire file into a string
            // filepath = "C:\t1"
            string contents = File.ReadAllText(@filepath);
            Console.Out.WriteLine("Contents : " + contents);
        }

        void readFileIntoArray(string filepath)
        {
            //Read all the lines from a file into an array
            // filepath = "C:\t1"
            string[] lines = File.ReadAllLines(@filepath);
            Console.Out.WriteLine("Contents : " + lines.Length);
            Console.In.ReadLine();
        }

        void readFileLineByLine(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine("XML template : " + line);
            }
            if (sr != null) sr.Close(); // should be in a finally or using block
        }

        #endregion

    }
}
