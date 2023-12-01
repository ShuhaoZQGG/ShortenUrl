using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Configurations;
using WebApi.Services;
namespace WebApi.Controllers;
[ApiController]
[Route("api/shorten")]
public class UrlController : ControllerBase
{
  private readonly ShrinkUrlSettings _config;
  private readonly IGenerateUrlService _service;

  public UrlController(IOptions<ShrinkUrlSettings> config, IGenerateUrlService service)
  {
    _config = config.Value;
    _service = service;
  }
  [HttpPost]
  public ActionResult<Uri> CreateShortUrl([FromBody] CreateUrlDto createUrlDto) 
  {

    try
    {
      string originalUrl = createUrlDto.OriginalUrl;
      string baseUrl = _config.BaseUrl;
      int maxLength = _config.MaxLength;
    
      Uri finalUrl = _service.GenerateShortUrl(originalUrl, baseUrl, maxLength);
      return Ok(finalUrl);
    }
    catch (ArgumentException ex)
    {
      return BadRequest(new { Error = ex.Message });
    }
    catch (FormatException ex)
    {
      return BadRequest(new { Error = ex.Message });
    }
  }
}
