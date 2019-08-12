using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class SetsAndArrays
    {
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

            //public Person(string firstName, string lastName)
            //{
            //    this.FirstName = firstName;
            //    this.LastName = lastName;
            //}

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

        class Employee: Person
        {
            private string role;
            public string Role { get; set; }
        }

        class Manager: Employee
        {

        }

        public void manageArrays()
        {
            // Declare and initialize an array of Employees.
            Employee[] employees = new Employee[10];
            for (int id = 0; id < employees.Length; id++)
                employees[id] = new Employee();
            // Implicit cast to an array of Persons.
            // (An Employee is a type of Person.)
            Person[] persons = employees;
            // Explicit cast back to an array of Employees.
            // (The Persons in the array happen to be Employees.)
            employees = (Employee[])persons;
            // Use the is operator.
            if (persons is Employee[])
            {
                // Treat them as Employees.
                //...
            }
            // Use the as operator.
            employees = persons as Employee[];
            // After this as statement, managers is null.
            Manager[] managers = persons as Manager[];
            // Use the is operator again, this time to see
            // if persons is compatible with Manager[].
            if (persons is Manager[])
            {
                // Treat them as Managers.
                //...
            }
            // This cast fails at run time because the array
            // holds Employees not Managers.
            managers = (Manager[])persons;
        }



        class Set<T>
        {
            //A set stores only unique items, so it sees whether an item already exists before adding it
            private List<T>[] buckets = new List<T>[100];
            //You split the data in a set of buckets.
            //Each bucket contains a subgroup of all the items in the set
            //Now your items are distributed over a hundred buckets instead of one single bucket
            //When you see whether an item exists, you first calculate the hash code, go to the corresponding bucket, and look for the item.

            public void Insert(T item)
            {
                //Hashing is the process of taking a large set of data and mapping it
                //to a smaller data set of fixed length.
                int bucket = GetBucket(item.GetHashCode());
                //GetHashCode is defined on the base class Object
                //you can override this method and provide a specific implementation
                if (Contains(item, bucket))
                    return;
                if (buckets[bucket] == null)
                    buckets[bucket] = new List<T>();
                buckets[bucket].Add(item);
            }
            public bool Contains(T item)
            {
                return Contains(item, GetBucket(item.GetHashCode()));
            }

            private int GetBucket(int hashcode)
            {
                // A Hash code can be negative. To make sure that you end up with a positive
                // value cast the value to an unsigned int. The unchecked block makes sure that
                // you can cast a value larger then int to an int safely.
                unchecked
                {
                    return (int)((uint)hashcode % (uint)buckets.Length);
                }
            }

            private bool Contains(T item, int bucket)
            {
                if (buckets[bucket] != null)
                    foreach (T member in buckets[bucket])
                        if (member.Equals(item))
                            return true;
                return false;
            }
        }



    }
}
