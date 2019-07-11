using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Program
    {

        static void Main(string[] args)
        {
            Multithreading mth = new Multithreading();
            ProgramFlow flw = new ProgramFlow();
            EventsCallbacks ecb = new EventsCallbacks();
            ExceptionHandling exh = new ExceptionHandling();

            //mostrarMenu();

            void mostrarMenu()
            {
            Console.WriteLine("MENU");
            Console.WriteLine("I. THREADS");
            Console.WriteLine("  1.TwoThreads");
            Console.WriteLine("  2.ThreadWithMethodParam");
            Console.WriteLine("  3.HowToStopThread");
            Console.WriteLine("  4.UseOfThreadStatic");
            Console.WriteLine("  5.UsingThreadLocalData");
            Console.WriteLine("II. TASKS");
            Console.WriteLine("  6.usingThreadPool");
            Console.WriteLine("  7.UsingTask");
            Console.WriteLine("  8.usingTaskReturningValue");
            Console.WriteLine("  9.usingTaskContinuation");
            Console.WriteLine("  10.differentContinuationTasks");
            Console.WriteLine("  11.childTasks");
            Console.WriteLine("  12.usingTaskFactory");
            Console.WriteLine("  13.waitForMultipleTasks");
            Console.WriteLine("  14.waitanyMultipleTasks");
            Console.WriteLine("  15.continuationForCancelledTask");
            Console.WriteLine("  16.taskTimeout");


                Console.WriteLine("III. PLINQ");
            Console.WriteLine("  17.AsyncyAwait");
            Console.WriteLine("  18.unorderedParallelQuery");
            Console.WriteLine("  19.orderedParallelQuery");
            Console.WriteLine("  20.makeParallelSequential");
            Console.WriteLine("  21.usingForAll");
            Console.WriteLine("  22.catchingAggregateException");

            //mth.AsyncyAwait();
            //mth.unorderedParallelQuery();
            //mth.orderedParallelQuery();
            //mth.makeParallelSequential();
            //mth.usingForAll();
            //mth.catchingAggregateException();


            Console.WriteLine("IV. CONCURRENT COLLECTIONS");
            Console.WriteLine("  23.usingBlockingCollection");
            Console.WriteLine("  24.BlockingCollectionWithGetConsumingEnum");
            Console.WriteLine("  25.usingConcurrentBag");
            Console.WriteLine("  26.concurrentBagEnumerate");
            Console.WriteLine("  27.usingConcurrentStack");
            Console.WriteLine("  28.usingConcurrentQueue");
            Console.WriteLine("  29.usingConcurrentDictionary");

            /* CONCURRENT COLLECTIONS */
            //mth.usingBlockingCollection();
            //mth.BlockingCollectionWithGetConsumingEnum();
            //mth.usingConcurrentBag();
            //mth.concurrentBagEnumerate();
            //mth.usingConcurrentStack();
            //mth.usingConcurrentQueue();
            //mth.usingConcurrentDictionary();

            Console.WriteLine("V. SYNCHRONIZATION");
            Console.WriteLine("  30.usingLock");
            Console.WriteLine("  31.usingInterLocked");
            Console.WriteLine("  32.usingCancellationToken");
            Console.WriteLine("  33.usingOperationCanceledException");

            /* SYNCHRONIZATION */
            //mth.usingLock();
            //mth.usingInterLocked();
            //mth.usingCancellationToken();
            //mth.usingOperationCanceledException();

            Console.WriteLine("VI. PROGRAM FLOW");
            Console.WriteLine("  34.complexIFstatement");
            Console.WriteLine("  35.CheckWithSwitch");
            Console.WriteLine("  36.switchWithGoto");
            Console.WriteLine("  37.basicFor");
            Console.WriteLine("  38.loopWithMultipleVariables");
            Console.WriteLine("  39.breakAndContinue");

                /* PROGRAM FLOW */
                //flw.complexIFstatement('e');
                //flw.CheckWithSwitch('y');
                //flw.switchWithGoto();
                //flw.basicFor();
                //flw.loopWithMultipleVariables();
                //flw.breakAndContinue();

            }

            /* THREADS */
            void MenuThreads(string input)
            {
                switch (input)
                {
                    case "1":
                        { mth.TwoThreads(); break;}
                    case "2":
                        { mth.ThreadWithMethodParam(); break;}
                    case "3":
                        { mth.HowToStopThread(); break;}
                    case "4":
                        { mth.UseOfThreadStatic(); break;}
                    case "5":
                        { mth.UsingThreadLocalData(); break;}
                    default:
                        { Console.WriteLine("Input incorrect"); break;}
                }
            }
            /* TASKS */
            void MenuTasks(string input)
            {
                switch (input)
                {
                    case "6":
                        { mth.usingThreadPool(); break; }
                    case "7":
                        { mth.UsingTask(); break; }
                    case "8":
                        { mth.usingTaskReturningValue(21); break; }
                    case "9":
                        { mth.usingTaskContinuation(21); break; }
                    case "10":
                        { mth.differentContinuationTasks(456); break; }
                    case "11":
                        { mth.childTasks(); break; }
                    case "12":
                        { mth.usingTaskFactory(); break; }
                    case "13":
                        { mth.waitForMultipleTasks(); break; }
                    case "14":
                        { mth.waitanyMultipleTasks(); break; }
                    case "15":
                        { mth.continuationForCancelledTask(); break; }
                    case "16":
                        { mth.taskTimeout(); break; }
                    default:
                        { Console.WriteLine("Input incorrect"); break; }
                }
            }



            //ecb.UseDelegate();
            //ecb.multicastDelegate();
            //ecb.usingActionDelegate();

            //Subscription to the delegate OnChange
            //two anonymous methods that write a text on the console
            ecb.OnChange += () => Console.WriteLine("Event raised to method 1");
            ecb.OnChange += () => Console.WriteLine("Event raised to method 2");
            //call to the method that contains OnChange() executing the two methods
            //ecb.Raise();

            ecb.OnChange2 += (sender, e)
                    => Console.WriteLine("Event raised: {0}", e.Value);

            ecb.OnChange2 += (sender, e)
                => Console.WriteLine("Subscriber 1 called");
            //ecb.OnChange2 += (sender, e) => { throw new Exception(); };

            ecb.OnChange2 += (sender, e)
                => Console.WriteLine("Subscriber 3 called");

            //ecb.Raise2();

            //exh.catchingFormatException();
            //exh.usingEnvironmentFailfast();
            //exh.inspectingException();
            //exh.UsingExceptionDispatchInfo();

            Console.ReadLine();

        }




    }
}
