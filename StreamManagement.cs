using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class StreamManagement
    {
        public void fileStreamCreation()
        {
            string path = @"c:\temp\test.dat";
            using (FileStream fileStream = File.Create(path))
            {
                string myValue = "MyValue";
                byte[] data = Encoding.UTF8.GetBytes(myValue);
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

        public void compressingData()
        {
            string folder = @"c:\temp";
            string uncompressedFilePath = Path.Combine(folder, "uncompressed.dat");
            string compressedFilePath = Path.Combine(folder, "compressed.gz");
            byte[] dataToCompress = Enumerable.Repeat((byte)'a', 1024 * 1024).ToArray();

            using (FileStream uncompressedFileStream = File.Create(uncompressedFilePath))
            {
                uncompressedFileStream.Write(dataToCompress, 0, dataToCompress.Length);
            }
            using (FileStream compressedFileStream = File.Create(compressedFilePath))
            {
                //you can pass another Stream to the constructor of a GZipStream
                using (GZipStream compressionStream = new GZipStream(
                            compressedFileStream, CompressionMode.Compress))
                {
                    //When writing data to the GZipStream, it compresses the data and then
                    //immediately forwards it to the FileStream
                    compressionStream.Write(dataToCompress, 0, dataToCompress.Length);
                }
            }

            FileInfo uncompressedFile = new FileInfo(uncompressedFilePath);
            FileInfo compressedFile = new FileInfo(compressedFilePath);

            Console.WriteLine(uncompressedFile.Length); // Displays 1048576
            Console.WriteLine(compressedFile.Length); // Displays 1052

        }

        public void usingBufferedStream()
        {
            string path = @"c:\temp\bufferedStream.txt";

            using (FileStream fileStream = File.Create(path))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        streamWriter.WriteLine("A line of text.");
                    }
                }
            }

        }

        public void characterEncoding()
        {
            // Create a file to hold the output.
            using (StreamWriter output = new StreamWriter("output.txt"))
            {
                // Create and write a string containing the symbol for pi.
                string srcString = "Area = \u03A0r^2";
                output.WriteLine("Source Text : " + srcString);
                // Write the UTF-16 encoded bytes of the source string.
                byte[] utf16String = Encoding.Unicode.GetBytes(srcString);
                output.WriteLine("UTF-16 Bytes: {0}",
                    BitConverter.ToString(utf16String));

                // Convert the UTF-16 encoded source string to UTF-8 and ASCII.
                byte[] utf8String = Encoding.UTF8.GetBytes(srcString);
                byte[] asciiString = Encoding.ASCII.GetBytes(srcString);
                // Write the UTF-8 and ASCII encoded byte arrays.
                output.WriteLine("UTF-8  Bytes: {0}",
                    BitConverter.ToString(utf8String));
                output.WriteLine("ASCII  Bytes: {0}",
                    BitConverter.ToString(asciiString));
                // Convert UTF-8 and ASCII encoded bytes back to UTF-16 encoded string and write.
                output.WriteLine("UTF-8  Text : {0}",
                    Encoding.UTF8.GetString(utf8String));
                output.WriteLine("ASCII  Text : {0}",
                    Encoding.ASCII.GetString(asciiString));
            }
            // Wait to continue.
            Console.WriteLine("\nMain method complete. Press Enter");
            Console.ReadLine();
        }

        public byte[] DecimalToByteArray(decimal src)
        {
            // Create a byte array from a decimal.
            // Create a MemoryStream as a buffer to hold the binary data.
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a BinaryWriter to write binary data to the stream.
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Write the decimal to the BinaryWriter/MemoryStream.
                    writer.Write(src);
                    // Return the byte representation of the decimal.
                    return stream.ToArray();
                }
            }
        }

        public decimal ByteArrayToDecimal(byte[] src)
        {
            // Create a decimal from a byte array.
            // Create a MemoryStream containing the byte array.
            using (MemoryStream stream = new MemoryStream(src))
            {
                // Create a BinaryReader to read the decimal from the stream.
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Read and return the decimal from the
                    // BinaryReader/MemoryStream.
                    return reader.ReadDecimal();
                }
            }
        }



    }
}
