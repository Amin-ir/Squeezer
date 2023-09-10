using Squeezer.Services;

namespace Squeezer.Utils
{
    public class ShortUrlRouteContstraint : IRouteConstraint
    {
        IEncoder Encoder;
        public ShortUrlRouteContstraint(IEncoder encoder)
        {
            Encoder = encoder;
        }
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string shortUrl = values["shortUrl"].ToString();
            return !string.IsNullOrEmpty(shortUrl) && shortUrl.Length >= 5 && Encoder.IsEncoded(shortUrl);
        }
    }
}
