namespace FlashCards.Models.Dtos;

public class GetCardListDto
{
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public ICollection<GetCardDto> Cards { get; set; } = new List<GetCardDto>();

}