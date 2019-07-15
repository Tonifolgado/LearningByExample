using System;
using System.IO;
using System.Text;
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
    }
}