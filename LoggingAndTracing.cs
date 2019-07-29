using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class LoggingAndTracing
    {
        const int numberOfIterations = 100000;

        public void usingDebug()
        {
            //This info is visible from the Ouput window in Debug mode
            Debug.WriteLine("Starting application");
            Debug.Indent();
            int i = 1 + 2;
            //If the Debug.Assert statement fails, you get a message box showing the current stack trace
            //This message box asks you to retry, abort, or ignore the assertion failure
            Debug.Assert(i == 3);
            Debug.WriteLineIf(i > 0, "i is greater than 0");
        }

        public void usingTracesource()
        {
            TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

            traceSource.TraceInformation("Tracing application..");
            //you can pass a parameter TraceEventType to specify the severity of the event
            //The second argument to the trace methods is the event ID number
            //The third parameter is a string with the message that should be traced
            traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
            traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });

            traceSource.Flush();
            traceSource.Close();

            // Outputs (to the output Window by default):
            // myTraceSource Information: 0 : Tracing application..
            // myTraceSource Critical: 0 : Critical trace
            // myTraceSource Information: 1 : a, b, c
        }

        public void TraceListenerConfig()
        {
            Stream outputFile = File.Create("tracefile.txt");
            TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);

            TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

            //If you don’t want the DefaultTraceListener to be active,
            //you need to clear the current listeners collection
            traceSource.Listeners.Clear();
            //You can add as many listeners as you want
            traceSource.Listeners.Add(textListener);

            traceSource.TraceInformation("Trace output");

            traceSource.Flush();
            traceSource.Close();
        }

        public void EventLogWriting()
        {
            //To use the EventLog class, you need to run with an account with premissions
            //When running it from Visual Studio, you have to run it as an administrator
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Please restart application");
                Console.ReadKey();
                return;
            }
            EventLog myLog = new EventLog();
            myLog.Source = "MySource";
            myLog.WriteEntry("Log event!");
            //These messages can then be viewed by the Windows Event Viewer
        }

        public void EventLogReading()
        {
            EventLog log = new EventLog("MyNewLog");

            Console.WriteLine("Total entries: " + log.Entries.Count);
            EventLogEntry last = log.Entries[log.Entries.Count - 1];
            Console.WriteLine("Index:   " + last.Index);
            Console.WriteLine("Source:  " + last.Source);
            Console.WriteLine("Type:    " + last.EntryType);
            Console.WriteLine("Time:    " + last.TimeWritten);
            Console.WriteLine("Message: " + last.Message);
        }

        public void EventLogSubscriptionToChanges()
        {
            //there is a special EntryWritten event that you can subscribe to for changes
            EventLog applicationLog = new EventLog("Application", ".", "testEventLogEvent");
            applicationLog.EntryWritten += (sender, e) =>
            {
                Console.WriteLine(e.Entry.Message);
            };
            applicationLog.EnableRaisingEvents = true;
            applicationLog.WriteEntry("Test message", EventLogEntryType.Information);

            Console.ReadKey();
        }

        public void usingStopWatch()
        {
            //StopWatch class has a Start, Stop, and Reset method.
            //You can get the elapsed time in milliseconds, in ticks, or formatted as here
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Algorithm1();
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            sw.Reset();

            sw.Start();
            Algorithm2();
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("Ready...");
            Console.ReadLine();
        }

        private static void Algorithm2()
        {
            string result = "";

            for (int x = 0; x < numberOfIterations; x++)
            {
                result += 'a';
            }
        }

        private static void Algorithm1()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < numberOfIterations; x++)
            {
                sb.Append('a');

            }

            string result = sb.ToString();
        }

        public void PerformanceCounterReading()
        {
            Console.WriteLine("Press escape key to stop");
            //All performance counters are part of a category, and within that category they have a unique name
            //To access the performance counters, your application has to run under full trust
            //or the account should be an administrator or be a part of the Performance Monitor Users group.
            //All performance counters implement IDisposable because they access unmanaged resources
            using (PerformanceCounter pc = new PerformanceCounter("Memory", "Available Bytes"))
            {
                string text = "Available memory: ";
                Console.Write(text);
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.Write(pc.RawValue);
                        Console.SetCursorPosition(text.Length, Console.CursorTop);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }

        }

        public void PerformanceCounterWriting()
        {
            if (CreatePerformanceCounters())
            {
                Console.WriteLine("Created performance counters");
                Console.WriteLine("Please restart application");
                Console.ReadKey();
                return;
            }
            var totalOperationsCounter = new PerformanceCounter(
                "MyCategory",
                "# operations executed",
                "",
                false);
            var operationsPerSecondCounter = new PerformanceCounter(
                "MyCategory",
                "# operations / sec",
                "",
               false);

            totalOperationsCounter.Increment();
            operationsPerSecondCounter.Increment();
        }

        private static bool CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("MyCategory"))
            {
                CounterCreationDataCollection counters =
                    new CounterCreationDataCollection
                {
                    new CounterCreationData(
                        "# operations executed",
                        "Total number of operations executed",
                        PerformanceCounterType.NumberOfItems32),
                    new CounterCreationData(
                        "# operations / sec",
                        "Number of operations executed per second",
                        PerformanceCounterType.RateOfCountsPerSecond32)
                };

                PerformanceCounterCategory.Create("MyCategory",
                        "Sample category for Codeproject", counters);

                return true;
            }
            return false;
        }

    }
}

