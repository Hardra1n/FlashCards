using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.Dtos;

public class UpdateCardListDto
{
    [Required]
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}