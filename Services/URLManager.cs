﻿using Squeezer.Infrastructure;
using Squeezer.Models;

namespace Squeezer.Services
{
    public class URLManager
    {
        SqueezerDbContext db;
        IURLShortener UrlShortener;
        public URLManager(SqueezerDbContext db, IURLShortener urlShortener)
        {
            this.db = db;
            UrlShortener = urlShortener;
        }
        public Url Create(Url url)
        {
            string shortenedPath = UrlShortener.Shorten(url.OriginalUrl);
            url.ShortenedUrl = shortenedPath;
            url.DateAdded = DateTime.Now;

            while (IsUrlDuplicate(url))
                url.ShortenedUrl += Random.Shared.Next(maxValue: 10);

            db.Add(url);
            db.SaveChanges();
            
            return url;
        }
        private bool IsUrlDuplicate(Url url)
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
    }
}
