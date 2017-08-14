using System;

namespace Celebscan.Service.Models
{
    public class CelebrityCacheRecord
    {
        public CelebrityCacheRecord(string name, string url, string imageUrl, string description)
        {
            Key = name.Normalize().ToLower();
            Name = name;
            Url = url;
            ImageUrl = imageUrl;
            Description = description;
        }
        
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public static CelebrityCacheRecord FromCelebrity(Celebrity celebrity)
        {
            return new CelebrityCacheRecord(celebrity.Name,celebrity.Url,celebrity.ImageUrl,celebrity.Description);
        }
    }
}