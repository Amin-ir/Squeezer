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
        public Url Get(int Id)
        {
            return db.Urls.First(url => url.Id == Id);
        }
        public IEnumerable<Url> GetByUser(int userId)
        {
            return db.Urls.Where(url => url.UserId == userId);
        }
        public IEnumerable<Url> GetAll()
        {
            return db.Urls;
        }
        public string GetOriginalUrl(string shortUrl)
        {
            return db.Urls.First(url => url.ShortenedUrl == shortUrl).OriginalUrl;
        }
        public int GetUrlCountByUser(int userId)
        {
            return db.Urls.Count(url => url.UserId == userId);
        }
    }
}
