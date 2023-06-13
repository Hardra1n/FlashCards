using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashCards.Models
{
    public class Card : ModelBase<Card>
    {
        [BindNever]
        public long Id { get; set; }

        [Required]
        [BindNever]
        public long SpacedRepetitionId { get; set; }

        [Required]
        public string FrontSide { get; set; } = String.Empty;

        [Required]
        public string BackSide { get; set; } = String.Empty;

        public override void ShallowCopy(Card objToCopy)
        {
            this.FrontSide = objToCopy.FrontSide;
            this.BackSide = objToCopy.BackSide;
        }
    }
}