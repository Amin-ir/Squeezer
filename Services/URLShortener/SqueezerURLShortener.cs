namespace Squeezer.Services
{
    public class SqueezerURLShortener : IURLShortener
    {
        IEncoder encoder;
        IEncryptor encryptor;
        public SqueezerURLShortener(IEncryptor encryptor, IEncoder encoder)
        {
            this.encoder = encoder;
            this.encryptor = encryptor;
        }
        public string Shorten(object url)
        {
            var encryptedUrl = encryptor.Encrypt(url);
            var encodedUrl = encoder.Encode(encryptedUrl);
            return encodedUrl.Substring(0,5);
        }
    }
}
