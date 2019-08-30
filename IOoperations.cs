using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        }


    }
}
