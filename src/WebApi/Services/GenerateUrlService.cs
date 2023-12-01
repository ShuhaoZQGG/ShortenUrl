using WebApi.Utils;

namespace WebApi.Services;
public class GenerateUrlService : IGenerateUrlService
{
  public Uri GenerateShortUrl(string originalUrl, string baseUrl, int maxLength)
  {
    if (string.IsNullOrEmpty(originalUrl))
    {
        throw new ArgumentException("Original URL cannot be empty or null.");
    }
    // Basic URL format check
    if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
    {
        throw new FormatException("Invalid URL format.");
    }
    string randomString = Cryptography.EncryptUrl(baseUrl, maxLength);
    Uri finalUrl = new Uri(new Uri(baseUrl), randomString);

    return finalUrl;
  }
}
