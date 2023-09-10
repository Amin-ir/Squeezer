namespace Squeezer.Services
{
    public interface IEncoder
    {
        public string Encode(object input);
        public bool IsEncoded(object input);
    }
}
