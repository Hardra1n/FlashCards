using FlashCards.Models;

public interface ICardRepository
{
    public IQueryable<Card> Cards { get; }

    public Card AddCard(Card card);

    public Card? UpdateCard(long id, Card card);

    public void RemoveCard(Card card);
}