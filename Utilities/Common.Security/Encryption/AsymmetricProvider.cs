using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Encryption
{
    public class AsymmetricProvider
    {
        public KeyPair GenerateNewKeyPair(int keySize = 2048)
        {
            // KeySize is measured in bits. 1024 is the default, 2048 is better, 4096 is more robust but takes a fair bit longer to generate.
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                return new KeyPair { PublicKey = rsa.ToXmlString(false), PrivateKey = rsa.ToXmlString(true) };
            }
        }

        public KeyPair GetKeyPair()
        {
            // Call query to retrive key pair           
            return new KeyPair { PublicKey = "", PrivateKey = ""};
            
        }

        private byte[] EncryptData(byte[] data, string publicKey)
        {
            using (var asymmetricProvider = new RSACryptoServiceProvider())
            {
                asymmetricProvider.FromXmlString(publicKey);
                return asymmetricProvider.Encrypt(data, true);
            }
        }

        private  byte[] DecryptData(byte[] data, string publicKey)
        {
            using (var asymmetricProvider = new RSACryptoServiceProvider())
            {
                asymmetricProvider.FromXmlString(publicKey);
                if (asymmetricProvider.PublicOnly)
                {
                    throw new Exception("The key provided is a public key and does not contain the private key elements required for decryption");
                }
                return asymmetricProvider.Decrypt(data, true);
            }
        }

        public string EncryptString(string value, string publicKey)
        {
            return Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(value), publicKey));
        }

        public string DecryptString(string value, string privateKey)
        {
            return Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(value), privateKey));
        }
    }
}
