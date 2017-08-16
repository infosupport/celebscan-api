using System.Collections.Generic;

namespace Celebscan.Service.Models
{
    public class PermalinkGeneratorParameters
    {
        public string ImageData { get; set; }
        public string Label { get; set; }
        public double Score { get; set; }
        public List<ScanResult> Scores { get; set; }
    }
}