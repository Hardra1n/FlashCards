using FlashCards.Models.Dtos.Remote;

namespace FlashCards.Models.Dtos;

public static class FlashCardsDtoExtensions
{
    public static Card ToCard(this CreateCardDto dto)
        => new Card()
        {
            FrontSide = dto.FrontSide,
            BackSide = dto.BackSide
        };


    public static Card ToCard(this UpdateCardDto dto)
        => new Card()
        {
            FrontSide = dto.FrontSide,
            BackSide = dto.BackSide
        };


    public static CardList ToCardList(this CreateCardListDto dto)
        => new CardList()
        {
            Name = dto.Name,
            Description = dto.Description
        };


    public static CardList ToCardList(this UpdateCardListDto dto)
        => new CardList()
        {
            Name = dto.Name,
            Description = dto.Description
        };


    public static GetCardDto ToGetCardDto(this Card card, RecieveRepetitionDto repetitionDto)
        => new GetCardDto()
        {
            Id = card.Id,
            SpacedRepetitionId = card.SpacedRepetitionId,
            FrontSide = card.FrontSide,
            BackSide = card.BackSide,
            BlockedUntil = repetitionDto.BlockedUntil
        };
}