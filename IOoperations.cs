using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class IOoperations
    {
        public void driveInformation()
        {
            DriveInfo[] drivesInfo = DriveInfo.GetDrives();

            foreach (DriveInfo driveInfo in drivesInfo)
            {
                Console.WriteLine("Drive {0}", driveInfo.Name);
                Console.WriteLine("  File type: {0}", driveInfo.DriveType);

                if (driveInfo.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", driveInfo.VolumeLabel);
                    Console.WriteLine("  File system: {0}", driveInfo.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        driveInfo.AvailableFreeSpace);
                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        driveInfo.TotalFreeSpace);
                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        driveInfo.TotalSize);
                }
            }
        }

        public void infoRetriever(string[] pars)
        {
            if (pars.Length == 0)
            {
                Console.WriteLine("Please supply a filename.");
                return;
            }
            // Display file information.
            FileInfo file = new FileInfo(pars[0]);
            Console.WriteLine("Checking file: " + file.Name);
            Console.WriteLine("File exists: " + file.Exists.ToString());

            if (file.Exists)
            {
                Console.Write("File created: ");
                Console.WriteLine(file.CreationTime.ToString());
                Console.Write("File last updated: ");
                Console.WriteLine(file.LastWriteTime.ToString());
                Console.Write("File last accessed: ");
                Console.WriteLine(file.LastAccessTime.ToString());
                Console.Write("File size (bytes): ");
                Console.WriteLine(file.Length.ToString());

                Console.Write("File attribute list: ");
                Console.WriteLine(file.Attributes.ToString());
            }
            Console.WriteLine();
            // Display directory information.
            DirectoryInfo dir = file.Directory;
            Console.WriteLine("Checking directory: " + dir.Name);
            Console.WriteLine("In directory: " + dir.Parent.Name);
            Console.Write("Directory exists: ");
            Console.WriteLine(dir.Exists.ToString());

            if (dir.Exists)
            {
                Console.Write("Directory created: ");
                Console.WriteLine(dir.CreationTime.ToString());
                Console.Write("Directory last updated: ");
                Console.WriteLine(dir.LastWriteTime.ToString());
                Console.Write("Directory last accessed: ");
                Console.WriteLine(dir.LastAccessTime.ToString());
                Console.Write("Directory attribute list: ");
                Console.WriteLine(dir.Attributes.ToString());
                Console.WriteLine("Directory contains: " +
                  dir.GetFiles().Length.ToString() + " files");
            }
            Console.WriteLine();
            // Display drive information.
            DriveInfo drv = new DriveInfo(file.FullName);
            Console.Write("Drive: ");
            Console.WriteLine(drv.Name);
            if (drv.IsReady)
            {
                Console.Write("Drive type: ");
                Console.WriteLine(drv.DriveType.ToString());
                Console.Write("Drive format: ");
                Console.WriteLine(drv.DriveFormat.ToString());
                Console.Write("Drive free space: ");
                Console.WriteLine(drv.AvailableFreeSpace.ToString());
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void directoryManagement()
        {
            //create a new directory with the static Directory class or with DirectoryInfo
            var directory = Directory.CreateDirectory(@"C:\Temp\ProgrammingInCSharp\Directory");
            Console.WriteLine(@"Created a directory with static Directory in C:\Temp\ProgrammingInCSharp");
            //DirectoryInfo object can be initialized with a non-existing folder
            //Calling Create will create the directory.
            var directoryInfo = new DirectoryInfo(@"C:\Temp\ProgrammingInCSharp\DirectoryInfo");
            directoryInfo.Create();
            Console.WriteLine(@"Created a directory with DirectoryInfo in C:\Temp\ProgrammingInCSharp");
        }

        public static long CalculateDirectorySize(DirectoryInfo directory,
              bool includeSubdirectories)
        {
            long totalSize = 0;
            // Examine all contained files.
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                totalSize += file.Length;
            }
            // Examine all contained directories.
            if (includeSubdirectories)
            {
                foreach (DirectoryInfo dir in directory.EnumerateDirectories())
                {
                    totalSize += CalculateDirectorySize(dir, true);
                }
            }
            return totalSize;
        }

        public void fileAndDirAttributes()
        {
            // This file has the archive and read-only attributes.
            FileInfo file = new FileInfo(@"C:\Windows\win.ini");
            // This displays the attributes.
            Console.WriteLine(file.Attributes.ToString());
            // This test fails because other attributes are set.
            if (file.Attributes == FileAttributes.ReadOnly)
            {
                Console.WriteLine("File is read-only (faulty test).");
            }
            // This test succeeds because it filters out just the
            // read-only attribute.
            if ((file.Attributes & FileAttributes.ReadOnly) ==
              FileAttributes.ReadOnly)
            {
                Console.WriteLine("File is read-only (correct test).");
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();

        }

        public void deleteExistingDir()
        {
            if (Directory.Exists(@"C:\Temp\ProgrammingInCSharp\Directory"))
            {
                Directory.Delete(@"C:\Temp\ProgrammingInCSharp\Directory");
                Console.WriteLine(@"C:\Temp\ProgrammingInCSharp\Directory deleted");
            }

            var directoryInfo = new DirectoryInfo(@"C:\Temp\ProgrammingInCSharp\DirectoryInfo");
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete();
                Console.WriteLine(@"C:\Temp\ProgrammingInCSharp\DirectoryInfo deleted");
            }

        }

        public void ListDirectories(DirectoryInfo directoryInfo,
            string searchPattern, int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
            {
                return;
            }

            string indent = new string('-', currentLevel);

            try
            {
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories(searchPattern);

                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    Console.WriteLine(indent + subDirectory.Name);
                    ListDirectories(subDirectory, searchPattern, maxLevel, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // You don't have access to this folder.
                Console.WriteLine(indent + "Can't access: " + directoryInfo.Name);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                // The folder is removed while iterating
                Console.WriteLine(indent + "Can't find: " + directoryInfo.Name);
                return;
            }
        }

        public void moveDirectory()
        {
            //It should be an existing directory
            try
            {
            Directory.Move(@"C:\source", @"c:\destination");
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Source");
            directoryInfo.MoveTo(@"C:\destination");
            }
            catch (DirectoryNotFoundException)
            {
                // The folder is removed while iterating
                Console.WriteLine("Can't find some expected directory");
                return;
            }

        }

        public void listDirectoryFiles()
        {
            Console.WriteLine("Using Directory class: ");
            foreach (string file in Directory.GetFiles(@"C:\Windows"))
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("Using DirectoryInfo class: ");
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Windows");
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                Console.WriteLine(fileInfo.FullName);
            }

        }

        public void deleteFile()
        {
            string path = @"c:\temp\test.txt";
            //using File class
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            //using FileInfo class
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }

        public void movingFile()
        {
            string path = @"c:\temp\test.txt";
            string destPath = @"c:\temp\destTest.txt";

            File.CreateText(path).Close();
            File.Move(path, destPath);

            FileInfo fileInfo = new FileInfo(path);
            fileInfo.MoveTo(destPath);
            string path2 = @"c:\temp\test.txt";
            string destPath2 = @"c:\temp\destTest.txt";

            File.CreateText(path2).Close();
            File.Copy(path2, destPath2);

            FileInfo fileInfo2 = new FileInfo(path2);
            fileInfo.CopyTo(destPath2);
        }

        public void pathCreation()
        {
            string folder = @"C:\temp";
            string fileName = "test.dat";
            string fullPath = folder + fileName; // Results in C:\temptest.dat
            Console.WriteLine("Using concatenation: " + fullPath);
            //Using Combine
            string fileName2 = "test2.dat";
            string fullPath2 = Path.Combine(folder, fileName2); // Results in C:\\temp\\test.dat
            Console.WriteLine("Using Path.Combine: " + fullPath2);
            //Using Path methods
            string path = @"C:\temp\subdir\file.txt";
            Console.WriteLine("Using Path.GetDirectoryName: {0}", Path.GetDirectoryName(path)); // Displays C:\temp\subdir
            Console.WriteLine("Using Path.GetExtension: {0}", Path.GetExtension(path)); // Displays .txt
            Console.WriteLine("Using Path.GetFileName: {0}", Path.GetFileName(path)); // Displays file.txt
            Console.WriteLine("Using Path.GetPathRoot: {0}", Path.GetPathRoot(path)); // Displays C:\
        }

        public string ReadAllText()
        {
            string path = @"C:\temp\test.txt";
            //Se puede usar File.Exists para comprobar previamente si el fichero existe
            //if (File.Exists(path))
            //{
            //    return File.ReadAllText(path);
            //}
            //return string.Empty;

            //Para controlar excepciones se puede usar try-catch
            try
            {
                return File.ReadAllText(path);
            }
            catch (DirectoryNotFoundException) { }
            catch (FileNotFoundException) { }
            return string.Empty;

        }

        public void webRequestExecution()
        {
            WebRequest request = WebRequest.Create("http://www.microsoft.com");
            //call the GetResponse method to execute the request and retrieve the response
            WebResponse response = request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());
            string responseText = responseStream.ReadToEnd();

            Console.WriteLine(responseText); // Displays the HTML of the website

            response.Close();

        }

        public void startNotepadProcess()
        {
            // Create a ProcessStartInfo object and configure it with the
            // information required to run the new process.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "notepad.exe";
            startInfo.Arguments = "file.txt";
            startInfo.WorkingDirectory = @"C:\Temp";
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.ErrorDialog = true;
            // Declare a new Process object.
            Process process;

            try
            {
                // Start the new process.
                process = Process.Start(startInfo);
                // Wait for the new process to terminate before exiting.
                Console.WriteLine("Waiting 30 seconds for process to finish.");

                if (process.WaitForExit(30000))
                {
                    Console.WriteLine("Process terminated.");
                }
                else
                {
                    Console.WriteLine("Timed out waiting for process to end.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not start process.");
                Console.WriteLine(ex);
            }

            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void terminateNotepadProcess()
        {
            // Create a new Process and run notepad.exe.
            using (Process process =
                Process.Start("notepad.exe", @"c:\temp\someFile.txt"))
            {
                // Wait for 5 seconds and terminate the notepad process.
                Console.WriteLine(
                    "Waiting 5 seconds before terminating notepad.exe.");
                Thread.Sleep(5000);
                // Terminate notepad process.
                Console.WriteLine("Terminating Notepad with CloseMainWindow.");

                // Try to send a close message to the main window.
                if (!process.CloseMainWindow())
                {
                    // Close message did not get sent - Kill Notepad.
                    Console.WriteLine("CloseMainWindow returned false - " +
                        " terminating Notepad with Kill.");
                    process.Kill();
                }
                else
                {
                    // Close message sent successfully; wait for 2 seconds
                    // for termination confirmation before resorting to Kill.
                    if (!process.WaitForExit(2000))
                    {
                        Console.WriteLine("CloseMainWindow failed to" +
                            " terminate - terminating Notepad with Kill.");
                        process.Kill();
                    }
                }
            }
            // Wait to continue.
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        #region ReadingFiles

        void readFileIntoString(string filepath)
        {
            //Read an entire file into a string
            // filepath = "C:\t1"
            string contents = File.ReadAllText(@filepath);
            Console.Out.WriteLine("Contents : " + contents);
        }

        void readFileIntoArray(string filepath)
        {
            //Read all the lines from a file into an array
            // filepath = "C:\t1"
            string[] lines = File.ReadAllLines(@filepath);
            Console.Out.WriteLine("Contents : " + lines.Length);
            Console.In.ReadLine();
        }

        void readFileLineByLine(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine("XML template : " + line);
            }
            if (sr != null) sr.Close(); // should be in a finally or using block
        }

        public void readWriteTextFile()
        {
            // Create a new file.
            using (FileStream fs = new FileStream("test.txt", FileMode.Create))
            {
                // Create a writer and specify the encoding.
                // The default (UTF-8) supports special Unicode characters,
                // but encodes all standard characters in the same way as
                // ASCII encoding.
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    // Write a decimal, string, and char.
                    w.WriteLine(124.23M);
                    w.WriteLine("Test string");
                    w.WriteLine('!');
                }
            }
            Console.WriteLine("Press Enter to read the information.");
            Console.ReadLine();
            // Open the file in read-only mode.
            using (FileStream fs = new FileStream("test.txt", FileMode.Open))
            {
                using (StreamReader r = new StreamReader(fs, Encoding.UTF8))
                {
                    // Read the data and convert it to the appropriate data type.
                    Console.WriteLine(Decimal.Parse(r.ReadLine()));
                    Console.WriteLine(r.ReadLine());
                    Console.WriteLine(Char.Parse(r.ReadLine()));
                }
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        public void readWriteBinaryFile()
        {
            // Create a new file and writer.
            using (FileStream fs = new FileStream("test.bin", FileMode.Create))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    // Write a decimal, two strings, and a char.
                    w.Write(124.23M);
                    w.Write("Test string");
                    w.Write("Test string 2");
                    w.Write('!');
                }
            }
            Console.WriteLine("Press Enter to read the information.");
            Console.ReadLine();
            // Open the file in read-only mode.
            using (FileStream fs = new FileStream("test.bin", FileMode.Open))
            {
                // Display the raw information in the file.
                using (StreamReader sr = new StreamReader(fs))
                {
                    Console.WriteLine(sr.ReadToEnd());
                    Console.WriteLine();
                    // Read the data and convert it to the appropriate data type.
                    fs.Position = 0;
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Console.WriteLine(br.ReadDecimal());
                        Console.WriteLine(br.ReadString());
                        Console.WriteLine(br.ReadString());
                        Console.WriteLine(br.ReadChar());
                    }
                }
            }
            // Wait to continue.
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Main method complete. Press Enter.");
            Console.ReadLine();
        }

        #endregion

        #region asyncAndParallel
        public async Task createAndWriteAsyncToFile()
        {
            using (FileStream stream = new FileStream("test.dat", FileMode.Create,
                FileAccess.Write, FileShare.None, 4096, true))
            {
                byte[] data = new byte[100000];
                new Random().NextBytes(data);

                await stream.WriteAsync(data, 0, data.Length);
            }
        }

        public void asyncReadFile()
        {
            // Create a test file.
            using (FileStream fs = new FileStream("test2.txt", FileMode.Create))
            {
                fs.SetLength(100000);
            }

            // Start the asynchronous file processor on another thread.
            AsyncProcessor asyncIO = new AsyncProcessor("test2.txt");
            asyncIO.StartProcess();
            // At the same time, do some other work.
            // In this example, we simply loop for 10 seconds.
            DateTime startTime = DateTime.Now;
            while (DateTime.Now.Subtract(startTime).TotalSeconds < 2)
            {
                Console.WriteLine("[MAIN THREAD]: Doing some work.");
                // Pause to simulate a time-consuming operation.
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }

            Console.WriteLine("[MAIN THREAD]: Complete.");
            Console.ReadLine();
            // Remove the test file.
            File.Delete("test2.txt");
        }

        public async Task readAsyncHttpRequest()
        {
            //GetStringAsync method returns Task<string>, 
            //so when the process is finished, a string value is available (or an exception).
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://www.microsoft.com");
            Console.WriteLine(result);
        }

        public async Task executeMultipleRequests()
        {
            HttpClient client = new HttpClient();

            string microsoft = await client.GetStringAsync("http://www.microsoft.com");
            string msdn = await client.GetStringAsync("http://msdn.microsoft.com");
            string blogs = await client.GetStringAsync("http://blogs.msdn.com/");
        }

        public async Task executeMultipleRequestsInParallel()
        {
            HttpClient client = new HttpClient();

            Task microsoft = client.GetStringAsync("http://www.microsoft.com");
            Task msdn = client.GetStringAsync("http://msdn.microsoft.com");
            Task blogs = client.GetStringAsync("http://blogs.msdn.com/");
            //Now, all three operations run parallel
            await Task.WhenAll(microsoft, msdn, blogs);
        }


        #endregion

        public void readUserInputfromConsole()
        {
            // Local variable to hold the key entered by the user.
            ConsoleKeyInfo key;
            // Control whether character or asterisk is displayed.
            bool secret = false;
            // Character List for the user data entered.
            List<char> input = new List<char>();

            string msg = "Enter characters and press Escape to see input." +
                            "\nPress F1 to enter/exit Secret mode and Alt-X to exit.";
            Console.WriteLine(msg);
            // Process input until the user enters "Alt+X" or "Alt+x".
            do
            {
                // Read a key from the console. Intercept the key so that it is not
                // displayed to the console. What is displayed is determined later
                // depending on whether the program is in secret mode.
                key = Console.ReadKey(true);

                // Switch secret mode on and off.
                if (key.Key == ConsoleKey.F1)
                {
                    if (secret)
                    {
                        // Switch secret mode off.
                        secret = false;
                    }
                    else
                    {
                        // Switch secret mode on.
                        secret = true;
                    }
                }
                // Handle Backspace.
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Count > 0)
                    {
                        // Backspace pressed, remove the last character.
                        input.RemoveAt(input.Count - 1);
                        Console.Write(key.KeyChar);
                        Console.Write(" ");
                        Console.Write(key.KeyChar);
                    }
                }
                // Handle Escape.
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Input: {0}\n\n",
                        new String(input.ToArray()));
                    Console.WriteLine(msg);
                    input.Clear();
                }
                // Handle character input.

                else if (key.Key >= ConsoleKey.A && key.Key <= ConsoleKey.Z)
                {
                    input.Add(key.KeyChar);
                    if (secret)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(key.KeyChar);
                    }
                }
            } while (key.Key != ConsoleKey.X
                || key.Modifiers != ConsoleModifiers.Alt);
            // Wait to continue.
            Console.WriteLine("\n\nMain method complete. Press Enter");
            Console.ReadLine();

        }


    }
}
