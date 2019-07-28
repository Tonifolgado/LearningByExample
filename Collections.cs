using System;
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

    }
}
