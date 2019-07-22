using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    }
}
