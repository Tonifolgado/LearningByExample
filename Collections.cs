using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    public class Person4
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private bool isDirty = false;
    }

    class Fruit
    {
        public Fruit(string nameVal, string colorVal)
        {
            Name = nameVal;
            Color = colorVal;
        }
        public string Name { get; set; }
        public string Color { get; set; }
    }

    class FruitComparer : IEqualityComparer<Fruit>
    {
        public bool Equals(Fruit first, Fruit second)
        {
            return first.Name == second.Name && first.Color == second.Color;
        }
        public int GetHashCode(Fruit fruit)
        {
            return fruit.Name.GetHashCode() + fruit.Name.GetHashCode();
        }
    }


    class Collections
    {
        public void Arrays()
        {
            //the array is created with a fixed size
            //Arrays are zero-based, the first element can be found at index 0
            //and the last element at the length of the array–1
            int[] arrayOfInt = new int[10];

            for (int x = 0; x < arrayOfInt.Length; x++)
            {
                arrayOfInt[x] = x;
            }
            //An array can also be initialized directly:
            int[] arrayOfInt2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //An array implements IEnumerable, so you can use it in a foreach loop
            foreach (int i in arrayOfInt)
            {
                Console.Write(i); // Displays 0123456789
            }

        }

        public void twodimensionalArray()
        {
            string[,] array2D = new string[3, 2] { { "one", "two" }, 
                { "three", "four" },
                { "five", "six" } };

            Console.WriteLine(array2D[0, 0]); // one
            Console.WriteLine(array2D[0, 1]); // two
            Console.WriteLine(array2D[1, 0]); // three
            Console.WriteLine(array2D[1, 1]); // four
            Console.WriteLine(array2D[2, 0]); // five
            Console.WriteLine(array2D[2, 1]); // six
        }

        public void listOfStrings()
        {
            //A List<T> just stores a group of items.
            //It enables duplicates and it quickly finds items
            List<string> listOfStrings = new List<string> { "A", "B", "C", "D", "E" };

            for (int x = 0; x < listOfStrings.Count; x++)
                Console.Write(listOfStrings[x]); // Displays: ABCDE

            listOfStrings.Remove("A");
            Console.WriteLine(listOfStrings[0]); // Displays: B

            listOfStrings.Add("F");
            Console.WriteLine(listOfStrings.Count); // Displays: 5

            bool hasC = listOfStrings.Contains("C");
            Console.WriteLine(hasC); // Displays: true
        }

        public void Dictionaries()
        {
            //A Dictionary can be used in scenarios in which you want to store items
            //and retrieve them by key, so it doesn’t allow duplicate keys
            //The Dictionary class is implemented as a hash table, which makes retrieving a value very fast
            //A key shouldn’t change during time and it can’t be null
            //The value can be null (if it’s a reference type).
            Person4 p1 = new Person4 { Id = 1, Name = "Name1" };
            Person4 p2 = new Person4 { Id = 2, Name = "Name2" };
            Person4 p3 = new Person4 { Id = 3, Name = "Name3" };

            var dict = new Dictionary<int, Person4>();
            dict.Add(p1.Id, p1);
            dict.Add(p2.Id, p2);
            dict.Add(p3.Id, p3);

            foreach (KeyValuePair<int, Person4> v in dict)
            {
                Console.WriteLine("{0}: {1}", v.Key, v.Value.Name);
            }
            Console.WriteLine("");

            dict[0] = new Person4 { Id = 4, Name = "Name4" };

            foreach (KeyValuePair<int, Person4> v in dict)
            {
                Console.WriteLine("{0}: {1}", v.Key, v.Value.Name);
            }

            Person4 result;            
            if (!dict.TryGetValue(5, out result))
            {
                Console.WriteLine("No person with a key of 5 can be found");
            }

        }

        public void UseHashSet()
        {
            //A set is a collection that contains no duplicate elements
            //and has no particular order
            //HashSet implements the ISet<T> interface 
            HashSet<int> oddSet = new HashSet<int>();
            HashSet<int> evenSet = new HashSet<int>();

            for (int x = 1; x <= 10; x++)
            {
                if (x % 2 == 0)
                    evenSet.Add(x);
                else
                    oddSet.Add(x);
            }

            DisplaySet(oddSet);
            DisplaySet(evenSet);

            oddSet.UnionWith(evenSet);
            DisplaySet(oddSet);
        }

        private void DisplaySet(HashSet<int> set)
        {
            Console.Write("{");
            foreach (int i in set)
            {
                Console.Write(" {0}", i);
            }
            Console.WriteLine(" }");
        }

        public void Queues()
        {
            Queue<string> myQueue = new Queue<string>();
            myQueue.Enqueue("Hello");
            myQueue.Enqueue("World");
            myQueue.Enqueue("From");
            myQueue.Enqueue("A");
            myQueue.Enqueue("Queue");

            foreach (string s in myQueue)
                Console.Write(s + " ");
            // Displays: Hello World From A Queue

        }

        public void Stacks()
        {
            Stack<string> myStack = new Stack<string>();
            myStack.Push("Hello");
            myStack.Push("World");
            myStack.Push("From");
            myStack.Push("A");
            myStack.Push("Queue");

            foreach (string s in myStack)
                Console.Write(s + " ");
            // Displays: Queue A From World Hello

        }

        public void foreachLoop()
        {
            // foreach loop to average grades in an array
            // set up an integer array and assign some values
            int[] arrGrades = new int[] { 78, 89, 90, 76, 98, 65 };
            // create three variables to hold the sum, number of grades, and the average
            int total = 0;
            int gradeCount = 0;
            double average = 0.0;
            // loop to iterate over each integer value in the array
            // foreach doesn't need to know the size initially as it is determined
            // at the time the array is accessed. 
            foreach (int grade in arrGrades)
            {
                total = total + grade; // add each grade value to total
                gradeCount++;          // increment counter for use in average
            }

            average = total / gradeCount;   // calculate average of grades
            Console.WriteLine(average);

        }

        public void sortArrayAndLists()
        {
            //Use the static System.Linq.Enumerable.OrderBy method 
            //to sort generic collections and arrays
            //For other collections, use the Cast method 
            //to convert to a generic collection and then use Enumerable.OrderBy.
            //OrderBy method takes an implementation of the IEnumerable interface 
            //and a function delegate (which can be a lambda expression)

            //The generic collection classes all implement IEnumerable 
            //and they, as well as arrays, can be sorted. 
            //The function delegate allows you to specify which property or method 
            //will be used to sort the data

            // Create a new array and populate it.
            int[] array = { 4, 2, 9, 3 };
            // Created a new, sorted array
            array = Enumerable.OrderBy(array, e => e).ToArray<int>();
            // Display the contents of the sorted array.
            foreach (int i in array)
            {
                Console.WriteLine(i);
            }

            // Create a list and populate it.
            List<string> list = new List<string>();
            list.Add("Michael");
            list.Add("Kate");
            list.Add("Andrea");
            list.Add("Angus");
            // Enumerate the sorted contents of the list.
            Console.WriteLine("\nList sorted by content");
            foreach (string person in Enumerable.OrderBy(list, e => e))
            {
                Console.WriteLine(person);
            }
            // Sort and enumerate based on a property.
            Console.WriteLine("\nList sorted by length property");
            foreach (string person in Enumerable.OrderBy(list, e => e.Length))
            {
                Console.WriteLine(person);
            }

            // Create a new ArrayList and populate it.
            ArrayList arraylist = new ArrayList(4);
            arraylist.Add("Michael");
            arraylist.Add("Kate");
            arraylist.Add("Andrea");
            arraylist.Add("Angus");
            // Sort the ArrayList.
            //The ArrayList collection cannot be used with the generic syntax.
            //It must be used the ArrayList.Sort() method
            arraylist.Sort();
            // Display the contents of the sorted ArrayList.
            Console.WriteLine("\nArraylist sorted by content");
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
        }

        public void copyColletionToArray()
        {
            //Use ICollection.CopyTo method implemented by all collection classes
            //or use ToArray method implemented by ArrayList, Stack, and Queue
            //CopyTo copies the collection's elements to an existing array
            //whereas ToArray creates a new array before copying

            //If the types do not match, or no implicit conversion is possible
            //from the collection element's type to the array element's type
            //a System.InvalidCastException exception is thrown
            // Create a new ArrayList and populate it.
            ArrayList list = new ArrayList(5);
            list.Add("Brenda");
            list.Add("George");
            list.Add("Justin");
            list.Add("Shaun");
            list.Add("Meaghan");
            // Create a string array and use the ICollection.CopyTo method
            string[] array1 = new string[list.Count];
            list.CopyTo(array1, 0);

            //If you call ToArray with no arguments, it returns an object[] array
            //regardless of the type of objects contained in the collection
            // Use ArrayList.ToArray to create an object array 
            //from the contents of the collection.
            object[] array2 = list.ToArray();
            // Use ArrayList.ToArray to create a strongly typed string
            // array from the contents of the collection.
            string[] array3 = (string[])list.ToArray(typeof(String));

            // Display the contents of the three arrays.
            Console.WriteLine("Array 1:");
            foreach (string s in array1)
            {
                Console.WriteLine("\t{0}", s);
            }
            Console.WriteLine("Array 2:");
            foreach (string s in array2)
            {
                Console.WriteLine("\t{0}", s);
            }
            Console.WriteLine("Array 3:");
            foreach (string s in array3)
            {
                Console.WriteLine("\t{0}", s);
            }

            Console.WriteLine("\nMain method complete. Press Enter");
        }

        public void selectCollectionElements()
        {
            //The output of a LINQ query is an instance of IEnumerable 
            //containing the collection/array elements that meet search criteria

            // Create a list of fruit.
            List<Fruit> myList = new List<Fruit>() {
                new Fruit("apple", "green"),
                new Fruit("orange", "orange"),
                new Fruit("banana", "yellow"),
                new Fruit("mango", "yellow"),
                new Fruit("cherry", "red"),
                new Fruit("fig", "brown"),
                new Fruit("cranberry", "red"),
                new Fruit("pear", "green")
            };
            // Select the names of fruit that isn't red and whose name
            // does not start with the letter "c." . Using LINQ
            IEnumerable<string> myResult = from e in myList
                                           where e.Color!= "red" && e.Name[0] != 'c'
                                           orderby e.Name
                                           select e.Name;
            // Write out the results.
            foreach (string result in myResult)
            {
                Console.WriteLine("Result: {0}", result);
            }
            // Perform the same query using lambda expressions.
            myResult = myList.Where(e => e.Color != "red" && e.Name[0]!= 'c').OrderBy(e => e.Name).Select(e => e.Name);
            // Write out the results.
            foreach (string result in myResult)
            {
                Console.WriteLine("Lambda Result: {0}", result);
            }
            Console.WriteLine("\n\nMain method complete. Press Enter");
        }

        public void removeDuplicateFromCollection()
        {
            // Create a list of fruit, including duplicates.
            List<Fruit> myList = new List<Fruit>() {
                new Fruit("apple", "green"),
                new Fruit("apple", "red"),
                new Fruit("orange", "orange"),
                new Fruit("orange", "orange"),
                new Fruit("banana", "yellow"),
                new Fruit("mango", "yellow"),
                new Fruit("cherry", "red"),
                new Fruit("fig", "brown"),
                new Fruit("fig", "brown"),
                new Fruit("fig", "brown"),
                new Fruit("cranberry", "red"),
                new Fruit("pear", "green")
            };
            // Use the Distinct method to remove duplicates
            // and print out the unique entries that remain.
            foreach (Fruit fruit in myList.Distinct(new FruitComparer()))
            {
                Console.WriteLine("Fruit: {0}:{1}", fruit.Name, fruit.Color);
            }
            Console.WriteLine("\n\nMain method complete. Press Enter");
        }
    }
}
