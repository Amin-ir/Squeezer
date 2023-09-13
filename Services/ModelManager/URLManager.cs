using Squeezer.Infrastructure;
using Squeezer.Models;

namespace Squeezer.Services
{
    public class URLManager : IModelManager<Url>
    {
        SqueezerDbContext db;
        IAuthenticator Authenticator;
        IURLShortener UrlShortener;
        public URLManager(SqueezerDbContext db, IURLShortener urlShortener, IAuthenticator authenticator)
        {
            this.db = db;
            UrlShortener = urlShortener;
            Authenticator = authenticator;
        }
        public Url Create(Url url)
        {
            string shortenedPath = UrlShortener.Shorten(url.OriginalUrl);
            url.ShortenedUrl = shortenedPath;
            url.DateAdded = DateTime.Now;
            url.UserId = Authenticator.IsAuthenticated() ? Authenticator.GetUserId() : null;

            while (IsDuplicate(url))
                url.ShortenedUrl += Random.Shared.Next(maxValue: 10);

            db.Add(url);
            db.SaveChanges();
            
            return url;
        }
        public bool IsDuplicate(Url url)
        {
            return db.Urls.Any(urlRecord => urlRecord.ShortenedUrl == url.ShortenedUrl);
        }
        public bool Delete(int urlId)
        {
            try
            {
                db.Remove(urlId);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Url> Get(int? userId)
        {
            return db.Urls.Where(url => (userId.HasValue) ? url.UserId == userId : true).ToList();
        }
        public string GetOriginalUrl(string shortUrl)
        {
            return db.Urls.First(url => url.ShortenedUrl == shortUrl).OriginalUrl;
        }
    }
}
