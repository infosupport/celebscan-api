using System.Collections.Generic;

namespace Celebscan.Service.Models
{
    public class PermalinkData
    {
        public string ImageData { get; set; }
        public string Label { get; set; }
        public double Score { get; set; }
        public List<ScanResult> Scores { get; set; }

        public static PermalinkData FromPermalink(Permalink link)
        {
            return new PermalinkData
            {
                ImageData = link.ImageData,
                Label = link.Label,
                Score = link.Score,
                Scores = link.Scores
            };
        }
    }
}