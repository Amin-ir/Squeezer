namespace Squeezer.Services
{
    public interface IEncryptor
    {
        public byte[] Encrypt(object input);
        public string EncryptToString(object input);
    }
}
