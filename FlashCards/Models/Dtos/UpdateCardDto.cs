using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.Dtos;

public class UpdateCardDto
{
    [Required]
    public string FrontSide { get; set; } = String.Empty;

    [Required]
    public string BackSide { get; set; } = String.Empty;
}