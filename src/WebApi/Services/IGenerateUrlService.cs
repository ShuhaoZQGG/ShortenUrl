namespace WebApi.Services;
public interface IGenerateUrlService
{
  Uri GenerateShortUrl(string originalUrl, string baseUrl, int maxLength);
}
