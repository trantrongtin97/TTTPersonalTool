using System.Security.Cryptography;

namespace TTT.Framework.Sercurity
{
    public class EncryptAes : IEncryptBase
    {
        private readonly int KeySize = 256;
        private readonly string Key = "zUVGxriznlO4JoZzee6Q8p+bmdnN/KF6tLQscPe+qzM=";
        private readonly string IV = "p/s2lKtOFx5kzLBMd1kTdA==";
        public string EncryptPassword(string rawPass)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = KeySize;
                aes.Key = Convert.FromBase64String(Key);
                aes.IV = Convert.FromBase64String(IV);
                return GetString(Encrypt(rawPass, aes.Key, aes.IV));
            }
        }

        public string DecriptPassword(string encriptPass)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = KeySize;
                aes.Key = Convert.FromBase64String(Key);
                aes.IV = Convert.FromBase64String(IV);
                return Decrypt(GetByte(encriptPass), aes.Key, aes.IV);
            }
        }

        private byte[] GetByte(string plainText)
        {
            return Convert.FromBase64String(plainText);
        }

        private string GetString(byte[] cipherText)
        {
            return Convert.ToBase64String(cipherText);
        }

        private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        private string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

    }
}
