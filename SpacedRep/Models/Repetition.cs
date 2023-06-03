using System.ComponentModel.DataAnnotations;

namespace SpacedRep.Models
{
    public class Repetition
    {
        public long Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public RepetitionStage Stage { get; set; }

        public DateTime? LastReviewOn { get; set; } = null;

        public DateTime? BlockedUntil { get; set; } = null;

    }
}