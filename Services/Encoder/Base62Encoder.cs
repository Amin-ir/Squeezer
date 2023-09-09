using System.Numerics;

namespace Squeezer.Services
{
    public class Base62Encoder : IEncoder
    {
        private const string Base62Domain = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public string Encode(object input)
        {
            string encodedArray = "";

            if(input.GetType() == typeof(byte[])) 
            {
                BigInteger bigInteger = new BigInteger((byte[])input);
                if (bigInteger < 0)
                    bigInteger *= -1;

                while(bigInteger > 0)
                {
                    var index = bigInteger % 62;
                    encodedArray += Base62Domain[(int)index];
                    
                    bigInteger /= 62;
                }
            }

            return encodedArray;
        }
    }
}
