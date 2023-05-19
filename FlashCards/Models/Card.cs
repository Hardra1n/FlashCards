using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashCards.Models
{
    public class Card
    {
        [BindNever]
        public long Id { get; set; }

        public long CardListId { get; set; }

        [Required]
        public string FrontSide { get; set; } = String.Empty;

        [Required]
        public string BackSide { get; set; } = String.Empty;

    }
}