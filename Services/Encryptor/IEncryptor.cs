namespace Squeezer.Services.Encryptor
{
    public interface IEncryptor
    {
        public byte[] Encrypt(object input);
    }
}
