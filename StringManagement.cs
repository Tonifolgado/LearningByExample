using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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

            //The value of xmlej is now:
            //<? xmlej version =\"1.0\" encoding=\"utf-16\"?>
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
            Console.WriteLine("p está en la posición: {0}", indexOfp);
            int lastIndexOfm = value.LastIndexOf('m'); // returns 5
            Console.WriteLine("La última m está en la posición: {0}", lastIndexOfm);
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

        #region XMLmanagement

        string xmlej = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                <people>
                  <person firstname=""john"" lastname=""doe"">
                    <contactdetails>
                      <emailaddress>john@unknown.com</emailaddress>
                    </contactdetails>
                  </person>
                  <person firstname=""jane"" lastname=""doe"">
                    <contactdetails>
                      <emailaddress>jane@unknown.com</emailaddress>
                      <phonenumber>001122334455</phonenumber>
                    </contactdetails>
                  </person>
                </people>";

        String xml2 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                    <people>
                      <person firstname=""john"" lastname=""doe"">
                        <contactdetails>
                          <emailaddress>john@unknown.com</emailaddress>
                        </contactdetails>
                      </person>
                      <person firstname=""jane"" lastname=""doe"">
                        <contactdetails>
                          <emailaddress>jane@unknown.com</emailaddress>
                          <phonenumber>001122334455</phonenumber>
                        </contactdetails>
                      </person>
                    </people>";

        public void usingXMLreader()
        {
            using (StringReader stringReader = new StringReader(xmlej))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader,
                    new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("people");

                    string firstName = xmlReader.GetAttribute("firstname");
                    string lastName = xmlReader.GetAttribute("lastname");

                    Console.WriteLine("Person: {0} {1}", firstName, lastName);
                    xmlReader.ReadStartElement("person");
                }
            }
        }

        public void usingXMLwriter()
        {
            StringWriter stream = new StringWriter();

            using (XmlWriter writer = XmlWriter.Create(
                stream,
                new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("People");
                writer.WriteStartElement("Person");
                writer.WriteAttributeString("firstName", "John");
                writer.WriteAttributeString("lastName", "Doe");
                writer.WriteStartElement("ContactDetails");
                writer.WriteElementString("EmailAddress", "john@unknown.com");
                //you need to make sure that you write both the start and the end tag of each element
                //You can choose to add attributes or to add elements that have a string value as their content.
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
            Console.WriteLine(stream.ToString());
        }

        public void usingXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlej);
            XmlNodeList nodes = doc.GetElementsByTagName("Person");
            // Output the names of the people in the document
            foreach (XmlNode node in nodes)
            {
                string firstName = node.Attributes["firstName"].Value;
                string lastName = node.Attributes["lastName"].Value;
                Console.WriteLine("Name: {0} {1}", firstName, lastName);
            }
            // Start creating a new node
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "Person", "");

            XmlAttribute firstNameAttribute = doc.CreateAttribute("firstName");
            firstNameAttribute.Value = "Foo";
            XmlAttribute lastNameAttribute = doc.CreateAttribute("lastName");
            lastNameAttribute.Value = "Bar";

            newNode.Attributes.Append(firstNameAttribute);
            newNode.Attributes.Append(lastNameAttribute);

            doc.DocumentElement.AppendChild(newNode);
            Console.WriteLine("Modified xmlej...");
            doc.Save(Console.Out);

            //Displays:
            //Name: john doe
            //Name: jane doe
            //Modified xmlej...
            //<?xmlej version="1.0" encoding="ibm850"?>
            //<people>
            //  <person firstname="john" lastname="doe">
            //   <contactdetails>
            //      <emailaddress>john@unknown.com</emailaddress>
            //    </contactdetails>
            //  </person>
            //  <person firstname="jane" lastname="doe">
            //    <contactdetails>
            //      <emailaddress>jane@unknown.com</emailaddress>
            //      <phonenumber>001122334455</phonenumber>
            //    </contactdetails>
            //  </person>
            //  <person firstname="Foo" lastname="Bar" />
            //</people>
        }

        public void xPathQuery()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlej);
            //XPath is a kind of query language for XML documents
            XPathNavigator nav = doc.CreateNavigator();
            string query = "//people/person[@firstName='jane']";
            XPathNodeIterator iterator = nav.Select(query);

            Console.WriteLine(@"Items found: {0}", iterator.Count); // Displays 1

            while (iterator.MoveNext())
            {
                string firstName = iterator.Current.GetAttribute("firstName", "");
                string lastName = iterator.Current.GetAttribute("lastName", "");
                Console.WriteLine("Name: {0} {1}", firstName, lastName);
            }
        }

        public void usingLINQtoXML()
        {
            XDocument doc = XDocument.Parse(xml2);
            //Attribute method returns instances of XAttribute
            //The XAttribute has a Value property of type string, but it also implements explicit operators
            //so you can cast it to most of the basic types in C#.
            IEnumerable<string> personNames = from p in doc.Descendants("person")
                                              select (string)p.Attribute("firstname")
                                                 + " " + (string)p.Attribute("lastname");
            foreach (string s in personNames)
            {
                Console.WriteLine(s);
            }

            // Displays:
            // John Doe
            // Jane Doe

            //Using Where and OrderBy in a LINQ to XML query
            IEnumerable<string> personNames2 = from p in doc.Descendants("person")
                                              where p.Descendants("phonenumber").Any()
                                              let name = (string)p.Attribute("firstname")
                                                          + " " + (string)p.Attribute("lastname")
                                              orderby name
                                              select name;

            foreach (string s in personNames2)
            {
                Console.WriteLine(s);
            }
        }

        public void updatingXML()
        {
            XElement root = XElement.Parse(xml2);
            //Functional construction treats modifying data as a problem of transformation
            //rather than as a detailed manipulation of data
            foreach (XElement p in root.Descendants("person"))
            {
                string name = (string)p.Attribute("firstname") + (string)p.Attribute("lastname");
                p.Add(new XAttribute("IsMale", name.Contains("john")));
                XElement contactDetails = p.Element("contactdetails");
                if (!contactDetails.Descendants("phonenumber").Any())
                {
                    contactDetails.Add(new XElement("phonenumber", "001122334455"));
                }
            }

            Console.WriteLine(xml2);
        }

        public void functionalCreationToUpdateXML()
        {
            XElement root = XElement.Parse(xml2);

            XElement newTree = new XElement("people",
                from p in root.Descendants("person")
                let name = (string)p.Attribute("firstname") + (string)p.Attribute("lastname")
                let contactDetails = p.Element("contactdetails")
                select new XElement("person",
                    new XAttribute("IsMale", name.Contains("john")),
                    p.Attributes(),
                    new XElement("contactdetails",
                        contactDetails.Element("emailaddress"),
                        contactDetails.Element("phonenumber")
                            ?? new XElement("phonenumber", "66778899")
                    )));

            Console.WriteLine(xml2);
        }

        public void usingXElement()
        {
            //use the class XElement for creating your own XML
            //use the Add method to construct an XML hierarchy
            XElement root = new XElement("Root", new List<XElement>
            {
                new XElement("Child1"),
                new XElement("Child2"),
                new XElement("Child3")
            },
            new XAttribute("MyAttribute", 42));
            root.Save("test.xml");

            //Outputs:
            //<Root MyAttribute="42">
            //    <Child1 />
            //    <Child2 />
            //    <Child3 />
            //</Root>

            Console.WriteLine(root);
        }

        #endregion

    }
}