using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVC.Models
{
    public class CardList
    {
        [BindNever]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; } = String.Empty;

        public IEnumerable<Card> Cards { get; set; } = new List<Card>();

    }
}