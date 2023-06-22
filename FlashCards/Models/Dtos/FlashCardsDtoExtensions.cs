namespace FlashCards.Models.Dtos;

public static class FlashCardsDtoExtensions
{
    public static Card ToCard(this CreateCardDto dto)
    {
        return new Card()
        {
            FrontSide = dto.FrontSide,
            BackSide = dto.BackSide
        };
    }

    public static Card ToCard(this UpdateCardDto dto)
    {
        return new Card()
        {
            FrontSide = dto.FrontSide,
            BackSide = dto.BackSide
        };
    }

    public static CardList ToCardList(this CreateCardListDto dto)
    {
        return new CardList()
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public static CardList ToCardList(this UpdateCardListDto dto)
    {
        return new CardList()
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }
}