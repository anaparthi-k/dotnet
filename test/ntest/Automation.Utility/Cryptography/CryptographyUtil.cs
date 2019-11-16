using System;
using System.Security.Cryptography;
using System.Text;

namespace Automation.Utility.Cryptography
{
    public class CryptographyUtil
    {
        public class Symmetric
        {
            public static string Encrypt(string key, string plainText)
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(plainText);

                //AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
                //string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            /// <summary>
            /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
            /// </summary>
            /// <param name="cipherText">encrypted string</param>
            /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
            /// <returns></returns>
            public static string Decrypt(string key, string cipherText)
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherText);

                //AppSettingsReader settingsReader = new AppSettingsReader();
                ////Get your key from config file to open the lock!
                //string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }
        }

        public class Asymmetric
        {
            public static void GetKey(out string publicKey,out string privateKey)
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                // Get the public keyy   
                publicKey = rsa.ToXmlString(false); // false to get the public key  

                privateKey = rsa.ToXmlString(true); // true to get the private key   
            }

            public static string EncryptText(string publicKey, string text)
            {
                // Convert the text to an array of bytes   
                UnicodeEncoding byteConverter = new UnicodeEncoding();
                byte[] dataToEncrypt = byteConverter.GetBytes(text);

                // Create a byte array to store the encrypted data in it   
                byte[] encryptedData;

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // Set the rsa pulic key   
                    rsa.FromXmlString(publicKey);

                    // Encrypt the data and store it in the encyptedData Array   
                    encryptedData = rsa.Encrypt(dataToEncrypt, false);
                }

                // Save the encypted data array into a file   
                // File.WriteAllBytes(fileName, encryptedData);

                return Convert.ToBase64String(encryptedData);//Encoding.UTF8.(encryptedData);
                //Console.WriteLine("Data has been encrypted");
            }

            // Method to decrypt the data withing a specific file using a RSA algorithm private key   
            public static string DecryptData(string privateKey, string cipherText)
            {
                // read the encrypted bytes from the file   
                //byte[] dataToDecrypt = File.ReadAllBytes(fileName);
                byte[] dataToDecrypt = Convert.FromBase64String(cipherText); //Encoding.UTF8.GetBytes(cipherText);

                // Create an array to store the decrypted data in it   
                byte[] decryptedData;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // Set the private key of the algorithm   
                    rsa.FromXmlString(privateKey);
                    decryptedData = rsa.Decrypt(dataToDecrypt, false);
                }

                // Get the string value from the decryptedData byte array   
                UnicodeEncoding byteConverter = new UnicodeEncoding();
                return byteConverter.GetString(decryptedData);
            }
        }
    }
}
