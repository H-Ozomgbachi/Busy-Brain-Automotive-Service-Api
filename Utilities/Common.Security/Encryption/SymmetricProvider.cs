using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Common.Security.Encryption
{
    public class SymmetricProvider
    {
        public static readonly string URIIVSeperator = "||";

        public static string URIEncrypt(object Object, string key, byte[] iv = null)
        {
            return URIEncryptWithJsonSerializer(Object, key);
        }

        public static string URIEncryptWithJsonSerializer(object data, string key)
        {
            string s = JsonConvert.SerializeObject(data);
            return URIEncrypt(s, key);
        }

        public static string URIEncrypt(string plainText, string key, byte[] iv = null)
        {
            var base64 = Convert.ToBase64String(Encrypt(plainText, key, ref iv)).Trim('=').Replace("+", "-").Replace("/", "_");

            string initialisationVector = Convert.ToBase64String(iv).Trim('=').Replace("+", "-").Replace("/", "_");

            return $"{base64}{Uri.EscapeUriString(URIIVSeperator)}{initialisationVector}";
        }

        public static byte[] Encrypt(string plainText, string key, ref byte[] iv, int blockSize = 256)
        {
            return Encrypt(plainText, Encoding.UTF8.GetBytes(key), ref iv, blockSize);
        }
        public static byte[] Encrypt(string plainText, byte[] key, ref byte[] iv, int blockSize = 256)
        {
            var inputBytes = Encoding.UTF8.GetBytes(plainText);
            iv = GenerateIV(32);

            var engine = new RijndaelEngine(blockSize);

            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());
            var keyParam = new KeyParameter(key);
            var keyParamWithIv = new ParametersWithIV(keyParam, iv);

            cipher.Init(true, keyParamWithIv);

            var outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            var length = cipher.ProcessBytes(inputBytes, outputBytes, 0);
            cipher.DoFinal(outputBytes, length);
            return outputBytes;
        }

        public static T URIDecrypt<T>(string cipherText, string key)
        {
            object decryptedString = _DecryptURIToString(cipherText, key);
            return _Deserialize<T>(decryptedString);
        }

        private static string _DecryptURIToString(string cipherText, string key)
        {
            var base64 = cipherText.Replace("-", "+").Replace("_", "/").Replace(Uri.EscapeUriString(URIIVSeperator), URIIVSeperator);

            var iv = string.Empty;
            var splitter = base64.Split(new[] { URIIVSeperator }, StringSplitOptions.None);
            if (splitter.Length == 2)
            {
                base64 = splitter[0];
                iv = splitter[1];
            }
            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
            iv = iv.PadRight(iv.Length + (4 - iv.Length % 4) % 4, '=');
            return Decrypt(Convert.FromBase64String(base64), key, Convert.FromBase64String(iv));
        }

        public static string Decrypt(byte[] cipherText, string key, byte[] iv = null)
        {
            return Decrypt(cipherText, Encoding.UTF8.GetBytes(key), iv);
        }

        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv = null, int blockSize = 256)
        {
            try
            {
                if (cipherText == null || cipherText.Length <= 0)
                {
                    throw new ArgumentNullException("EncryptedString");
                }

                if (key == null || key.Length == 0)
                {
                    throw new ArgumentNullException("Key");
                }

                if (iv == null || iv.Length == 0)
                {
                    throw new ArgumentNullException("iv");
                }

                var engine = new RijndaelEngine(blockSize);
                var blockCipher = new CbcBlockCipher(engine);
                var cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding());
                var keyParam = new KeyParameter(key);
                var keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 32);

                cipher.Init(false, keyParamWithIV);

                byte[] comparisonBytes = new byte[cipher.GetOutputSize(cipherText.Length)];
                var length = cipher.ProcessBytes(cipherText, comparisonBytes, 0);
                cipher.DoFinal(comparisonBytes, length);
                return Encoding.UTF8.GetString(comparisonBytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static T _Deserialize<T>(object decryptedString)
        {
            T deserializedObject = default(T);
            try
            {
                deserializedObject = _DeserializeFromJSON<T>(decryptedString.ToString());
            }
            catch (Exception)
            {
                throw;
            }
            return deserializedObject;
        }

        private static T _DeserializeFromJSON<T>(string decryptedJSON)
        {
            return JsonConvert.DeserializeObject<T>(decryptedJSON);
        }

        private static byte[] GenerateIV(int length)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] nonce = new byte[length];
                rng.GetBytes(nonce);
                return nonce;
            }
        }
    }
}
