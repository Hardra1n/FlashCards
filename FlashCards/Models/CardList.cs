using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashCards.Models
{
    public class CardList
    {
        [BindNever]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        [BindNever]
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}