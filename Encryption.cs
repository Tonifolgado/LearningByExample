using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    class Encryption
    {
        public void SymmetricEncryption()
        {
            string original = "My secret data!";
            //AES is a symmetric algorithm that uses a key and IV for encryption. 
            //By using the same key and IV, you can decrypt a piece of text. 
            //The cryptography classes all work on byte sequences.

            using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())
            {
                byte[] encrypted = Encrypt(symmetricAlgorithm, original);
                string roundtrip = Decrypt(symmetricAlgorithm, encrypted);
                Console.WriteLine("Texto encriptado: {0}", encrypted);

                // Displays: My secret data! 
                Console.WriteLine("Original:   {0}", original);
                Console.WriteLine("Round Trip: {0}", roundtrip);
            }
        }
        public void AsymmetricEncryption()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKeyXML = rsa.ToXmlString(false);
            string privateKeyXML = rsa.ToXmlString(true);

            Console.WriteLine("The public key is: {0}", publicKeyXML);
            Console.WriteLine("");
            Console.WriteLine("The private key is: {0}", privateKeyXML);
            Console.WriteLine("");

            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes("My Secret Data!");

            byte[] encryptedData;

            //you first need to convert the data you want to encrypt to a byte sequence
            //To encrypt the data, you need only the public key
                //using a Key Container
                string containerName = "SecretContainer";
            //The .NET Framework offers a secure location for storing asymmetric keys in a key container
            //A key container can be specific to a user or to the whole machine. 
            CspParameters csp = new CspParameters() { KeyContainerName = containerName };
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(csp))
            {
                RSA.FromXmlString(publicKeyXML);
                encryptedData = RSA.Encrypt(dataToEncrypt, false);

                //byte[] encryptedData;

            }

            Console.WriteLine("Encrypted data: {0}", encryptedData);

            //You then use the private key to decrypt the data.
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(csp))
            {
                RSA.FromXmlString(privateKeyXML);
                decryptedData = RSA.Decrypt(encryptedData, false);
            }

            string decryptedString = ByteConverter.GetString(decryptedData);
            Console.WriteLine(decryptedString); // Displays: My Secret Data!



        }

        static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            //The SymmetricAlgorithm class has both a method for creating an encryptor and a decryptor
            //By using the CryptoStream class, you can encrypt or decrypt a byte sequence
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                    new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt =
                    new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        public void HashCodeCalculation()
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            SHA256 sha256 = SHA256.Create();

            //using the SHA256Managed algorithm to calculate the hash code for a piece of text.
            string data1 = "A paragraph of text";
            //GetBytes obtains a string of bytes from a string of chars 
            byte[] hashA = sha256.ComputeHash(byteConverter.GetBytes(data1));

            string data2 = "A paragraph of changed text";
            byte[] hashB = sha256.ComputeHash(byteConverter.GetBytes(data2));

            string data3 = "A paragraph of text";
            byte[] hashC = sha256.ComputeHash(byteConverter.GetBytes(data3));

            //different strings give a different hash code 
            //and the same string gives the exact same hash code
            Console.WriteLine("String1: {0}", data1);
            Console.WriteLine("String2: {0}", data2);
            Console.WriteLine("String3: {0}", data3);

            Console.WriteLine(hashA.SequenceEqual(hashB)); // Displays: false
            Console.WriteLine("String1 y String2 tienen diferente hashcode");
            Console.WriteLine(hashA.SequenceEqual(hashC)); // Displays: true
            Console.WriteLine("String1 y String3 tienen el mismo hashcode");
        }

        public SecureString SecureStringInitialize()
        {
            //SecureString implements IDisposable, so using is employ to assure disposing
            using (SecureString ss = new SecureString())
            {
                Console.Write("Please enter password: ");
                while (true)
                {
                    //SecureString can deal with only individual characters at a time
                    //It’s not possible to pass a string directly                     
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    //if it is used the Enter key the program exits
                    if (cki.Key == ConsoleKey.Enter) break;

                    ss.AppendChar(cki.KeyChar);
                    //only the * character is displayed
                    Console.Write("*");
                }
                ss.MakeReadOnly();
                return ss;
            }
        }

        public void ConvertToUnsecureString(SecureString securePassword)
        {            
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                //The Marshal class offers five methods used when you are decrypting a SecureString
                //Those methods accept a SecureString and return an IntPtr. 
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

    }
}
