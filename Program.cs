using System;

namespace LearningByExample1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Multithreading mth = new Multithreading();
            ProgramFlow flw = new ProgramFlow();
            EventsCallbacks ecb = new EventsCallbacks();
            ExceptionHandling exh = new ExceptionHandling();
            StringManagement strm = new StringManagement();

            /* PROGRAM FLOW */
            //flw.complexIFstatement('e');
            //flw.CheckWithSwitch('y');
            //flw.switchWithGoto();
            //flw.basicFor();
            //flw.loopWithMultipleVariables();
            //flw.breakAndContinue();
            /* THREADS */
            //mth.TwoThreads();
            //mth.ThreadWithMethodParam();
            //mth.HowToStopThread();
            //mth.UseOfThreadStatic();
            //mth.UsingThreadLocalData();
            /* TASKS */
            //mth.usingThreadPool();
            //mth.UsingTask();
            //mth.usingTaskReturningValue(21);
            //mth.usingTaskContinuation(21);
            //mth.differentContinuationTasks(456);
            //mth.childTasks();
            //mth.usingTaskFactory();
            //mth.waitForMultipleTasks();
            //mth.waitanyMultipleTasks();
            //mth.continuationForCancelledTask();
            //mth.taskTimeout();
            /* PLINQ */
            //mth.AsyncyAwait();
            //mth.unorderedParallelQuery();
            //mth.orderedParallelQuery();
            //mth.makeParallelSequential();
            //mth.usingForAll();
            //mth.catchingAggregateException();
            /* CONCURRENT COLLECTIONS */
            //mth.usingBlockingCollection();
            //mth.BlockingCollectionWithGetConsumingEnum();
            //mth.usingConcurrentBag();
            //mth.concurrentBagEnumerate();
            //mth.usingConcurrentStack();
            //mth.usingConcurrentQueue();
            //mth.usingConcurrentDictionary();
            /* SYNCHRONIZATION */
            //mth.usingLock();
            //mth.usingInterLocked();
            //mth.usingCancellationToken();
            //mth.usingOperationCanceledException();
            /* EXCEPTION HANDLING */
            //exh.catchingFormatException();
            //exh.usingEnvironmentFailfast();
            //exh.inspectingException();
            //exh.UsingExceptionDispatchInfo();
            /* EVENT MANAGEMENT */
            //ecb.UseDelegate();
            //ecb.multicastDelegate();
            //ecb.usingActionDelegate();

            #region EventHandling

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

            #endregion EventHandling

            //strm.createLotOfStrings();
            //strm.usingStringBuilder();
            //strm.StringWriterAsOutputForXMLwriter();
            strm.usingStrings();
            //strm.ChangeStringWithRegex();


            Console.ReadLine();
        }
    }
}