using System.Security.Cryptography;
using System.Text;

namespace Squeezer.Services
{
    public class MD5Encryptor : IEncryptor
    {
        public byte[] Encrypt(object input)
        {
            byte[] resultBytes;
            using (var grapher = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input.ToString());
                resultBytes = grapher.ComputeHash(bytes);
            }
            return resultBytes;
        }
        public string EncryptToString(object input)
        {
            byte[] byteHash = Encrypt(input);
            return Encoding.UTF8.GetString(byteHash);
        }
    }
}
