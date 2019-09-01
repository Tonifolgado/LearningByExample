using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Reflection
    {
        public void retrieveTypeInformation()
        {
            // Obtain type information using the typeof operator.
            Type t1 = typeof(StringBuilder);
            Console.WriteLine(t1.AssemblyQualifiedName);

            // Obtain type information using the Type.GetType method.
            // Case-sensitive, return null if not found.
            Type t2 = Type.GetType("System.String");
            Console.WriteLine(t2.AssemblyQualifiedName);
            // Case-sensitive, throw TypeLoadException if not found.
            Type t3 = Type.GetType("System.String", true);
            Console.WriteLine(t3.AssemblyQualifiedName);
            // Case-insensitive, throw TypeLoadException if not found.
            Type t4 = Type.GetType("system.string", true, true);
            Console.WriteLine(t4.AssemblyQualifiedName);
            // Assembly-qualifed type name.
            Type t5 = Type.GetType("System.Data.DataSet,System.Data," +
                "Version=2.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089");
            Console.WriteLine(t5.AssemblyQualifiedName);

            // Obtain type information using the Object.GetType method.
            StringBuilder sb = new StringBuilder();
            Type t6 = sb.GetType();
            Console.WriteLine(t6.AssemblyQualifiedName);
            Console.WriteLine("\nMain method complete. Press Enter.");
        }

        // A method to test whether an object is an instance of a type
        // or a derived type.
        public static bool IsType(object obj, string type)
        {
            // Get the named type, use case-insensitive search, throw
            // an exception if the type is not found.
            Type t = Type.GetType(type, true, true);

            return t == obj.GetType() || obj.GetType().IsSubclassOf(t);
        }

        public void testObjectType()
        {
            // Create a new StringReader for testing.
            Object someObject = new StringReader("This is a StringReader");

            // Test if someObject is a StringReader by obtaining and
            // comparing a Type reference using the typeof operator.
            if (typeof(StringReader) == someObject.GetType())
            {
                Console.WriteLine("typeof: someObject is a StringReader");
            }
            // Test if someObject is, or is derived from, a TextReader
            // using the is operator.
            if (someObject is TextReader)
            {
                Console.WriteLine(
                    "is: someObject is a TextReader or a derived class");
            }
            // Test if someObject is, or is derived from, a TextReader using
            // the Type.GetType and Type.IsSubclassOf methods.
            if (IsType(someObject, "System.IO.TextReader"))
            {
                Console.WriteLine("GetType: someObject is a TextReader");
            }
            // Use the "as" operator to perform a safe cast.
            StringReader reader = someObject as StringReader;
            if (reader != null)
            {
                Console.WriteLine("as: someObject is a StringReader");
            }
            Console.WriteLine("\nMain method complete. Press Enter.");
        }

        //how to instantiate a System.Text.StringBuilder object using reflection
        //and how to specify the initial content for the StringBuilder (a string) 
        //and its capacity (an int)
        public static StringBuilder CreateStringBuilder()
        {
            // Obtain the Type for the StringBuilder class.
            Type type = typeof(StringBuilder);
            // Create a Type[] containing Type instances for each
            // of the constructor arguments - a string and an int.
            Type[] argTypes = new Type[] { typeof(System.String),
              typeof(System.Int32) };
            // Obtain the ConstructorInfo object.
            ConstructorInfo cInfo = type.GetConstructor(argTypes);
            // Create an object[] containing the constructor arguments.
            object[] argVals = new object[] { "Some string", 30 };
            // Create the object and cast it to StringBuilder.
            StringBuilder sb = (StringBuilder)cInfo.Invoke(argVals);
            return sb;
        }

        public static void instantiateObjectUsingReflection()
        {
            // Instantiate a new IPlugin using the PluginFactory.
            //IPlugin plugin = PluginFactory.CreatePlugin(
            //    "Recipe03-12",  // Private assembly name
            //    "Apress.VisualCSharpRecipes.Chapter03.SimplePlugin",
            //    // Plug-in class name.
            //    "A Simple Plugin"       // Plug-in instance description
            //);

            // Start and stop the new plug-in.
            //plugin.Start();
            //plugin.Stop();
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter.");

        }


    }
}
