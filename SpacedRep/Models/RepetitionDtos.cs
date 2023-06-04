using System.ComponentModel.DataAnnotations;

namespace SpacedRep.Models
{
    public class RepetitionReadDto
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public RepetitionStage Stage { get; set; }
        public DateTime? LastReviewOn { get; set; }
        public DateTime? BlockedUntil { get; set; }
    }

    public class RepetitionUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [EnumDataType(typeof(RepetitionStage))]
        public RepetitionStage Stage { get; set; }
    }

    public static class RepetitionDtoExtensions
    {
        public static Repetition ToRepetition(this RepetitionUpdateDto dto)
            => new Repetition()
            {
                Id = dto.Id,
                Stage = dto.Stage
            };

        public static RepetitionReadDto ToReadDto(this Repetition rep)
            => new RepetitionReadDto()
            {
                Id = rep.Id,
                Stage = rep.Stage,
                CreatedOn = rep.CreatedOn,
                LastReviewOn = rep.LastReviewOn,
                BlockedUntil = rep.BlockedUntil
            };
    }
}