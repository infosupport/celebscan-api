using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Celebscan.Service.Models
{
    public class PermalinkGeneratorParameters
    {
        [Required]
        public string ImageData { get; set; }
        
        [Required]
        public string Label { get; set; }
        
        [Required]
        public double Score { get; set; }
        
        [Required]
        public List<ScanResult> Scores { get; set; }
    }
}