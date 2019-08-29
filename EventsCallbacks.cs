using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class EventsCallbacks
    {
        //a delegate for methods that return an int
        public delegate int Calculate(int x, int y);
        //a dekegate for methods that return void
        public delegate void Delvoid();
        //OnChange is a delegate property of type Action
        //public Action OnChange { get; set; }
        //By using the event syntax, you are no use a public property but a public field
        //An event cannot be directly assigned to (with the = instead of +=) operator. 
        //So you don’t have the risk of someone removing all previous subscriptions,
        //as with the delegate syntax
        public event Action OnChange = delegate { };
        public event EventHandler<MyArgs> OnChange2 = delegate { };
        public static EventHandler MyEvent;


        #region methodsToUseWithDelegates
        public int Add(int x, int y) { return x + y; }
        public int Multiply(int x, int y) { return x * y; }        

        public void MethodOne() { Console.WriteLine("MethodOne"); }
        public void MethodTwo() { Console.WriteLine("MethodTwo"); }

        static void EventHandlerMethod(object sender, EventArgs args)
        {
            Console.WriteLine("Named method called");
        }

        public class MyArgs : EventArgs
        {
            public int Value { get; set; }
            public MyArgs(int value)
            {
                Value = value;
            }

        }

        #endregion


        public void UseDelegate()
        {
            //el method Add is assigned to the delegate calc
            Calculate calc = Add;
            //another way is using lambda expression to assing an anonymous method
            //    Calculate calc = (x, y) => x + y;
            Console.WriteLine(calc(3, 4)); // Displays 7
            //el method Multiply is assigned to the delegate calc
            calc = Multiply;
            // it can be using a lamda expression
            //    calc = (x, y) => x * y;
            Console.WriteLine(calc(3, 4)); // Displays 12
        }
        public void multicastDelegate()
        {
            Delvoid d = MethodOne;
            d += MethodTwo;
            //when executing the delegate all the subscribed methods are executed
            d();            
        // Displays
        // MethodOne
        // MethodTwo
        
        }

        public void usingActionDelegate()
        {
            //a built-in delegate type that doesn’t return a value
            Action<int, int> calc = (x, y) =>
            {
                Console.WriteLine(x + y);
            };

            calc(3, 4); // Displays 7
        }

        public void Raise()
        {
            //check if the delegate is used (subscribed)
            if (OnChange != null)
            {
                OnChange();
            }
        }

        public void Raise2()
        {
            OnChange2(this, new MyArgs(42));
        }

        public void handleEventWithAnonymousFunction()
        {
            // Use a named method to register for the event.
            MyEvent += new EventHandler(EventHandlerMethod);
            // Use an anonymous delegate to register for the event.
            MyEvent += new EventHandler(delegate (object sender, EventArgs eventargs)
            {
                Console.WriteLine("Anonymous delegate called");
            });
            // Use a lamda expression to register for the event.
            MyEvent += new EventHandler((sender, eventargs) =>
            {
                Console.WriteLine("Lamda expression called");
            });
            Console.WriteLine("Raising the event");
            MyEvent.Invoke(new object(), new EventArgs());
            Console.WriteLine("\nMain method complete. Press Enter.");
            Console.ReadLine();

        }


    }






}
