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
            => await _httpClient.GetFromJsonAsync<IEnumerable<CardList>>("api/CardList");

        public async Task<CardList?> GetAsyncCardLists(long id)
            => await _httpClient.GetFromJsonAsync<CardList>($"/api/CardList/{id}");

        public async Task<CardList?> CreateAsyncCardList(CardList list)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<CardList>("/api/CardList", list);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CardList>();
            }

            return null;
        }

        public async Task<CardList?> UpdateAsyncCardList(long id, CardList list)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<CardList>($"/api/CardList/{id}", list);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CardList>();
            }

            return null;
        }

        public async Task<bool> RemoveAsyncCardList(long id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/CardList/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}