namespace AdvertisingPlatform.App.Exceptions;

public class LocationValidationException : ArgumentException
{
    public LocationValidationException() : base() { }

    public LocationValidationException(string exceptionText)
        : base(exceptionText)
    {
    }

    public LocationValidationException(string exceptionText, string paramName)
        : base(exceptionText, paramName)
    {
    }
}