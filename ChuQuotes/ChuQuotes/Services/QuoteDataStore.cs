using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuQuotes.Models;
using SQLite;

namespace ChuQuotes.Services
{
    public class QuoteDataStore : IDataStore<Quote>
    {
        private static readonly Lazy<SQLiteAsyncConnection> LazyInitializer =
            new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));

        private static SQLiteAsyncConnection Database => LazyInitializer.Value;
        private static bool                  _initialized;

        public QuoteDataStore() => InitializeAsync().SafeFireAndForget(false);

        private static async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (Database.TableMappings.All(m => m.MappedType.Name != nameof(Quote)))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Quote)).ConfigureAwait(false);
                    _initialized = true;
                }
            }
        }

        public async Task<Quote> GetItemAsync(string id) =>
            await Database.GetAsync<Quote>(id);

        public async Task<IEnumerable<Quote>> GetItemsAsync() =>
            await Database.Table<Quote>().ToListAsync();

        public async Task<bool> AddItemAsync(Quote quote) =>
            await Database.FindAsync<Quote>(quote.Id) == null &&
            await Database.InsertAsync(quote)         == 1;

        public async Task<bool> UpdateItemAsync(Quote quote) =>
            await Database.FindAsync<Quote>(quote.Id) != null &&
            await Database.UpdateAsync(quote)         == 1;

        public async Task<bool> DeleteItemAsync(string id) =>
            await Database.FindAsync<Quote>(id)   != null &&
            await Database.DeleteAsync<Quote>(id) == 1;
    }
}