using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LearningByExample1
{
    #region SerializableClasses

    [Serializable]
    public class Persona
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    [Serializable]
    public class Persono
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NonSerialized]
        private bool isDirty = false;
    }

    [Serializable]
    public class Person2
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NonSerialized]
        private bool isDirty = false;

        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerializing.");
        }

        [OnSerialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerialized.");
        }

        [OnDeserializing()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnDeserializing.");
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerialized.");
        }
    }

    [Serializable]
    public class Order
    {
        [XmlAttribute]
        public int ID { get; set; }

        [XmlIgnore]
        public bool IsDirty { get; set; }

        [XmlArray("Lines")]
        [XmlArrayItem("OrderLine")]
        public List<OrderLine> OrderLines { get; set; }
    }

    [Serializable]
    public class VIPOrder : Order
    {
        public string Description { get; set; }
    }

    [Serializable]
    public class OrderLine
    {
        [XmlAttribute]
        public int ID { get; set; }

        [XmlAttribute]
        public int Amount { get; set; }

        [XmlElement("OrderedProduct")]
        public Product Product { get; set; }
    }

    [Serializable]
    public class Product
    {
        [XmlAttribute]
        public int ID { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    [Serializable]
    public class PersonComplex : ISerializable
    {
        //If you have a sensitive class, you should implement the ISerializable interface
        //to have control over which values are serialized
        //You could choose to not serialize sensitive data 
        //or possibly encrypt prior to serialization
        public int Id { get; set; }
        public string Name { get; set; }
        private bool isDirty = false;

        public PersonComplex() { }

        //The other important step is adding a special protected constructor 
        //that takes a SerializationInfo and StreamingContext
        //This constructor is called during deserialization
        //and you use it to retrieve the values and initialize your object.
        protected PersonComplex(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("Value1");
            Name = info.GetString("Value2");
            isDirty = info.GetBoolean("Value3");
        }

        [System.Security.Permissions.SecurityPermission(SecurityAction.Demand,
                                                        SerializationFormatter = true)]
        //GetObjectData method is called when your object is serialized
        //It should add the values that you want to serialize as key/value pairs
        //to the SerializationInfo object that’s passed to the method
        //you should mark this method with a SecurityPermission attribute 
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value1", Id);
            info.AddValue("Value2", Name);
            info.AddValue("Value3", isDirty);
        }
    }

    //You have now seen XML and binary serialization
    //Another type of serialization is used when you use WCF
    //types used in WCF are serialized so they can be sent to other applications
    //The Data Contract Serializer is used by WCF to serialize your objects to XML or JSON
    [DataContract]
    public class PersonDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        private bool isDirty = false;
    }

    [DataContract]
    public class Person3
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }


    #endregion


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
            //XmlSerializer outputs human-readable text. You can open it in Notepad
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
        private static Order CreateOrder()
    {
        Product p1 = new Product { ID = 1, Description = "p2", Price = 9 };
        Product p2 = new Product { ID = 2, Description = "p3", Price = 6 };

        Order order = new VIPOrder
        {
            ID = 4,
            Description = "Order for John Doe. Use the nice giftwrap",
            OrderLines = new List<OrderLine>
        {
            new OrderLine { ID = 5, Amount = 1, Product = p1},
            new OrderLine { ID = 6 ,Amount = 10, Product = p2},
        }
        };

        return order;
    }
        public void serializeDerivedClass()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Order), new Type[] { typeof(VIPOrder) });
            string xml3;
            using (StringWriter stringWriter = new StringWriter())
            {
                Order order = CreateOrder();
                serializer.Serialize(stringWriter, order);
                xml3 = stringWriter.ToString();
            Console.WriteLine(xml3);
            }
            Console.WriteLine("");
            using (StringReader stringReader = new StringReader(xml3))
            {
                Order o = (Order)serializer.Deserialize(stringReader);
                // Use the order
            Console.WriteLine("Serialize result: {0}", o.ToString());
            }
        }

        public void binarySerialization()
        {
            Persono p = new Persono
            {
                Id = 1,
                Name = "John Doe"
            };

            IFormatter formatter = new BinaryFormatter();
            //Binary serialization creates a compact stream of bytes. Private fields are serialized by default.
            using (Stream stream = new FileStream("data.bin", FileMode.Create))
            {
                formatter.Serialize(stream, p);
            }

            using (Stream stream = new FileStream("data.bin", FileMode.Open))
            {
                Persono dp = (Persono)formatter.Deserialize(stream);
                Console.WriteLine(dp.ToString());
            }
        }

        public void usingDataContractSerializer()
        {
            PersonDataContract p = new PersonDataContract
            {
                Id = 1,
                Name = "John Doe"
            };
            //You need to specify a Stream object that has the input or output
            //when serializing or deserializing an object
            using (Stream stream = new FileStream("data.xml", FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(PersonDataContract));
                ser.WriteObject(stream, p);
                Console.WriteLine(stream);
            }

            using (Stream stream = new FileStream("data.xml", FileMode.Open))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(PersonDataContract));
                PersonDataContract result = (PersonDataContract)ser.ReadObject(stream);
                Console.WriteLine(result.ToString());
            }

        }

        public void usingDataContractJsonSerializer()
        {
            Person3 p = new Person3
            {
                Id = 1,
                Name = "John Doe"
            };

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person3));
                ser.WriteObject(stream, p);

                stream.Position = 0;
                StreamReader streamReader = new StreamReader(stream);
                Console.WriteLine(streamReader.ReadToEnd()); // Displays {"Id":1,"Name":"John Doe"}

                stream.Position = 0;
                Person3 result = (Person3)ser.ReadObject(stream);
            }

        }

        // Serialize an ArrayList object to a binary file.
        //Use a formatter to serialize an object and write it to a System.IO.FileStream object
        //When you need to retrieve the object, use the same formatter 
        //to read the serialized data from the file and deserialize 
        //Both the BinaryFormatter and SoapFormatter classes implement 
        //the interface System.Runtime.Serialization.IFormatter, which defines
        //two methods: Serialize andDeserialize
        private static void BinarySerialize(ArrayList list)
        {
            using (FileStream str = File.Create("people.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, list);
                //The Serialize method takes a System.IO.Stream reference
                //and a System.Object reference as arguments, 
                //serializes the Object, and writes it to the Stream
            }
        }
        // Deserialize an ArrayList object from a binary file.
        private static ArrayList BinaryDeserialize()
        {
            ArrayList people = null;

            using (FileStream str = File.OpenRead("people.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                people = (ArrayList)bf.Deserialize(str);
            }
            return people;
            //The Deserialize method takes a Stream reference as an argument
            //reads the serialized object data from the Stream
            //and returns an Object reference to a deserialized object
            //You must cast the returned Object reference to the correct type.
        }

        // Serialize an ArrayList object to a SOAP file.
        //The SoapFormatter class produces a SOAP document
        private static void SoapSerialize(ArrayList list)
        {
            using (FileStream str = File.Create("people.soap"))
            {
                SoapFormatter sf = new SoapFormatter();
                sf.Serialize(str, list);
            }
        }
        // Deserialize an ArrayList object from a SOAP file.
        private static ArrayList SoapDeserialize()
        {
            ArrayList people = null;

            using (FileStream str = File.OpenRead("people.soap"))
            {
                SoapFormatter sf = new SoapFormatter();
                people = (ArrayList)sf.Deserialize(str);
            }
            return people;
        }

        public void serializeDeserializeExample()
        {
            // Create and configure the ArrayList to serialize.
            ArrayList people = new ArrayList();
            people.Add("Graeme");
            people.Add("Lin");
            people.Add("Andy");
            // Serialize the list to a file in both binary and SOAP form.
            BinarySerialize(people);
            SoapSerialize(people);
            // Rebuild the lists of people from the binary and SOAP
            // serializations and display them to the console.
            ArrayList binaryPeople = BinaryDeserialize();
            ArrayList soapPeople = SoapDeserialize();
            Console.WriteLine("Binary people:");
            foreach (string s in binaryPeople)
            {
                Console.WriteLine("\t" + s);
            }
            Console.WriteLine("\nSOAP people:");
            foreach (string s in soapPeople)
            {
                Console.WriteLine("\t" + s);
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
        }

        public void JSONserialization()
        {
            //1. Create a Stream that either writes to the destination you wish to serialize to 
            //or is the source of the data you wish to deserialize from
            //2. Create an instance of DataContractJsonSerializer, 
            //using the type of the object that you wish to serialize or deserialize
            //as the constructor argument
            //3. Call WriteObject (to serialize) or ReadObject (to deserialize) 
            //using the object you wish to process as a method argument

            // Create a list of strings.
            List<string> myList = new List<string>()
            {
                "apple", "orange", "banana", "cherry"
            };
            // Create memory stream - we will use this
            // to get the JSON serialization as a string.
            MemoryStream memoryStream = new MemoryStream();
            // Create the JSON serializer.
            DataContractJsonSerializer jsonSerializer
                = new DataContractJsonSerializer(myList.GetType());
            // Serialize the list.
            jsonSerializer.WriteObject(memoryStream, myList);
            // Get the JSON string from the memory stream.
            string jsonString = Encoding.Default.GetString(memoryStream.ToArray());
            // Write the string to the console.
            Console.WriteLine(jsonString);

            // Create a new stream so we can read the JSON data.
            memoryStream = new MemoryStream(Encoding.Default.GetBytes(jsonString));
            // Deserialize the list.
            myList = jsonSerializer.ReadObject(memoryStream) as List<string>;
            // Enumerate the strings in the list.
            foreach (string strValue in myList)
            {
                Console.WriteLine(strValue);
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
        }

        public static void ListAssemblies()
        {
            // Get an array of the assemblies loaded into the current
            // application domain.
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                Console.WriteLine(a.GetName());
            }
        }

        public void loadAssemblyIntoCurrentAppDomain()
        {
            // List the assemblies in the current application domain.
            Console.WriteLine("**** BEFORE ****");
            ListAssemblies();
            // Load the System.Data assembly using a fully qualified display name.
            string name1 = "System.Data,Version=2.0.0.0," +
                "Culture=neutral,PublicKeyToken=b77a5c561934e089";
            Assembly a1 = Assembly.Load(name1);
            // Load the System.Xml assembly using an AssemblyName.
            AssemblyName name2 = new AssemblyName();
            name2.Name = "System.Xml";
            name2.Version = new Version(2, 0, 0, 0);
            name2.CultureInfo = new CultureInfo("");    //Neutral culture.
            name2.SetPublicKeyToken(
                new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 });
            Assembly a2 = Assembly.Load(name2);
            // Load the SomeAssembly assembly using a partial display name.
            Assembly a3 = Assembly.Load("SomeAssembly");
            // Load the assembly named c:\shared\MySharedAssembly.dll.
            Assembly a4 = Assembly.LoadFrom(@"c:\shared\MySharedAssembly.dll");
            // List the assemblies in the current application domain.
            Console.WriteLine("\n\n**** AFTER ****");
            ListAssemblies();
            Console.WriteLine("\nMain method complete. Press Enter.");
        }

    }
}
