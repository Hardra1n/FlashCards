using FlashCards.Models;

public interface ICardRepository
{
    public IQueryable<Card> Cards { get; }

    public Card InsertCard(Card card);

    public Card? UpdateCard(long id, Card card);

    public void DeleteCard(Card card);
}