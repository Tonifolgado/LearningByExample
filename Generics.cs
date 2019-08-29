using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Generics
    {
        public void usingGenericMethod()
        {
            int[] arrInts = new int[] { 2, 5, 4, 7, 6, 7, 1, 3, 9, 8 };
            char[] arrChar = new char[] { 'f', 'a', 'r', 'c', 'h' };

            // Sorting: integer Sort
            for (int i = 0; i < arrInts.Length; i++)
            {
                for (int j = i + 1; j < arrInts.Length; j++)
                {
                    if (arrInts[i] > arrInts[j])
                    {
                        swap<int>(ref arrInts[i], ref arrInts[j]);
                    }
                }
            }

            foreach (var item in arrInts)
            {
                Console.WriteLine(item);
            }

            // Sorting: character Sort
            for (int i = 0; i < arrChar.Length; i++)
            {
                for (int j = i + 1; j < arrChar.Length; j++)
                {
                    if (arrChar[i] > arrChar[j])
                    {
                        swap<char>(ref arrChar[i], ref arrChar[j]);
                    }
                }
            }

            foreach (var item in arrChar)
            {
                Console.WriteLine(item);
            }
        }

        static void swap<T>(ref T valueOne, ref T valueTwo)
        {
            T temp = valueOne;
            valueOne = valueTwo;
            valueTwo = temp;
        }

        public void stronglyTypedCollection()
        {
            //When you instantiate the collection, specify the type of object
            //the collection should contain using the generics syntax
            // Create an AssemblyName object for use during the example.
            AssemblyName assembly1 = new AssemblyName("com.microsoft.crypto, " +
                "Culture=en, PublicKeyToken=a5d015c7d5a0b012, Version=1.0.0.0");
            // Create and use a Dictionary of AssemblyName objects.
            Dictionary<string, AssemblyName> assemblyDictionary =
                new Dictionary<string, AssemblyName>();
            assemblyDictionary.Add("Crypto", assembly1);
            AssemblyName a1 = assemblyDictionary["Crypto"];

            Console.WriteLine("Got AssemblyName from dictionary: {0}", a1);
            // Create and use a List of Assembly Name objects.
            List<AssemblyName> assemblyList = new List<AssemblyName>();
            assemblyList.Add(assembly1);
            AssemblyName a2 = assemblyList[0];
            Console.WriteLine("\nFound AssemblyName in list: {0}", a1);
            // Create and use a Stack of Assembly Name objects.
            Stack<AssemblyName> assemblyStack = new Stack<AssemblyName>();
            assemblyStack.Push(assembly1);
            AssemblyName a3 = assemblyStack.Pop();
            Console.WriteLine("\nPopped AssemblyName from stack: {0}", a1);
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
        }
    }
    //Implementation of a generic type
    class Bag<T>
    {
        // A List to hold the bags's contents. The list must be
        // of the same type as the bag.
        private List<T> items = new List<T>();
        // A method to add an item to the bag.
        public void Add(T item)
        {
            items.Add(item);
        }
        // A method to get a random item from the bag.
        public T Remove()
        {
            T item = default(T);
            if (items.Count != 0)
            {
                // Determine which item to remove from the bag.
                Random r = new Random();
                int num = r.Next(0, items.Count);
                // Remove the item.
                item = items[num];
                items.RemoveAt(num);
            }
            return item;
        }
        // A method to provide an enumerator from the underlying list
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        // A method to remove all items from the bag 
        // and return them as an array
        public T[] RemoveAll()
        {
            T[] i = items.ToArray();
            items.Clear();
            return i;
        }

        public static void genericBagManagement()
        {
            // Create a new bag of strings.
            Bag<string> bag = new Bag<string>();
            // Add strings to the bag.
            bag.Add("Darryl");
            bag.Add("Bodders");
            bag.Add("Gary");
            bag.Add("Mike");
            bag.Add("Nigel");
            bag.Add("Ian");
            Console.WriteLine("Bag contents are:");
            foreach (string elem in bag)
            {
                Console.WriteLine("Element: {0}", elem);
            }
            // Take four strings from the bag and display.
            Console.WriteLine("\nRemoving individual elements");
            Console.WriteLine("Removing = {0}", bag.Remove());
            Console.WriteLine("Removing = {0}", bag.Remove());
            Console.WriteLine("Removing = {0}", bag.Remove());
            Console.WriteLine("Removing = {0}", bag.Remove());
            Console.WriteLine("\nBag contents are:");
            foreach (string elem in bag)
            {
                Console.WriteLine("Element: {0}", elem);
            }
            // Remove the remaining items from the bag.
            Console.WriteLine("\nRemoving all elements");
            string[] s = bag.RemoveAll();
            Console.WriteLine("\nBag contents are:");
            foreach (string elem in bag)
            {
                Console.WriteLine("Element: {0}", elem);
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
            Console.ReadLine();

        }
    }
}
