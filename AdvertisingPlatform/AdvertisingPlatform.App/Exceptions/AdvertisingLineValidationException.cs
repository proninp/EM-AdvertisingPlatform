namespace AdvertisingPlatform.App.Exceptions;

public class AdvertisingLineValidationException : ArgumentException
{
    public AdvertisingLineValidationException() : base() { }

    public AdvertisingLineValidationException(string exceptionText)
        : base(exceptionText)
    {
    }

    public AdvertisingLineValidationException(string exceptionText, string paramName)
        : base(exceptionText, paramName)
    {
    }
}