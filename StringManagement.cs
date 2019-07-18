﻿using System;
using System.Globalization;
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

        #region Regex
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

        public bool ValidateZipCode(string zipCode)
        {
            // Valid zipcodes: 1234AB | 1234 AB | 1001 AB

            if (zipCode.Length < 6) return false;

            string numberPart = zipCode.Substring(0, 4);
            int number;
            if (!int.TryParse(numberPart, out number)) return false;

            string characterPart = zipCode.Substring(4);

            if (numberPart.StartsWith("0"))
            {
                Console.WriteLine("ZIP code not valid");
                Console.WriteLine("Valid zipcodes: 1234AB | 1234 AB | 1001 AB");
                return false;
            }
            if (characterPart.Trim().Length < 2)
            {
                Console.WriteLine("ZIP code not valid");
                Console.WriteLine("Valid zipcodes: 1234AB | 1234 AB | 1001 AB");
                return false;
            }
            if (characterPart.Length == 3 && characterPart.Trim().Length != 2)
            {
                Console.WriteLine("ZIP code not valid");
                Console.WriteLine("Valid zipcodes: 1234AB | 1234 AB | 1001 AB");
                return false;
            }

            Console.WriteLine("ZIP code OK");
            return true;

        }

        public bool ValidateZipCodeRegEx(string zipCode)
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$",
                RegexOptions.IgnoreCase);
            return match.Success;
        }

        public void RegexWithMultipleSpaces()
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);

            string input = "1 2 3 4   5";
            string result = regex.Replace(input, " ");

            Console.WriteLine(result); // Displays 1 2 3 4 5
        }

        #endregion


        public void formattingStrings()
        {
            Console.WriteLine("Format a number as a currency: ");
            double cost = 1234.56;
            Console.WriteLine(cost.ToString("C",
                              new System.Globalization.CultureInfo("en-US")));
            // Displays $1,234.56
            Console.WriteLine("Displaying a DateTime with different format strings: ");
            DateTime d = new DateTime(2013, 4, 22);
            CultureInfo provider = new CultureInfo("en-US");
            Console.WriteLine("Format d: " + d.ToString("d", provider)); // Displays 4/22/2013
            Console.WriteLine("Format D: " + d.ToString("D", provider)); // Displays Monday, April 22, 2013
            Console.WriteLine("Format M: " + d.ToString("M", provider)); // Displays April 22

        }
    }
}