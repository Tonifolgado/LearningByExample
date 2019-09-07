using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    [Author("Lena")]
    [Author("Allen", Company = "Apress")]
    class FakeClass
    {
        public static void programAttributesInspection()
        {
            // Get a Type object for this class.
            Type type = typeof(FakeClass);

            // Get the attributes for the type. Apply a filter so that only
            // instances of AuthorAttribute are returned.
            object[] attrs =
                type.GetCustomAttributes(typeof(AuthorAttribute), true);
            // Enumerate the attributes and display their details.
            foreach (AuthorAttribute a in attrs)
            {
                Console.WriteLine(a.Name + ", " + a.Company);
            }
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();
        }

        public static void extractTypeMembers()
        {
            // Get the type we are interested in.
            Type myType = typeof(FakeClass);
            // Get the constructor details.
            Console.WriteLine("\nConstructors...");
            foreach (ConstructorInfo constr in myType.GetConstructors())
            {
                Console.Write("Constructor: ");
                // Get the paramters for this constructor.
                foreach (ParameterInfo param in constr.GetParameters())
                {
                    Console.Write("{0} ({1}), ", param.Name, param.ParameterType);
                }
                Console.WriteLine();
            }
            // Get the method details.
            Console.WriteLine("\nMethods...");
            foreach (MethodInfo method in myType.GetMethods())
            {
                Console.Write(method.Name);
                // Get the paramters for this constructor.
                foreach (ParameterInfo param in method.GetParameters())
                {
                    Console.Write("{0} ({1}), ", param.Name, param.ParameterType);
                }
                Console.WriteLine();
            }
            // Get the property details.
            Console.WriteLine("\nProperty...");
            foreach (PropertyInfo property in myType.GetProperties())
            {
                Console.Write("{0} ", property.Name);
                // Get the paramters for this constructor.
                foreach (MethodInfo accessor in property.GetAccessors())
                {
                    Console.Write("{0}, ", accessor.Name);
                }
                Console.WriteLine();
            }

            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();
        }
        public string MyProperty
        {
            get;
            set;
        }

        public FakeClass()
        { }
        public FakeClass(string param1, int param2, char param3)
        {
        }
        public static void printMessage(string param1, int param2, char param3)
        {
            Console.WriteLine("PrintMessage {0} {1} {2}", param1, param2, param3);
        }

        public void printMessage2(string param1, int param2, char param3)
        {
            Console.WriteLine("PrintMessage {0} {1} {2}", param1, param2, param3);
        }

        public static void invokeTypeMemberUsingReflection()
        {
            // Create an instance of this type.
            object myInstance = new FakeClass();
            // Get the type we are interested in.
            Type myType = typeof(FakeClass);
            // Get the method information.
            MethodInfo methodInfo = myType.GetMethod("printMessage",
                new Type[] { typeof(string), typeof(int), typeof(char) });
            // Invoke the method using the instance we created.
            myType.InvokeMember("printMessage", BindingFlags.InvokeMethod,
                null, myInstance, new object[] { "hello", 37, 'c' });
            methodInfo.Invoke(null, BindingFlags.InvokeMethod, null,
                new object[] { "hello", 37, 'c' }, null);
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();
        }

        public static void invokeTypeMemberDinamically()
        {
            // Create an instance of this type.
            dynamic myInstance = new FakeClass();
            // Call the method dynamically.
            myInstance.printMessage2("hello", 37, 'c');
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();
        }

        public static void customDynamicTypeManagement()
        {
            dynamic dynamicDict = new MyDynamicDictionary();
            // Set some properties.
            Console.WriteLine("Setting property values");
            dynamicDict.FirstName = "Adam";
            dynamicDict.LastName = "Freeman";
            // Get some properties.
            Console.WriteLine("\nGetting property values");
            Console.WriteLine("Firstname {0}", dynamicDict.FirstName);
            Console.WriteLine("Lastname {0}", dynamicDict.LastName);
            // Call an implemented member.
            Console.WriteLine("\nGetting a static property");
            Console.WriteLine("Count {0}", dynamicDict.Count);
            Console.WriteLine("\nGetting a non-existent property");
            try
            {
                Console.WriteLine("City {0}", dynamicDict.City);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                Console.WriteLine("Caught exception");
            }
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();
        }
    }
    class MyDynamicDictionary : DynamicObject
    {
        private IDictionary<string, object> dict = new Dictionary<string, object>();
        public int Count
        {
            get
            {
                Console.WriteLine("Get request for Count property");
                return dict.Count;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Console.WriteLine("Get request for {0}", binder.Name);
            return dict.TryGetValue(binder.Name, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine("Set request for {0}, value {1}", binder.Name, value);
            dict[binder.Name] = value;
            return true;
        }

    }



}

