﻿using System;
using System.Collections;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return FirstName + " " + LastName;
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





}