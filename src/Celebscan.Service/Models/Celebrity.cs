namespace Celebscan.Service.Models
{
    public class Celebrity
    {
        public Celebrity(string name, string url, string imageUrl, string description)
        {
            Name = name;
            Url = url;
            ImageUrl = imageUrl;
            Description = description;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public static Celebrity FromCacheRecord(CelebrityCacheRecord record)
        {
            return new Celebrity(record.Name,record.Url,record.ImageUrl,record.Description);
        }
    }
}