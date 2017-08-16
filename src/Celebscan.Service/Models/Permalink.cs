using System.Collections.Generic;

namespace Celebscan.Service.Models
{
    public class Permalink
    {
        public string Id { get; set; }
        public string ImageData { get; set; }
        public string Label { get; set; }
        public double Score { get; set; }
        public List<ScanResult> Scores { get; set; }

        public static Permalink FromParameters(string identifier, PermalinkGeneratorParameters parameters)
        {
            return new Permalink
            {
                Id = identifier,
                ImageData = parameters.ImageData,
                Label = parameters.Label,
                Score = parameters.Score,
                Scores = parameters.Scores
            };
        }
    }
}