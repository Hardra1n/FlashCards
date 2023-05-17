using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models
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