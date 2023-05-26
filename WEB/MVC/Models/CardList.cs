using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class CardList
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();

    }
}