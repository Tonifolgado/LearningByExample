using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace LearningByExample1
{
    public class StringManagement
    {
        public void createLotOfStrings()
        {
            //This code will run 100 times, and each time it will create a new string
            //The reference s will point only to the last item
            //all other strings are immediately ready for garbage collection
            string s = string.Empty;

            for (int i = 0; i < 100; i++)
            {
                s += "x";
                Console.WriteLine(s);
            }
            Console.WriteLine(s);
        }

        public void usingStringBuilder()
        {
            StringBuilder sb2 = new StringBuilder(string.Empty);

            for (int i = 0; i < 100; i++)
            {
                sb2.Append("*");
            }
            Console.WriteLine(sb2);

            StringBuilder sb = new StringBuilder("A : is the initial value");
            //change the character on the first position
            sb[0] = 'B';
            Console.WriteLine(sb);
        }

        public void StringWriterAsOutputForXMLwriter()
        {
            var stringWriter = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(stringWriter))
            {
                writer.WriteStartElement("book");
                writer.WriteElementString("price", "19.95");
                writer.WriteEndElement();
                writer.Flush();
            }
            string xml = stringWriter.ToString();

            //The value of xml is now:
            //<? xml version =\"1.0\" encoding=\"utf-16\"?>
            //         < book >
            //           < price > 19.95 </ price >
            //         </ book >
            Console.WriteLine(xml);
        }

        public void usingStrings()
        {
            string value = "My Sample Value";
            Console.WriteLine("El string utilizado es: {0}", value);
            int indexOfp = value.IndexOf('p'); // returns 6
            Console.WriteLine("p está en la posición: {0}",indexOfp);
            int lastIndexOfm = value.LastIndexOf('m'); // returns 5
            Console.WriteLine("La última m está en la posición: {0}",lastIndexOfm);
            if (value.StartsWith("M"))
            {
                Console.WriteLine("Empieza por M");
            }
            string subString = value.Substring(3, 6); // Returns 'Sample'
            Console.WriteLine("Con Substring se extrae: {0}", subString);

            Console.WriteLine(" ");
            //iterating over a string by character
            foreach (char c in value)
                Console.WriteLine(c);

            Console.WriteLine(" ");
            //iterating over a string by words (substrings separated by spaces)
            foreach (string word in "My sentence separated by spaces".Split(' '))
            {
                Console.WriteLine(word);
            }

        }

        public void ChangeStringWithRegex()
        {
            //Changing a string with a regular expression
            string pattern = "(Mr\\.? |Mrs\\.? |Miss |Ms\\.? )";
            string[] names = { "Mr. Henry Hunt", "Ms. Sara Samuels",
             "Abraham Adams", "Ms. Nicole Norris" };

            //extract the patterns and puts blank
            foreach (string name in names)
                Console.WriteLine(Regex.Replace(name, pattern, String.Empty));
        }
    }
}