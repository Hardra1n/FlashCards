using MVC.Models;

namespace MVC.Data
{
    public class FlashCardsClient
    {
        private readonly HttpClient _httpClient;

        public FlashCardsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CardList>?> GetAsyncCardLists()
            => await _httpClient.GetFromJsonAsync<IEnumerable<CardList>>(
                GetCardListEndpointURI());

        public async Task<CardList?> GetAsyncCardLists(long id)
            => await _httpClient.GetFromJsonAsync<CardList>(
                GetCardListEndpointURI(id.ToString()));

        public async Task<CardList?> CreateAsyncCardList(CardList list)
        {
            HttpResponseMessage response
                = await _httpClient.PostAsJsonAsync<CardList>(
                    GetCardListEndpointURI(),
                    list);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CardList>();
            }

            return null;
        }

        public async Task<CardList?> UpdateAsyncCardList(long id, CardList list)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<CardList>(
                GetCardListEndpointURI(id.ToString()),
                list);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CardList>();
            }

            return null;
        }

        public async Task<bool> RemoveAsyncCardList(long id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(
                GetCardListEndpointURI(id.ToString()));
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Card>?> GetAsyncCardsByListId(long listId)
            => await _httpClient.GetFromJsonAsync<IEnumerable<Card>>(
                GetCardEndpointURI(listId));

        public async Task<Card?> GetAsyncCard(long listId, long cardId)
            => await _httpClient.GetFromJsonAsync<Card>(
                GetCardEndpointURI(listId, cardId.ToString()));

        public async Task<Card?> CreateCard(long listId, Card card)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Card>(
                GetCardEndpointURI(listId),
                card);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Card>();
            }

            return null;
        }

        public async Task<Card?> UpdateAsyncCard(long listId, long cardId, Card card)
        {
            HttpResponseMessage response
                = await _httpClient.PutAsJsonAsync<Card>(
                    GetCardEndpointURI(listId, cardId.ToString()),
                    card);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Card>();
            }
            return null;
        }

        public async Task<bool> DeleteAsyncCard(long listId, long cardId)
        {
            HttpResponseMessage response
                = await _httpClient.DeleteAsync(GetCardEndpointURI(listId, cardId.ToString()));
            return response.IsSuccessStatusCode;
        }

        private string GetCardListEndpointURI(string? addition = null)
        {
            string baseUriAddress = $"/api/CardList";
            if (addition != null)
            {
                baseUriAddress += $"/{addition}";
            }
            return baseUriAddress;
        }

        private string GetCardEndpointURI(long listId, string? addition = null)
        {
            string baseUriAddress = GetCardListEndpointURI() + $"/{listId}/Cards";
            if (addition != null)
            {
                baseUriAddress += $"/{addition}";
            }

            return baseUriAddress;
        }
    }
}