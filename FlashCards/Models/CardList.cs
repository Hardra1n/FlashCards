using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashCards.Models
{
    public class CardList : ModelBase<CardList>
    {
        [BindNever]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        [BindNever]
        public ICollection<Card> Cards { get; } = new List<Card>();

        public override void ShallowCopy(CardList objToCopy)
        {
            this.Name = objToCopy.Name;
            this.Description = objToCopy.Description;
        }
    }
}