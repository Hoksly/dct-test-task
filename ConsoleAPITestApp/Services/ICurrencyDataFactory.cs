using ConsoleAPITestApp.Models;

namespace ConsoleAPITestApp.Services
{
    public interface ICurrencyDataFactory
    {
        Currency CreateCurrency(object rawData); 
    }
}
