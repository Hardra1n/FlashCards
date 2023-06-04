using System.ComponentModel.DataAnnotations;

namespace SpacedRep.Models
{
    public class Repetition
    {
        public long Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; private set; }

        [Required]
        [EnumDataType(typeof(RepetitionStage))]
        public RepetitionStage Stage { get; set; }

        public DateTime? LastReviewOn { get; private set; } = null;

        public DateTime? BlockedUntil { get; private set; } = null;

        public Repetition()
        {
            CreatedOn = DateTime.Now;
            Stage = RepetitionStage.Created;
        }

        public void Copy(Repetition rep)
        {
            Stage = rep.Stage;
        }
    }
}