using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Caterpillar.Core.Security
{
    /// <summary>
    /// Helper for encryption and decryption operations
    /// </summary>
    public class EncryptDecryptHelper
    {
        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        private static readonly byte[] _salt = Encoding.ASCII.GetBytes("nkcy2012iprotest");

        public static string Encrypt(string text, string token)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            string outStr;                              // Encrypted string to return 
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data. 

            try
            {
                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(token, _salt);

                // Create a RijndaelManaged object 
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV 
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream. 
                            swEncrypt.Write(text);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return outStr;
        }

        public static string Decrypt(string text, string token)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext;

            try
            {
                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(token, _salt);

                // Create the streams used for decryption.                 
                byte[] bytes = Convert.FromBase64String(text);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object 
                    // with the specified key and IV. 
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream 
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform. 
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string. 
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            return plaintext;
        }
    }
}
