using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LearningByExample1
{
    [Serializable]
    public class Persona
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    class Serialization
    {
        public void serializeWithXMLserializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Persona));
            string xml;
            //Serialization is the process of transforming an object in-memory 
            //into a stream of bytes or text. 
            using (StringWriter stringWriter = new StringWriter())
            {
                Persona p = new Persona
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Age = 42
                };
                serializer.Serialize(stringWriter, p);
                xml = stringWriter.ToString();
            }
            Console.WriteLine(xml);

            using (StringReader stringReader = new StringReader(xml))
            {
                Persona p = (Persona)serializer.Deserialize(stringReader);
                Console.WriteLine("{0} {1} is {2} years old", p.FirstName, p.LastName, p.Age);
            }

            // Displays
            //<?xml version="1.0" encoding="utf-16"?>
            //<Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            // xmlns:xsd="http://www.w3.org/2001/XMLSchema">
            //  <FirstName>John</FirstName>
            //  <LastName>Doe</LastName>
            //  <Age>42</Age>
            //</Person>
            //John Doe is 42 years old
        }

    }
}
