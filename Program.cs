using System;
using System.IO;
using System.Security;

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
            Conversion cnv = new Conversion();
            Encryption cryp = new Encryption();
            LoggingAndTracing lgtr = new LoggingAndTracing();
            IOoperations iop = new IOoperations();
            StreamManagement str = new StreamManagement();

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

            Person p = new Person("John", "Doe");
            //Console.WriteLine(p);
            // Displays 'John Doe' due to the overriding of ToString with default FL format
            // use ToString to display first name and last name in format LSF
            //Console.WriteLine(p.ToString("LSF"));
            //cnv.differentConversions();

            /* STRINGS */
            //strm.createLotOfStrings();
            //strm.usingStringBuilder();
            //strm.StringWriterAsOutputForXMLwriter();
            //strm.usingStrings();
            //strm.ChangeStringWithRegex();
            //strm.formattingStrings();
            //strm.ValidateZipCode("08820");
            //strm.ValidateZipCodeRegEx("08820");
            //strm.RegexWithMultipleSpaces();

            /* ENCRYPTION */
            //cryp.SymmetricEncryption();
            //cryp.AsymmetricEncryption();
            //cryp.HashCodeCalculation();
            //cryp.SecureStringInitialize();
            SecureString ss = new SecureString();
            //cryp.ConvertToUnsecureString(ss);

            /* LOGGING AND TRACING */
            //lgtr.usingDebug();
            //lgtr.usingTracesource();
            //lgtr.TraceListenerConfig();
            //lgtr.EventLogWriting();
            //lgtr.EventLogReading();
            //lgtr.EventLogSubscriptionToChanges();
            //lgtr.usingStopWatch();
            //lgtr.PerformanceCounterReading();
            //lgtr.PerformanceCounterWriting();

            /* IO & STREAM MANAGEMENT */
            //iop.driveInformation();
            //iop.directoryManagement();
            //iop.deleteExistingDir();
            // List the subdirectories for Program Files containing the character 'a' with maximum depth of 5
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Program Files");
            //iop.ListDirectories(directoryInfo, "*a*", 5, 0);
            //iop.moveDirectory();
            //iop.listDirectoryFiles();
            //iop.deleteFile();
            //iop.movingFile();
            //iop.pathCreation();
            //iop.ReadAllText()
            //iop.webRequestExecution();
            //iop.createAndWriteAsyncToFile();
            //iop.readAsyncHttpRequest();
            //iop.executeMultipleRequests();
            //iop.executeMultipleRequestsInParallel();

            //str.fileStreamCreation();
            //str.srWriterCreation();
            //str.openFileStream();
            //str.compressingData();
            //str.usingBufferedStream();
            


            Console.ReadLine();
        }
    }
}