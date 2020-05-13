using System.Threading.Tasks;
using ChuQuotes.Models;

namespace ChuQuotes.Services
{
    public interface IChuckService
    {
        /// <summary>
        /// Get a random quote from api.chucknorris.io
        /// </summary>
        /// <returns>Quote</returns>
        Task<Quote> GetQuote();
    }
}