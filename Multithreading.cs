using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningByExample1
{
    public class Multithreading
    {
        // A private class used to pass data to the DisplayMessage method 
        //when it is executed using the thread pool.
        private class MessageInfo
        {
            private int iterations;
            private string message;
            // A constructor that takes configuration settings for the thread.
            public MessageInfo(int iterations, string message)
            {
                this.iterations = iterations;
                this.message = message;
            }
            // Properties to retrieve configuration settings.
            public int Iterations { get { return iterations; } }
            public string Message { get { return message; } }
        }

        // A private class used to pass initialization data to a new thread.
        private class ThreadStartData
        {
            public ThreadStartData(int iterations, string message, int delay)
            {
                this.iterations = iterations;

                this.message = message;
                this.delay = delay;
            }
            // Member variables hold initialization data for a new thread.
            private readonly int iterations;
            private readonly string message;
            private readonly int delay;
            // Properties provide read-only access to initialization data.
            public int Iterations { get { return iterations; } }
            public string Message { get { return message; } }
            public int Delay { get { return delay; } }
        }

        // A method that conforms to the System.Threading.WaitCallback 
        //delegate signature. Displays a message to the console.
        public static void DisplayMessage(object state)
        {
            // Safely cast the state argument to a MessageInfo object.
            MessageInfo config = state as MessageInfo;
            // If the config argument is null, no arguments were passed to
            // the ThreadPool.QueueUserWorkItem method; use default values.
            if (config == null)
            {
                // Display a fixed message to the console three times.
                for (int count = 0; count < 3; count++)
                {
                    Console.WriteLine("A thread pool example.");
                    // Sleep for the purpose of demonstration. Avoid sleeping
                    // on thread-pool threads in real applications.
                    Thread.Sleep(1000);
                }
            }
            else
            {

                // Display the specified message the specified number of times.
                for (int count = 0; count < config.Iterations; count++)
                {
                    Console.WriteLine(config.Message);
                    // Sleep for the purpose of demonstration. Avoid sleeping
                    // on thread-pool threads in real applications.
                    Thread.Sleep(1000);
                }
            }
        }

        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.        
        private static void TraceMsg(DateTime time, string msg)
        {
            Console.WriteLine("[{0,3}/{1}] - {2} : {3}",
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread ? "pool" : "fore",
                time.ToString("HH:mm:ss.ffff"), msg);
        }

        // A delegate that allows you to perform asynchronous execution of
        // LongRunningMethod.
        public delegate DateTime AsyncExampleDelegate(int delay, string name);

        // A simulated long-running method.
        public static DateTime LongRunningMethod(int delay, string name)
        {
            TraceMsg(DateTime.Now, name + " example - thread starting.");
            // Simulate time-consuming processing.
            Thread.Sleep(delay);
            TraceMsg(DateTime.Now, name + " example - thread stopping.");
            // Return the method's completion time.
            return DateTime.Now;
        }

        // This method executes LongRunningMethod asynchronously and continues
        // with other processing. Once the processing is complete, the method
        // blocks until LongRunningMethod completes.
        public static void BlockingExample()
        {
            Console.WriteLine(Environment.NewLine +
                "*** Running Blocking Example ***");
            // Invoke LongRunningMethod asynchronously. Pass null for both the
            // callback delegate and the asynchronous state object.
            AsyncExampleDelegate longRunningMethod = LongRunningMethod;
            IAsyncResult asyncResult = longRunningMethod.BeginInvoke(2000,
                "Blocking", null, null);
            // Perform other processing until ready to block.
            for (int count = 0; count < 3; count++)
            {
                TraceMsg(DateTime.Now,
                    "Continue processing until ready to block...");
                Thread.Sleep(200);
            }
            // Block until the asynchronous method completes.
            TraceMsg(DateTime.Now,
                "Blocking until method is complete...");
            // Obtain the completion data for the asynchronous method.
            DateTime completion = DateTime.MinValue;

            try
            {
                completion = longRunningMethod.EndInvoke(asyncResult);
            }

            catch
            {
                // Catch and handle those exceptions you would if calling
                // LongRunningMethod directly.
            }
            // Display completion information.
            TraceMsg(completion, "Blocking example complete.");
        }

        // This method executes LongRunningMethod asynchronously and then
        // enters a polling loop until LongRunningMethod completes.
        public static void PollingExample()
        {
            Console.WriteLine(Environment.NewLine +
                "*** Running Polling Example ***");
            // Invoke LongRunningMethod asynchronously. Pass null for both the
            // callback delegate and the asynchronous state object.
            AsyncExampleDelegate longRunningMethod = LongRunningMethod;
            IAsyncResult asyncResult = longRunningMethod.BeginInvoke(2000,
                "Polling", null, null);
            // Poll the asynchronous method to test for completion. If not
            // complete, sleep for 300 ms before polling again.
            TraceMsg(DateTime.Now, "Poll repeatedly until method is complete.");
            while (!asyncResult.IsCompleted)
            {
                TraceMsg(DateTime.Now, "Polling...");
                Thread.Sleep(300);
            }
            // Obtain the completion data for the asynchronous method.
            DateTime completion = DateTime.MinValue;
            try
            {
                completion = longRunningMethod.EndInvoke(asyncResult);
            }
            catch
            {
                // Catch and handle those exceptions you would if calling
                // LongRunningMethod directly.
            }

            // Display completion information.
            TraceMsg(completion, "Polling example complete.");
        }

        // This method executes LongRunningMethod asynchronously and then
        // uses a WaitHandle to wait efficiently until LongRunningMethod
        // completes. Use of a timeout allows the method to break out of
        // waiting in order to update the user interface or fail if the
        // asynchronous method is taking too long.
        public static void WaitingExample()
        {
            Console.WriteLine(Environment.NewLine +
                "*** Running Waiting Example ***");
            // Invoke LongRunningMethod asynchronously. Pass null for both the
            // callback delegate and the asynchronous state object.
            AsyncExampleDelegate longRunningMethod = LongRunningMethod;
            IAsyncResult asyncResult = longRunningMethod.BeginInvoke(2000,
                "Waiting", null, null);

            // Wait for the asynchronous method to complete. Time out after
            // 300 ms and display status to the console before continuing to
            // wait.
            TraceMsg(DateTime.Now, "Waiting until method is complete...");
            while (!asyncResult.AsyncWaitHandle.WaitOne(300, false))
            {
                TraceMsg(DateTime.Now, "Wait timeout...");
            }
            // Obtain the completion data for the asynchronous method.
            DateTime completion = DateTime.MinValue;
            try
            {
                completion = longRunningMethod.EndInvoke(asyncResult);
            }
            catch
            {
                // Catch and handle those exceptions you would if calling
                // LongRunningMethod directly.
            }
            // Display completion information.
            TraceMsg(completion, "Waiting example complete.");
        }

        // This method executes LongRunningMethod asynchronously multiple
        // times and then uses an array of WaitHandle objects to wait
        // efficiently until all of the methods are complete. Use of
        // a timeout allows the method to break out of waiting in order
        // to update the user interface or fail if the asynchronous
        // method is taking too long.

        public static void WaitAllExample()
        {
            Console.WriteLine(Environment.NewLine +
                "*** Running WaitAll Example ***");
            // An ArrayList to hold the IAsyncResult instances for each of the
            // asynchronous methods started.
            ArrayList asyncResults = new ArrayList(3);
            // Invoke three LongRunningMethods asynchronously. Pass null for
            // both the callback delegate and the asynchronous state object.
            // Add the IAsyncResult instance for each method to the ArrayList.
            AsyncExampleDelegate longRunningMethod = LongRunningMethod;
            asyncResults.Add(longRunningMethod.BeginInvoke(3000,
                "WaitAll 1", null, null));
            asyncResults.Add(longRunningMethod.BeginInvoke(2500,
                "WaitAll 2", null, null));
            asyncResults.Add(longRunningMethod.BeginInvoke(1500,
                "WaitAll 3", null, null));
            // Create an array of WaitHandle objects that will be used to wait
            // for the completion of all the asynchronous methods.
            WaitHandle[] waitHandles = new WaitHandle[3];
            for (int count = 0; count < 3; count++)
            {
                waitHandles[count] =
                    ((IAsyncResult)asyncResults[count]).AsyncWaitHandle;
            }
            // Wait for all three asynchronous method to complete. Time out
            // after 300 ms and display status to the console before continuing
            // to wait.
            TraceMsg(DateTime.Now, "Waiting until all 3 methods are complete...");
            while (!WaitHandle.WaitAll(waitHandles, 300, false))
            {
                TraceMsg(DateTime.Now, "WaitAll timeout...");
            }
            // Inspect the completion data for each method, and determine the
            // time at which the final method completed.
            DateTime completion = DateTime.MinValue;

            foreach (IAsyncResult result in asyncResults)
            {
                try
                {
                    DateTime time = longRunningMethod.EndInvoke(result);
                    if (time > completion) completion = time;
                }
                catch
                {
                    // Catch and handle those exceptions you would if calling
                    // LongRunningMethod directly.
                }
            }
            // Display completion information.
            TraceMsg(completion, "WaitAll example complete.");
        }

        // This method executes LongRunningMethod asynchronously and passes
        // an AsyncCallback delegate instance. The referenced CallbackHandler
        // method is called automatically when the asynchronous method
        // completes, leaving this method free to continue processing.
        public static void CallbackExample()
        {
            Console.WriteLine(Environment.NewLine +
                "*** Running Callback Example ***");
            // Invoke LongRunningMethod asynchronously. Pass an AsyncCallback
            // delegate instance referencing the CallbackHandler method that
            // will be called automatically when the asynchronous method
            // completes. Pass a reference to the AsyncExampleDelegate delegate
            // instance as asynchronous state; otherwise, the callback method
            // has no access to the delegate instance in order to call
            // EndInvoke.
            AsyncExampleDelegate longRunningMethod = LongRunningMethod;
            IAsyncResult asyncResult = longRunningMethod.BeginInvoke(2000,
                "Callback", CallbackHandler, longRunningMethod);
            // Continue with other processing.
            for (int count = 0; count < 15; count++)
            {
                TraceMsg(DateTime.Now, "Continue processing...");
                Thread.Sleep(200);
            }
        }

        // A method to handle asynchronous completion using callbacks.
        public static void CallbackHandler(IAsyncResult result)
        {

            // Extract the reference to the AsyncExampleDelegate instance
            // from the IAsyncResult instance. This allows you to obtain the
            // completion data.
            AsyncExampleDelegate longRunningMethod =
                (AsyncExampleDelegate)result.AsyncState;
            // Obtain the completion data for the asynchronous method.
            DateTime completion = DateTime.MinValue;
            try
            {
                completion = longRunningMethod.EndInvoke(result);
            }
            catch
            {
                // Catch and handle those exceptions you would if calling
                // LongRunningMethod directly.
            }
            // Display completion information.
            TraceMsg(completion, "Callback example complete.");
        }

        // A method that is executed when the AutoResetEvent is signaled
        // or the wait operation times out.
        private static void EventHandler(object state, bool timedout)
        {
            // Display appropriate message to the console based on whether
            // the wait timed out or the AutoResetEvent was signaled.
            if (timedout)
            {
                Console.WriteLine("{0} : Wait timed out.",
                    DateTime.Now.ToString("HH:mm:ss.ffff"));
            }
            else
            {
                Console.WriteLine("{0} : {1}",
                    DateTime.Now.ToString("HH:mm:ss.ffff"), state);
            }
        }

        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.
        private static void TraceMsg(string msg)
        {
            Console.WriteLine("[{0,3}] - {1} : {2}",
                Thread.CurrentThread.ManagedThreadId,
                DateTime.Now.ToString("HH:mm:ss.ffff"), msg);
        }

        // Declare the method that will be executed in its own thread. The
        // method displays a message to the console a specified number of
        // times, sleeping between each message for a specified duration.
        private static void DisplayMessage2(object config)
        {
            ThreadStartData data = config as ThreadStartData;

            if (data != null)
            {
                for (int count = 0; count < data.Iterations; count++)
                {
                    TraceMsg(data.Message);
                    // Sleep for the specified period.
                    Thread.Sleep(data.Delay);
                }
            }
            else
            {
                TraceMsg("Invalid thread configuration.");
            }
        }

        // Declare an object for synchronization of access to the console.
        // A static object is used because you are using it in static methods.
        private static object consoleGate = new Object();
        // Declare a Queue to represent the work queue.
        private static Queue<string> workQueue = new Queue<string>();
        // Declare a flag to indicate to activated threads that they should
        // terminate and not process more work items.
        private static bool processWorkItems = true;
        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.
        private static void TraceMsg2(string msg)
        {
            lock (consoleGate)
            {



                Console.WriteLine("[{0,3}/{1}] - {2} : {3}",
                                    Thread.CurrentThread.ManagedThreadId,
                                    Thread.CurrentThread.IsThreadPoolThread ? "pool" : "fore",
                                    DateTime.Now.ToString("HH:mm:ss.ffff"), msg);
            }
        }
        // Declare the method that will be executed by each thread to process
        // items from the work queue.
        private static void ProcessWorkItems()
        {
            // A local variable to hold the work item taken from the work queue.
            string workItem = null;
            TraceMsg2("Thread started, processing items from queue...");
            // Process items from the work queue until termination is signaled.
            while (processWorkItems)
            {
                // Obtain the lock on the work queue.
                Monitor.Enter(workQueue);

                try
                {
                    // Pop the next work item and process it, or wait if none
                    // is available.
                    if (workQueue.Count == 0)
                    {
                        TraceMsg2("No work items, waiting...");
                        // Wait until Pulse is called on the workQueue object.
                        Monitor.Wait(workQueue);
                    }
                    else
                    {
                        // Obtain the next work item.
                        workItem = workQueue.Dequeue();
                    }
                }
                finally
                {
                    // Always release the lock.
                    Monitor.Exit(workQueue);
                }
                // Process the work item if one was obtained.
                if (workItem != null)
                {
                    // Obtain a lock on the console and display a series
                    // of messages.
                    lock (consoleGate)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            TraceMsg2("Processing " + workItem);
                            Thread.Sleep(200);
                        }
                    }
                    // Reset the status of the local variable.
                    workItem = null;
                }
            }
            // This will be reached only if processWorkItems is false.
            TraceMsg2("Terminating.");
        }

        // Boolean to signal that the second thread should terminate.
        static bool terminate = false;
        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.
        private static void TraceMsg3(string msg)
        {
            Console.WriteLine("[{0,3}] - {1} : {2}",
                Thread.CurrentThread.ManagedThreadId,
                DateTime.Now.ToString("HH:mm:ss.ffff"), msg);
        }

        // Declare the method that will be executed on the separate thread.
        // The method waits on the EventWaitHandle before displaying a message
        // to the console and then waits two seconds and loops.
        private static void DisplayMessage3()
        {
            // Obtain a handle to the EventWaitHandle with the name "EventExample".
            EventWaitHandle eventHandle =
                EventWaitHandle.OpenExisting("EventExample");
            TraceMsg3("DisplayMessage Started.");
            while (!terminate)
            {
                // Wait on the EventWaitHandle, time out after 2 seconds. WaitOne
                // returns true if the event is signaled; otherwise, false. The
                // first time through, the message will be displayed immediately
                // because the EventWaitHandle was created in a signaled state.
                if (eventHandle.WaitOne(2000, true))
                {
                    TraceMsg3("EventWaitHandle In Signaled State.");
                }
                else
                {
                    TraceMsg3("WaitOne Timed Out -- " +
                        "EventWaitHandle In Unsignaled State.");
                }
                Thread.Sleep(2000);
            }

            TraceMsg3("Thread Terminating.");
        }

        // Boolean to signal that the second thread should terminate.
        static bool terminate2 = false;
        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.
        private static void TraceMsg4(string msg)
        {
            Console.WriteLine("[{0,3}] - {1} : {2}",
                Thread.CurrentThread.ManagedThreadId,
                DateTime.Now.ToString("HH:mm:ss.ffff"), msg);
        }
        // Declare the method that will be executed on the separate thread.
        // In a loop the method waits to obtain a Mutex before displaying a
        // message to the console and then waits 1 second before releasing the
        // Mutex.
        private static void DisplayMessage4()
        {
            // Obtain a handle to the Mutex with the name "MutexExample".
            // Do not attempt to take ownership immediately.
            using (Mutex mutex = new Mutex(false, "MutexExample"))
            {
                TraceMsg4("Thread started.");
                while (!terminate2)
                {
                    // Wait on the Mutex.
                    mutex.WaitOne();
                    TraceMsg4("Thread owns the Mutex.");
                    Thread.Sleep(1000);
                    TraceMsg4("Thread releasing the Mutex.");
                    // Release the Mutex.
                    mutex.ReleaseMutex();
                    // Sleep a little to give another thread a good chance of
                    // acquiring the Mutex.
                    Thread.Sleep(100);
                }
                TraceMsg4("Thread terminating.");
            }
        }

        // Boolean to signal that the second thread should terminate.
        static bool terminate5 = false;

        // A utility method for displaying useful trace information to the
        // console along with details of the current thread.
        private static void TraceMsg5(string msg)
        {
            Console.WriteLine("[{0,3}] - {1} : {2}",
                Thread.CurrentThread.ManagedThreadId,
                DateTime.Now.ToString("HH:mm:ss.ffff"), msg);
        }

        // Declare the method that will be executed on the separate thread.
        // In a loop the method waits to obtain a Semaphore before displaying a
        // message to the console and then waits 1 second before releasing the
        // Semaphore.
        private static void DisplayMessage5()
        {
            // Obtain a handle to the Semaphore with the name "SemaphoreExample".
            using (Semaphore sem = Semaphore.OpenExisting("SemaphoreExample"))
            {
                TraceMsg5("Thread started.");
                while (!terminate5)
                {
                    // Wait on the Semaphore.
                    sem.WaitOne();
                    TraceMsg5("Thread owns the Semaphore.");
                    Thread.Sleep(1000);
                    TraceMsg5("Thread releasing the Semaphore.");
                    // Release the Semaphore.
                    sem.Release();
                    // Sleep a little to give another thread a good chance of
                    // acquiring the Semaphore.
                    Thread.Sleep(100);
                }
                TraceMsg5("Thread terminating.");
            }
        }


        #region threads

        [ThreadStatic]
        public static int _field;

        public static ThreadLocal<int> _anotherfield = new ThreadLocal<int>(() =>
        {
            //CurrentThread is used to get info about the thread executing
            return Thread.CurrentThread.ManagedThreadId;
        });

        public void SimpleMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(1000);
            }

        }

        public void methodExecutionUsingNewThread()
        {
            // Create a new Thread object specifying DisplayMessage
            // as the method it will execute.
            Thread thread = new Thread(DisplayMessage);
            // Make this a foreground thread - this is the
            // default - call used for example purposes.
            thread.IsBackground = false;
            // Create a new ThreadStartData object to configure the thread.
            ThreadStartData config =
                new ThreadStartData(5, "A thread example.", 500);
            TraceMsg("Starting new thread.");

            // Start the new thread and pass the ThreadStartData object
            // containing the initialization data.
            thread.Start(config);
            // Continue with other processing.
            for (int count = 0; count < 13; count++)
            {
                TraceMsg("Main thread continuing processing...");
                Thread.Sleep(200);
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void MethodWithParam(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(0);
            }
        }

        public void TwoThreads()
        {
            Thread t = new Thread(new ThreadStart(SimpleMethod));
            t.IsBackground = false;
            t.Start();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work");
                //to indicate the OS that the thread has finished
                //and switch to another thread
                Thread.Sleep(0);
            }
            //the thread t waits until the main thread finishes
            t.Join();
        }

        public void ThreadWithMethodParam()
        {
            Thread t2 = new Thread(new ParameterizedThreadStart(MethodWithParam));
            t2.Start(5);
            t2.Join();
        }

        public void HowToStopThread()
        {
            //How to stop a thread
            bool stopped = false;

            //thread initialized with a lambda expression
            //it keeps running until stopped becomes true
            Thread t = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            stopped = true;
            t.Join();
        }

        public void UseOfThreadStatic()
        {
            //Two thread that share a local variable (max 10)
            //Each thread gets its own copy of the field (marked as ThreadStatic)
            //Without this attribute both threads access the same variable (max 20)
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread A: {0}", _field);
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread B: {0}", _field);
                }
            }).Start();
        }

        public void UsingThreadLocalData()
        {
            //Using local data in a thread
            new Thread(() =>
            {
                for (int x = 0; x < _anotherfield.Value; x++)
                { Console.WriteLine("Thread A: {0}", x); }
            }).Start();
            new Thread(() =>
            {
                for (int x = 0; x < _anotherfield.Value; x++)
                { Console.WriteLine("Thread B: {0}", x); }
            }).Start();
            Console.ReadKey();
        }

        // This thread procedure performs the task.
        static void ThreadProc(Object stateInfo)
        {
            // No state object was passed to QueueUserWorkItem, so stateInfo is null.
            Console.WriteLine("Hello from the thread pool.");
        }
        public void usingThreadPool()
        {
            // Queue the task.
            ThreadPool.QueueUserWorkItem(ThreadProc);
            Console.WriteLine("Main thread does some work, then sleeps.");
            Thread.Sleep(1000);

            Console.WriteLine("Main thread exits.");
            // The example displays output like the following:
            //       Main thread does some work, then sleeps.
            //       Hello from the thread pool.
            //       Main thread exits.

        }

        public void periodicalMethodExecution()
        {
            //Create a System.Threading.Timer object and pass it 
            //the method you want to execute along with a state object 
            //that the timer will pass to your method when the timer expires

            // Create the state object that is passed to the TimerHandler
            // method when it is triggered. In this case, a message to display.
            string state = "Timer expired.";
            Console.WriteLine("{0} : Creating Timer.",
                DateTime.Now.ToString("HH:mm:ss.ffff"));
            // Create a timer that fires first after 2 seconds and then every
            // second. Use an anonymous method for the timer expiry handler.
            using (Timer timer = new Timer(delegate (object s)
                {
                    Console.WriteLine("{0} : {1}",
                    DateTime.Now.ToString("HH:mm:ss.ffff"), s);
                }
                , state, 2000, 1000))
            {
                int period;
                // Read the new timer interval from the console until the
                // user enters 0 (zero). Invalid values use a default value
                // of 0, which will stop the example.
                do
                {
                    try
                    {
                        period = Int32.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        period = 0;
                    }
                    // Change the timer to fire using the new interval starting
                    // immediately.
                    if (period > 0) timer.Change(0, period);
                } while (period > 0);
            }
            // Wait to continue.
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void methodExecutionAtaSpecificTime()
        {
            // Create a 30-second timespan.
            TimeSpan waitTime = new TimeSpan(0, 0, 30);
            // Create a Timer that fires once at the specified time. 
            // Specify an interval of −1 to stop the timer 
            // executing the method repeatedly
            // Use an anonymous method for the timer expiry handler.
            new Timer(delegate (object s)
            {
                Console.WriteLine("Timer fired at {0}",
                DateTime.Now.ToString("HH:mm:ss.ffff"));
            }
                      , null, waitTime, new TimeSpan(-1));
            Console.WriteLine("Waiting for timer. Press Enter to terminate.");
            Console.ReadLine();
        }

        public void methodExecutionBySignalingWaithandleObject()
        {
            //The method's signature must match that defined by 
            //System.Threading.WaitOrTimerCallback delegate
            //Using the static ThreadPool.RegisterWaitForSingleObject method
            //register the method to execute and the WaitHandle object that
            //will trigger execution when signaled.

            // Create the new AutoResetEvent in an unsignaled state.
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            // Create the state object that is passed to the event handler
            // method when it is triggered. In this case, a message to display.
            string state = "AutoResetEvent signaled.";
            // Register the EventHandler method to wait for the AutoResetEvent to
            // be signaled. Set a timeout of 10 seconds, and configure the wait
            // operation to reset after activation (last argument).

            RegisteredWaitHandle handle = ThreadPool.RegisterWaitForSingleObject(
                            autoEvent, EventHandler, state, 10000, false);

            Console.WriteLine("Press ENTER to signal the AutoResetEvent" +
                " or enter \"Cancel\" to unregister the wait operation.");

            while (Console.ReadLine().ToUpper() != "CANCEL")
            {
                // If "Cancel" has not been entered into the console, signal
                // the AutoResetEvent, which will cause the EventHandler
                // method to execute. The AutoResetEvent will automatically
                // revert to an unsignaled state.
                autoEvent.Set();
            }
            // Unregister the wait operation.
            Console.WriteLine("Unregistering wait operation.");
            handle.Unregister(null);
            // Wait to continue.
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }


        #endregion

        #region tasks
        public void UsingTask()
        {
            //Wait is equivalent to Join on a thread
            //It waits till the Task is finished before exiting the application
            Task t = Task.Run(() =>
            {
                for (int x = 0; x < 100; x++)
                { Console.Write("*.."); }
            });
            t.Wait();
        }

        //The .NET Framework provides a simple thread-pool implementation 
        //accessible through the members of the ThreadPool static class. 
        //The QueueUserWorkItem method allows you to execute a method 
        //using a thread-pool thread by placing a work item on a queue
        public void taskInaThreadPool()
        {
            //Declare a method containing the code you want to execute. 
            //The method's signature must match that defined by 
            //the System.Threading.WaitCallback delegate
            //Call the static method QueueUserWorkItem 
            //of the System.Threading.ThreadPool class, passing it your method name
            //The runtime will queue your method and execute it 
            //when a thread-pool thread becomes available.

            // Execute DisplayMessage using the thread pool and no arguments.
            ThreadPool.QueueUserWorkItem(DisplayMessage);
            // Create a MessageInfo object to pass to the DisplayMessage method.
            MessageInfo info = new MessageInfo(5, "A thread pool example with arguments.");
            // Set the max number of threads.
            ThreadPool.SetMaxThreads(2, 2);
            // Execute DisplayMessage using the thread pool 
            // and providing an argument.
            ThreadPool.QueueUserWorkItem(DisplayMessage, info);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void executeMethodAsynchronously()
        {
            //Declare a delegate with the same signature as the method
            //Create an instance of the delegate that references the method
            //Call the BeginInvoke method of the delegate instance 
            //to start executing your method
            //Use the EndInvoke method to determine the method's status
            //as well as obtain the method's return value if complete.

            // Demonstrate the various approaches to asynchronous method completion.
            BlockingExample();
            PollingExample();
            WaitingExample();
            WaitAllExample();
            CallbackExample();
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void usingTaskReturningValue(int entero)
        {
            //Task<T> class is used if a Task should return a value. 
            Task<int> t = Task.Run(() =>
            { return entero * 2; });
            Console.WriteLine(t.Result); // Displays entero*2
            //Attempting to read the Result property on a Task will force the thread to wait until the Task is finished
        }

        public void usingTaskContinuation(int entero)
        {
            Task<int> t = Task.Run(() =>
            {
                return entero;
            }).ContinueWith((i) =>
            { return i.Result * 2; });
            Console.WriteLine(t.Result);
        }

        public void differentContinuationTasks(int entero)
        {
            Task<int> t = Task.Run(() =>
            { return entero; });
            t.ContinueWith((i) =>
            { Console.WriteLine("Canceled"); }, TaskContinuationOptions.OnlyOnCanceled);
            t.ContinueWith((i) =>
            { Console.WriteLine("Faulted"); }, TaskContinuationOptions.OnlyOnFaulted);
            var completedTask = t.ContinueWith((i) =>
            { Console.WriteLine("Completed"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            completedTask.Wait();
        }

        public void childTasks()
        {
            //The finalTask runs only after the parent Task is finished
            //the parent Task finishes when all three children are finished
            //You can use this to create Task hierarchies that will go through all the steps specified
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];
                new Task(() => results[0] = 0,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = 1,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = 2,
                    TaskCreationOptions.AttachedToParent).Start();
                return results;
            });

            var finalTask = parent.ContinueWith(
               parentTask =>
               {
                   foreach (int i in parentTask.Result)
                       Console.WriteLine(i);
               });
            finalTask.Wait();
        }

        public void usingTaskFactory()
        {
            //A TaskFactory is created with a certain configuration and can then be used to create Tasks with that configuration
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                   TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);
                return results;
            });
            var finalTask = parent.ContinueWith(
               parentTask =>
               {
                   foreach (int i in parentTask.Result)
                       Console.WriteLine(i);
               });
            finalTask.Wait();
        }

        public void waitForMultipleTasks()
        {
            Task[] tasks = new Task[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("This is the task 1");
                return 1;
            });
            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("This is the task 2");
                return 2;
            });
            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("This is the task 3");
                return 3;
            }
                                      );
            //WaitAll to wait for multiple Tasks to finish before continuing execution
            Task.WaitAll(tasks);
            //also have WhenAll method to schedule a continuation method after all Tasks have finished
        }

        public void waitanyMultipleTasks()
        {
            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() => { Thread.Sleep(2000); return 1; });
            tasks[1] = Task.Run(() => { Thread.Sleep(1000); return 2; });
            tasks[2] = Task.Run(() => { Thread.Sleep(3000); return 3; });

            while (tasks.Length > 0)
            {
                //Instead of waiting until all tasks are finished, wait until one of the tasks is finished
                int i = Task.WaitAny(tasks);
                Task<int> completedTask = tasks[i];

                Console.WriteLine(completedTask.Result);

                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
            }

        }

        public void continuationForCancelledTask()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            //add a continuation Task that executes only when the Task is canceled
            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                throw new OperationCanceledException();

            }, token).ContinueWith((t) =>
            {
                t.Exception.Handle((e) => true);
                Console.WriteLine("You have canceled the task");
            }, TaskContinuationOptions.OnlyOnCanceled);

        }

        public void taskTimeout()
        {
            Task longRunning = Task.Run(() =>
            {
                Thread.Sleep(10000);
            });

            int index = Task.WaitAny(new[] { longRunning }, 1000);

            if (index == -1)
                Console.WriteLine("Task timed out");
        }



        #endregion

        #region concurrentCollections

        public void usingBlockingCollection()
        {
            //The program terminates when the user doesn’t enter any data. 
            //Until that, every string entered is added by the write Task and removed by the read Task
            //use the CompleteAdding method to signal BlockingCollection that no more items will be added
            //If other threads are waiting for new items, they won’t be blocked anymore
            BlockingCollection<string> col = new BlockingCollection<string>();
            Task read = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(col.Take());
                }
            });

            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s)) break;
                    col.Add(s);
                }
            });

            write.Wait();
        }
        
        public void BlockingCollectionWithGetConsumingEnum()
        {
            //By using the GetConsumingEnumerable method, you get an IEnumerable that blocks until it finds a new item
            //That way, you can use a foreach with your BlockingCollection to enumerate it 
            BlockingCollection<string> col = new BlockingCollection<string>();
            Task read = Task.Run(() =>
            {

                foreach (string v in col.GetConsumingEnumerable())
                    Console.WriteLine(v);
            });

            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s)) break;
                    col.Add(s);
                }
            });
            write.Wait();

        }

        public void usingConcurrentBag()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            bag.Add(42);
            bag.Add(21);

            int result;
            if (bag.TryTake(out result))
                Console.WriteLine(result);

            if (bag.TryPeek(out result))
                Console.WriteLine("There is a next item: {0}", result);
        }
        
        public void concurrentBagEnumerate()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Task.Run(() =>
            {
                bag.Add(42);
                Thread.Sleep(1000);
                bag.Add(21);
            });

            Task.Run(() =>
            {
                foreach (int i in bag)
                    Console.WriteLine(i);
            }).Wait();

            // Displays
            // 42
        }

        public void usingConcurrentStack()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            stack.Push(42);

            int result;
            if (stack.TryPop(out result))
                Console.WriteLine("Popped: {0}", result);
            //add and remove multiple items at once by using PushRange and TryPopRange
            stack.PushRange(new int[] { 1, 2, 3 });

            int[] values = new int[2];
            stack.TryPopRange(values);

            foreach (int i in values)
                Console.WriteLine(i);

            // Popped: 42
            // 3
            // 2
        }

        public void usingConcurrentQueue()
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
            queue.Enqueue(42);

            int result;
            if (queue.TryDequeue(out result))
                Console.WriteLine("Dequeued: {0}", result);

            // Dequeued: 42
        }

        public void usingConcurrentDictionary()
        {
            var dict = new ConcurrentDictionary<string, int>();
            if (dict.TryAdd("k1", 42))
            {
                Console.WriteLine("Added");
            }

            //TryUpdate checks to see whether the current value is equal to the existing value before updating it
            //AddOrUpdate makes sure an item is added if it’s not there, and updated to a new value if it is
            //GetOrAdd gets the current value of an item if it’s available; if not, it adds the new value by using a factory method
            if (dict.TryUpdate("k1", 21, 42))
            {
                Console.WriteLine("42 updated to 21");
            }

            dict["k1"] = 42; // Overwrite unconditionally

            int r1 = dict.AddOrUpdate("k1", 3, (s, i) => i * 2);
            int r2 = dict.GetOrAdd("k2", 3);

        }

        #endregion

        #region parallelQuery

        public void AsyncyAwait()
        {
            string result = DownloadContent().Result;
            Console.WriteLine(result);
        }

        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                //The GetStringAsync uses asynchronous code internally and returns a Task<string> to the caller that will finish when the data is retrieved
                //In the meantime, your thread can do other work.
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }
        }

        public void unorderedParallelQuery()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel()
                .Where(i => i % 2 == 0)
                .ToArray();

            foreach (int i in parallelResult)
                Console.WriteLine(i);
        }

        public void orderedParallelQuery()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().AsOrdered()
                .Where(i => i % 2 == 0)
                .ToArray();

            foreach (int i in parallelResult)
                Console.WriteLine(i);

        }

        public void makeParallelSequential()
        {
            var numbers = Enumerable.Range(0, 20);

            var parallelResult = numbers.AsParallel().AsOrdered()
                .Where(i => i % 2 == 0).AsSequential();

            foreach (int i in parallelResult.Take(5))
                Console.WriteLine(i);
        }

        public void usingForAll()
        {
            //use the ForAll operator to iterate over a collection when the iteration can also be done in a parallel way
            var numbers = Enumerable.Range(0, 20);
            var parallelResult = numbers.AsParallel()
                .Where(i => i % 2 == 0);
            parallelResult.ForAll(e => Console.WriteLine(e));

        }

        public void catchingAggregateException()
        {
            var numbers = Enumerable.Range(0, 20);
            try
            {
                var parallelResult = numbers.AsParallel()
                     .Where(i => IsEven(i));
                parallelResult.ForAll(e => Console.WriteLine(e));
            }
            catch (AggregateException e)
            {
                Console.WriteLine("There where {0} exceptions",
                                    e.InnerExceptions.Count);
            }
        }



        #endregion

        #region synchronization

        public void usingLock()
        {
            int n = 0;
            object _lock = new object();

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    lock (_lock)
                        n++;
            });

            for (int i = 0; i < 1000000; i++)
                lock (_lock)
                    n--;

            up.Wait();
            Console.WriteLine(n);
            //the program always outputs 0 because access to the variable n is now synchronized
            //There is no way that one thread could change the value while the other thread is working with it.
        }

        public void usingInterLocked()
        {
            //Interlocked guarantees that the increment and decrement operations are executed atomically
            int n = 0;

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n);
            });

            for (int i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);


            up.Wait();
            Console.WriteLine(n);

        }

        public void usingCancellationToken()
        {
            //pass a CancellationToken to a Task, which periodically monitors the token 
            //to see whether cancellation is requested
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {

                    Console.Write("*");
                    Thread.Sleep(1000);
                }

            }, token);

            //The CancellationToken is used in the asynchronous Task. 
            //The CancellationTokenSource is used to signal that the Task should cancel itself.
            //In this case, the operation will just end when cancellation is requested.
            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();

            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();

        }

        public void usingOperationCanceledException()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }

                token.ThrowIfCancellationRequested();

            }, token);

            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();

                cancellationTokenSource.Cancel();
                task.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions[0].Message);
            }
            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();

        }               

        public void monitorForSynchronize()
        {
            TraceMsg2("Starting worker threads.");
            // Add an initial work item to the work queue.
            lock (workQueue)
            {
                workQueue.Enqueue("Work Item 1");
            }
            // Create and start three new worker threads running the
            // ProcessWorkItems method.
            for (int count = 0; count < 3; count++)
            {
                (new Thread(ProcessWorkItems)).Start();
            }
            Thread.Sleep(1500);
            // The first time the user presses Enter, add a work item and
            // activate a single thread to process it.
            TraceMsg2("Press Enter to pulse one waiting thread.");
            Console.ReadLine();
            // Acquire a lock on the workQueue object.
            lock (workQueue)
            {
                // Add a work item.
                workQueue.Enqueue("Work Item 2.");

                // Pulse one waiting thread.
                Monitor.Pulse(workQueue);
            }
            Thread.Sleep(2000);
            // The second time the user presses Enter, add three work items and
            // activate three threads to process them.
            TraceMsg2("Press Enter to pulse three waiting threads.");
            Console.ReadLine();
            // Acquire a lock on the workQueue object.
            lock (workQueue)
            {
                // Add work items to the work queue, and activate worker threads.
                workQueue.Enqueue("Work Item 3.");
                Monitor.Pulse(workQueue);
                workQueue.Enqueue("Work Item 4.");
                Monitor.Pulse(workQueue);
                workQueue.Enqueue("Work Item 5.");
                Monitor.Pulse(workQueue);
            }
            Thread.Sleep(3500);
            // The third time the user presses Enter, signal the worker threads
            // to terminate and activate them all.
            TraceMsg2("Press Enter to pulse all waiting threads.");
            Console.ReadLine();
            // Acquire a lock on the workQueue object.
            lock (workQueue)
            {
                // Signal that threads should terminate.
                processWorkItems = false;
                // Pulse all waiting threads.
                Monitor.PulseAll(workQueue);
            }
            Thread.Sleep(1000);
            // Wait to continue.
            TraceMsg2("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void synchronizeMultipleThreadsWithAnEvent()
        {
            // Create a new EventWaitHandle with an initial signaled state, in
            // manual mode, with the name "EventExample".
            using (EventWaitHandle eventWaitHandle =
                new EventWaitHandle(true, EventResetMode.ManualReset,
                "EventExample"))
            {
                // Create and start a new thread running the DisplayMesssage method.
                TraceMsg3("Starting DisplayMessageThread.");
                Thread trd = new Thread(DisplayMessage3);
                trd.Start();
                // Allow the EventWaitHandle to be toggled between a signaled and
                // unsignaled state up to three times before ending.
                for (int count = 0; count < 3; count++)
                {
                    // Wait for Enter to be pressed.
                    Console.ReadLine();
                    // You need to toggle the event. The only way to know the
                    // current state is to wait on it with a 0 (zero) timeout
                    // and test the result.
                    if (eventWaitHandle.WaitOne(0, true))
                    {
                        TraceMsg3("Switching Event To UnSignaled State.");
                        // Event is signaled, so unsignal it.
                        eventWaitHandle.Reset();
                    }
                    else
                    {
                        TraceMsg3("Switching Event To Signaled State.");
                        // Event is unsignaled, so signal it.
                        eventWaitHandle.Set();
                    }
                }
                // Terminate the DisplayMessage thread, and wait for it to
                // complete before disposing of the EventWaitHandle.
                terminate = true;
                eventWaitHandle.Set();
                trd.Join(5000);
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void synchronizationWithMutex()
        {
            // Create a new Mutex with the name "MutexExample".
            using (Mutex mutex = new Mutex(false, "MutexExample"))
            {
                TraceMsg4("Starting threads -- press Enter to terminate.");
                // Create and start three new threads running the
                // DisplayMesssage method.
                Thread trd1 = new Thread(DisplayMessage4);
                Thread trd2 = new Thread(DisplayMessage4);
                Thread trd3 = new Thread(DisplayMessage4);
                trd1.Start();
                trd2.Start();
                trd3.Start();
                // Wait for Enter to be pressed.
                Console.ReadLine();
                // Terminate the DisplayMessage threads, and wait for them to
                // complete before disposing of the Mutex.
                terminate = true;
                trd1.Join(5000);
                trd2.Join(5000);
                trd3.Join(5000);
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void synchronizationWithSemaphore()
        {
            // Create a new Semaphore with the name "SemaphoreExample". The
            // Semaphore can be owned by up to two threads at the same time.
            using (Semaphore sem = new Semaphore(2, 2, "SemaphoreExample"))
            {
                TraceMsg5("Starting threads -- press Enter to terminate.");

                // Create and start three new threads running the
                // DisplayMesssage method.
                Thread trd1 = new Thread(DisplayMessage5);
                Thread trd2 = new Thread(DisplayMessage5);
                Thread trd3 = new Thread(DisplayMessage5);
                trd1.Start();
                trd2.Start();
                trd3.Start();
                // Wait for Enter to be pressed.
                Console.ReadLine();
                // Terminate the DisplayMessage threads and wait for them to
                // complete before disposing of the Semaphore.
                terminate5 = true;
                trd1.Join(5000);
                trd2.Join(5000);
                trd3.Join(5000);
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();

        }

        public void synchronizeAccessToSharedDataValue()
        {
            int firstInt = 2500;
            int secondInt = 8000;

            Console.WriteLine("firstInt initial value = {0}", firstInt);
            Console.WriteLine("secondInt initial value = {0}", secondInt);
            // Decrement firstInt in a thread-safe manner.
            // The thread-safe equivalent of firstInt = firstInt - 1.
            Interlocked.Decrement(ref firstInt);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("firstInt after decrement = {0}", firstInt);
            // Increment secondInt in a thread-safe manner.
            // The thread-safe equivalent of secondInt = secondInt + 1.
            Interlocked.Increment(ref secondInt);
            Console.WriteLine("secondInt after increment = {0}", secondInt);
            // Add the firstInt and secondInt values, and store the result in firstInt.
            // The thread-safe equivalent of firstInt = firstInt + secondInt.
            Interlocked.Add(ref firstInt, secondInt);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("firstInt after Add = {0}", firstInt);
            Console.WriteLine("secondInt after Add = {0}", secondInt);
            // Exchange the value of firstInt with secondInt.
            // The thread-safe equivalenet of secondInt = firstInt.
            Interlocked.Exchange(ref secondInt, firstInt);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("firstInt after Exchange = {0}", firstInt);
            Console.WriteLine("secondInt after Exchange = {0}", secondInt);
            // Compare firstInt with secondInt, and if they are equal, set
            // firstInt to 5000.
            // The thread-safe equivalenet of:
            //     if (firstInt == secondInt) firstInt = 5000.
            Interlocked.CompareExchange(ref firstInt, 5000, secondInt);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("firstInt after CompareExchange = {0}", firstInt);
            Console.WriteLine("secondInt after CompareExchange = {0}", secondInt);
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        #endregion

        public static bool IsEven(int i)
        {
            if (i % 10 == 0) throw new ArgumentException("i");
            return i % 2 == 0;
        }


    }
}
