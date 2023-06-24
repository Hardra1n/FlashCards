namespace FlashCards.Models.Dtos;

public class GetCardDto
{
    public long Id { get; set; }

    public long SpacedRepetitionId { get; set; }

    public DateTime? BlockedUntil { get; set; }

    public string FrontSide { get; set; } = String.Empty;

    public string BackSide { get; set; } = String.Empty;


}