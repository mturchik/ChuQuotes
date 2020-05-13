using System.Threading.Tasks;
using ChuQuotes.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ChuQuotes.Services
{
    public class ChuckService : IChuckService
    {
        private readonly IRestClient _chuck;
        public ChuckService() => _chuck = new RestClient("https://api.chucknorris.io/jokes/random");

        public async Task<Quote> GetQuote()
        {
            var response = await _chuck.ExecuteAsync(new RestRequest(Method.GET));
            return !response.IsSuccessful ? null : JsonConvert.DeserializeObject<Quote>(response.Content);
        }
    }
}