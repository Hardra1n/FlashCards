using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Card
    {
        public long Id { get; set; }

        [Required]
        public string FrontSide { get; set; } = String.Empty;

        [Required]
        public string BackSide { get; set; } = String.Empty;
    }
}