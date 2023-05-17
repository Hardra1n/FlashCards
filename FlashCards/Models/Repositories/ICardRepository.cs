using FlashCards.Models;

public interface ICardRepository
{
    public IQueryable<Card> Cards { get; }

}