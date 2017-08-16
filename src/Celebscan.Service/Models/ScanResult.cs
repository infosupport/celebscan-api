using System.ComponentModel.DataAnnotations;

namespace Celebscan.Service.Models
{
    public class ScanResult
    {
        [Required]
        public string Label { get; set; }
        
        [Required]
        public double Score { get; set; }
    }
}