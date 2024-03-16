using ConsoleAPITestApp.Models;

namespace ConsoleAPITestApp.Services.API
{
    public interface ICryptoAPI
    {
        Task<IEnumerable<Currency>> GetTopCurrenciesAsync(int count);
        Task<Currency> GetCurrencyAsync(string id);
        Task<bool> Ping();

    }
}
