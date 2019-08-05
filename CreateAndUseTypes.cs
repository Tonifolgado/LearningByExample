using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Card
    {

    }
    class CreateAndUseTypes
    {
        //If you want to access a specific card, you can go through the Cards property to access it
        public List<Card> Cards { get; set; }
        //public ICollection<Card> Cards { get; private set; }
        //An indexer property allows your class to be accessed with an index, just like a regular array
        private int _maximumNumberOfCards;

        public CreateAndUseTypes(int maximumNumberOfCards)
        {
            _maximumNumberOfCards = maximumNumberOfCards;
            Cards = new List<Card>();
        }

        // Rest of the class
        public void valueTypesAndAlias()
        {
            // create a variable to hold a value type using the alias form
            // but don't assign a variable
            int myInt;
            int myNewInt = new int();

            // create a variable to hold a .NET value type
            // this type is the .NET version of the alias form int
            // note the use of the keyword new, we are creating an object from 
            // the System.Int32 class
            System.Int32 myInt32 = new System.Int32();

            // you will need to comment out this first Console.WriteLine statement
            // as Visual Studio will generate an error about using an unassigned
            // variable.  This is to prevent using a value that was stored in the
            // memory location prior to the creation of this variable
            //Console.WriteLine(myInt);

            // print out the default value assigned to an int variable
            // that had no value assigned previously
            Console.WriteLine(myNewInt);

            // this statement will work fine and will print out the default value for
            // this type, which in this case is 0
            Console.WriteLine(myInt32);

            // assigning one value type to another
            int secondInt;
            // myInt will be assigned the value of 2
            myInt = 2;
            // secondInt will contain the value 2 after this statement executes
            secondInt = myInt;
            // output the value of the variables
            Console.WriteLine(myInt);
            Console.WriteLine(secondInt);
            Console.WriteLine();

        }

        public void usingValueTypes()
        {
            // declare some numeric data types
            int myInt;
            double myDouble;
            byte myByte;
            char myChar;
            decimal myDecimal;
            float myFloat;
            long myLong;
            short myShort;
            bool myBool;

            // assign values to these types and then
            // print them out to the console window
            // also use the sizeOf operator to determine
            // the number of bytes taken up be each type

            myInt = 5000;
            Console.WriteLine("Integer");
            Console.WriteLine(myInt);
            Console.WriteLine("Tipo: " + myInt.GetType());
            Console.WriteLine("Tamaño: " + sizeof(int));
            Console.WriteLine();

            myDouble = 5000.0;
            Console.WriteLine("Double");
            Console.WriteLine(myDouble);
            Console.WriteLine("Tipo: " + myDouble.GetType());
            Console.WriteLine("Tamaño: " + sizeof(double));
            Console.WriteLine();

            myByte = 254;
            Console.WriteLine("Byte");
            Console.WriteLine(myByte);
            Console.WriteLine("Tipo: " + myByte.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

            myChar = 'r';
            Console.WriteLine("Char");
            Console.WriteLine(myChar);
            Console.WriteLine("Tipo: " + myChar.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

            myDecimal = 20987.89756M;
            Console.WriteLine("Decimal");
            Console.WriteLine(myDecimal);
            Console.WriteLine("Tipo: " + myDecimal.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

            myFloat = 254.09F;
            Console.WriteLine("Float");
            Console.WriteLine(myFloat);
            Console.WriteLine("Tipo: " + myFloat.GetType());
            Console.WriteLine("Tamaño: "+ sizeof(byte));
            Console.WriteLine();

            myLong = 2544567538754;
            Console.WriteLine("Long");
            Console.WriteLine(myLong);
            Console.WriteLine("Tipo: " + myLong.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

            myShort = 3276;
            Console.WriteLine("Short");
            Console.WriteLine(myShort);
            Console.WriteLine("Tipo: " + myShort.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

            myBool = true;
            Console.WriteLine("Boolean");
            Console.WriteLine(myBool);
            Console.WriteLine("Tipo: " + myBool.GetType());
            Console.WriteLine("Tamaño: " + sizeof(byte));
            Console.WriteLine();

        }

    }    

    public class FieldvsProperty
    {
        // A field can offer direct access to the data it contains
        private string[] _myField;
        //The field that contains the real data is private and can be accessed 
        //only through the property (except when inside the class).
        public string MyProperty
        {
            get { return _myField[0]; }
            set { _myField[0] = value; }
        }
        //Using a property with the default get and set methods is so common that 
        //C# added a shorthand notation for it: public int Value { get; set; }

    }

    // *********************************
    public class Base
    {
        private int _privateField = 42;
        protected int _protectedField = 42;

        private void MyPrivateMethod() { }
        protected void MyProtectedMethod() { }
    }

    public class Derived : Base
    {
        public void MyDerivedMethod()
        {
            // _privateField = 41; // Not OK, this will generate a compile error
            _protectedField = 43; // OK, protected fields can be accessed
            // MyPrivateMethod(); // Not OK, this will generate a compile error
            MyProtectedMethod(); // OK, protected methods can be accessed
        }
    }

    class Person
    {
        //Example of a property
        private string firstName;


        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                firstName = value;
            }
        }
        public string LastName { get; set; }

            public Person(string firstName, string lastName)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        //a more detailed overriding of ToString method:
        public string ToString(string format = "FL")
            //the default value for the format is FL
        {
            //Trim quita los espacios en blanco
            //ToUpper convierte en mayúsculas
            format = format.Trim().ToUpperInvariant();

            switch (format)
            {
                case "FL":
                    return FirstName + " " + LastName;
                case "LF":
                    return LastName + " " + FirstName;
                case "FSL":
                    return FirstName + ", " + LastName;
                case "LSF":
                    return LastName + ", " + FirstName;
                default:
                    throw new FormatException(String.Format(
                            "The '{0}' format string is not supported.", format));
            }
        }





    }

    class People : IEnumerable<Person>
    {
        //Implementing IEnumerable
        public People(Person[] people)
        {
            this.people = people;
        }
        Person[] people;

        public IEnumerator<Person> GetEnumerator()
        {
            for (int index = 0; index < people.Length; index++)
            {
                yield return people[index];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    // ********************************

    class Conversion
    {
        static string value = "1";
        static string curvalue = "€19,95";
        static int result;
        private bool success = int.TryParse(value, out result);
        static CultureInfo english = new CultureInfo("En");
        static CultureInfo dutch = new CultureInfo("Nl");

        public void differentConversions()
        {
            if (success)
            {
                // value is a valid integer 
            }
            else
                {
                //value is a not valid integer
                }
        
        decimal numdec = decimal.Parse(curvalue, NumberStyles.Currency, dutch);
        Console.WriteLine(numdec.ToString(english));

         int i = Convert.ToInt32(null);
         Console.WriteLine(i); // null is converted to 0

         double d = 23.15;
         int j = Convert.ToInt32(d);
         Console.WriteLine(j); // the value is rounded to 23
        }

        public void parsingDateTime()
        {


        }


    }




 }
