using Microsoft.Extensions.Options;

namespace WebApi.Configurations;
public class ShrinkUrlSettings
{  
  public string BaseUrl { get; set; }
  public int MaxLength { get; set; }
}
