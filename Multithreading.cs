using System;
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

        #endregion
                             
        public static bool IsEven(int i)
        {
            if (i % 10 == 0) throw new ArgumentException("i");
            return i % 2 == 0;
        }


    }
}
