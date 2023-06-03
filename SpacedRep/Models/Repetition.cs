using System.ComponentModel.DataAnnotations;

namespace SpacedRep.Models
{
    public class Repetition
    {
        public long Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

    }
}