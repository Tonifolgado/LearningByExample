using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class StreamManagement
    {
        public void fileStreamCreation()
        {
            //FileStream supports methods for reading and writing bytes in files
            string path = @"c:\temp\test.dat";
            using (FileStream fileStream = File.Create(path))
            {
                string myValue = "MyValue";
                byte[] data = Encoding.UTF8.GetBytes(myValue);
                //When writing to a FileStream, you can use the synchronous method Write, which expects a byte array.
                fileStream.Write(data, 0, data.Length);
            }
        }

        public void srWriterCreation()
        {
            string path = @"c:\temp\test.dat";

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                string myValue = "MyValue";
                streamWriter.Write(myValue);
            }
        }

        public void openFileStream()
        {
            //using a FileStream object, reading the bytes, and converting them back to a string with the correct encoding
            string path = @"c:\temp\test.dat";
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] data = new byte[fileStream.Length];

                for (int index = 0; index < fileStream.Length; index++)
                {
                    data[index] = (byte)fileStream.ReadByte();
                }
                Console.WriteLine(Encoding.UTF8.GetString(data)); // Displays: MyValue
            }
            //If you know that you are parsing a text file, you can also use a StreamReader
            //StreamReader uses a default encoding and returns the bytes to you as a string
            using (StreamReader streamWriter = File.OpenText(path))
            {
                Console.WriteLine(streamWriter.ReadLine()); // Displays: MyValue
            }
        }


    }
}
