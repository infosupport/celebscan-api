namespace Celebscan.Service.Models
{
    public class PermalinkData
    {
        public PermalinkData(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}