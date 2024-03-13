namespace ConsoleAPITestApp.Services
{
    public interface IFetchStrategy
    {
        Task<object> FetchRawData(string id); // Raw data type is object for flexibility
    }
}
