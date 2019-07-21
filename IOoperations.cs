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
