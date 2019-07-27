using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    }
}
