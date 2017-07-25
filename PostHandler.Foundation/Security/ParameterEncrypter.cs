namespace PostHandler.Foundation.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;


    public sealed class ParameterEncrypter 
    {
        #region Fields
        private static volatile ParameterEncrypter instance;
        private static object SyncRoot = new Object();
        private readonly SHA512Managed Encryptor = default(SHA512Managed);
        private readonly UnicodeEncoding UnicodeEncoding = default(UnicodeEncoding);
        private readonly UTF8Encoding UTF8Encoding = default(UTF8Encoding);
        #endregion

        public static ParameterEncrypter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null) { instance = new ParameterEncrypter(); }
                    }
                }
                return instance;
            }
        }

        private ParameterEncrypter()
        {
            Encryptor = new SHA512Managed();
            UnicodeEncoding = new UnicodeEncoding();
        }

        public string SHA512Encrypt(string password)
        {
            var bytPassword = UnicodeEncoding.GetBytes(password);
            var hashPassword = Encryptor.ComputeHash(bytPassword);
            return Convert.ToBase64String(hashPassword);
        }

        public string Md5Encrypt(string data)
        {
            var encoding = new ASCIIEncoding();
            var bytes = encoding.GetBytes(data);
            var hashed = MD5.Create().ComputeHash(bytes);
            return UTF8Encoding.GetString(hashed);
        }
    }
}