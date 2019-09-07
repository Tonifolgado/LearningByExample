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
            Collections clt = new Collections();
            EventsCallbacks ecb = new EventsCallbacks();
            ExceptionHandling exh = new ExceptionHandling();
            StringManagement strm = new StringManagement();
            Conversion cnv = new Conversion();
            Encryption cryp = new Encryption();
            LoggingAndTracing lgtr = new LoggingAndTracing();
            IOoperations iop = new IOoperations();
            StreamManagement str = new StreamManagement();
            Serialization ser = new Serialization();
            CreateAndUseTypes typ = new CreateAndUseTypes(25);
            Generics gnr = new Generics();
            DataManagement dtm = new DataManagement();
            Reflection rfx = new Reflection();

            /* PROGRAM FLOW */
            //flw.complexIFstatement('e');
            //flw.CheckWithSwitch('y');
            //flw.switchWithGoto();
            //flw.basicFor();
            //flw.anotherBasicFor();
            //flw.loopWithMultipleVariables();
            //flw.breakAndContinue();
            //flw.ifStatements();
            //flw.beyondBasicIfStatements();
            //flw.multipleConditionswithSwitch();
            //flw.lotteryProgram();
            //flw.WhileExample();
            //flw.simpleDoWhile();
            //flw.workingWithLoops();
            //flw.manipulateConsoleAppearance();

            /* COLLECTIONS */
            //clt.Arrays();
            //clt.twodimensionalArray();
            //clt.listOfStrings();
            //clt.Dictionaries();
            //clt.UseHashSet();
            //clt.Queues();
            //clt.Stacks();
            //clt.foreachLoop();
            //clt.sortArrayAndLists();
            //clt.copyColletionToArray();
            //clt.selectCollectionElements();
            //clt.removeDuplicateFromCollection();

            /* THREADS */
            //mth.TwoThreads();
            //mth.ThreadWithMethodParam();
            //mth.HowToStopThread();
            //mth.UseOfThreadStatic();
            //mth.UsingThreadLocalData();
            //mth.executeMethodAsynchronously();
            //mth.periodicalMethodExecution();
            //mth.methodExecutionAtaSpecificTime();

            /* TASKS */
            //mth.usingThreadPool();
            //mth.UsingTask();
            //mth.taskInaThreadPool();
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
            //ecb.handleEventWithAnonymousFunction();

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
            //cnv.parsingDateTime();
            //cnv.parsingNumbers();
            //cnv.dateTimeFromStrings();

            /* STRINGS AND XML */
            //strm.createLotOfStrings();
            //strm.usingStringBuilder();
            //strm.StringWriterAsOutputForXMLwriter();
            //strm.usingStrings();
            //strm.ChangeStringWithRegex();
            //strm.formattingStrings();
            //strm.ValidateZipCode("08820");
            //strm.ValidateZipCodeRegEx("08820");
            //strm.RegexWithMultipleSpaces();

            // Test the input from the command line. The first argument is the
            // regular expression, and the second is the input.
            //string regexp = @"^[\w -] +@([\w -] +\.)+[\w -] +$";
            //string input = @"myname @mydomain.com";
            //Console.WriteLine("Regular Expression: {0}", regexp);
            //Console.WriteLine("Input: {0}", input);
            //Console.WriteLine("Valid = {0}", strm.ValidateInput(regexp, input));
            //Console.WriteLine("Valid = {0}", strm.ValidateInput2(regexp, input));
            //Console.WriteLine("\nMain method complete. Press Enter");

            //strm.usingXMLreader();
            //strm.usingXMLwriter();
            //strm.usingXmlDocument();
            //strm.xPathQuery();
            //strm.usingLINQtoXML();
            //strm.usingXElement();
            //strm.updatingXML();
            //strm.functionalCreationToUpdateXML();
            //Console.WriteLine(strm.ReverseString("Madam Im Adam"));
            //Console.WriteLine(strm.ReverseString("The quick brown fox jumped over the lazy dog."));            

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
            //iop.readUserInputfromConsole();

            /* STREAM MANAGEMENT */
            //str.fileStreamCreation();
            //str.srWriterCreation();
            //str.openFileStream();
            //str.compressingData();
            //str.usingBufferedStream();
            //str.characterEncoding();

            /* CONVERT Basic Value Types to Byte Arrays */
            //byte[] b = null;
            // Convert a bool to a byte array and display.
            //b = BitConverter.GetBytes(true);
            //Console.WriteLine(BitConverter.ToString(b));
            // Convert a byte array to a bool and display.
            //Console.WriteLine(BitConverter.ToBoolean(b, 0));
            // Convert an int to a byte array and display.
            //b = BitConverter.GetBytes(3678);
            //Console.WriteLine(BitConverter.ToString(b));
            // Convert a byte array to an int and display.
            //Console.WriteLine(BitConverter.ToInt32(b, 0));
            // Convert a decimal to a byte array and display.
            //b = str.DecimalToByteArray(285998345545.563846696m);
            //Console.WriteLine(BitConverter.ToString(b));
            // Convert a byte array to a decimal and display.
            //Console.WriteLine(str.ByteArrayToDecimal(b));
            // Wait to continue.
            //Console.WriteLine("Main method complete. Press Enter");


            /* SERIALIZATION */
            //ser.serializeWithXMLserializer();
            //ser.serializeDerivedClass();
            //ser.binarySerialization();
            //ser.usingDataContractSerializer();
            //ser.usingDataContractJsonSerializer();
            //ser.serializeDeserializeExample();
            //ser.JSONserialization();
            //ser.loadAssemblyIntoCurrentAppDomain();//no funciona

            /* CREATE AND USE TYPES */
            //typ.valueTypesAndAlias();
            //typ.usingValueTypes();
            Book bk = new Book();
            //bk.manageBooks();
            //typ.usingEnums();
            //typ.manageStudents();
            //typ.passingValueTypesToMethods();
            Student std = new Student("Pere","Puig");
            //std.displayDetails();
            //typ.anonymousType();
            //typ.usingExpandoObject();
            //typ.overloadOperator();
            //typ.conversionOperators();
            //typ.customIndexer();

            /* GENERICS */
            //gnr.usingGenericMethod();
            //gnr.stronglyTypedCollection();

            /* DATES */
            //dtm.dateTimeManagement();
            //dtm.largeIntegerValuesCalculate();

            /* REFLECTION */
            //rfx.retrieveTypeInformation();
            //rfx.testObjectType();
            //Reflection.CreateStringBuilder();
            //FakeClass.programAttributesInspection();
            //FakeClass.extractTypeMembers();
            //FakeClass.invokeTypeMemberUsingReflection();
            //FakeClass.invokeTypeMemberDinamically();
            //FakeClass.customDynamicTypeManagement();

            Console.ReadLine();
        }
    }
}