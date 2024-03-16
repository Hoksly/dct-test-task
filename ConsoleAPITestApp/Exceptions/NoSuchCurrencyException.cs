namespace ConsoleAPITestApp.Exceptions
{
    namespace ConsoleAPITestApp.Services.API 
    {
        public class NoSuchCurrencyException : Exception
        {
            public NoSuchCurrencyException() : base() { }

            public NoSuchCurrencyException(string message) : base(message) { }

            public NoSuchCurrencyException(string message, Exception innerException) : base(message, innerException) { }
        }
    }

}
