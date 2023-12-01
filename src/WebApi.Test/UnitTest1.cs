using WebApi.Controllers;
namespace WebApi.Test;
using Moq;
using System;
using WebApi.Configurations;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CreateShortUrl_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var configMock = new Mock<IOptions<ShrinkUrlSettings>>();
        configMock.Setup(x => x.Value).Returns(new ShrinkUrlSettings { BaseUrl = "https://example.co", MaxLength = 6 });
        var serviceMock = new Mock<IGenerateUrlService>();
        serviceMock.Setup(x => x.GenerateShortUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(new Uri("https://example.com"));
        var controller = new UrlController(configMock.Object, serviceMock.Object);
        var dto = new CreateUrlDto { OriginalUrl = "https://www.quantbe.com/welcome/canada/logs/validate" };
        // Act
        var result = controller.CreateShortUrl(dto);
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = result.Result as OkObjectResult;
        Assert.IsInstanceOfType(okResult.Value, typeof(Uri));
    }

  [TestMethod]
  public void CreateShortUrl_EmptyOriginalUrl_ReturnsBadRequest()
  {
      // Arrange
      var configMock = new Mock<IOptions<ShrinkUrlSettings>>();
      configMock.Setup(x => x.Value).Returns(new ShrinkUrlSettings { BaseUrl = "https://example.co", MaxLength = 6 });
      var generateUrlService = new GenerateUrlService();
      var controller = new UrlController(configMock.Object, generateUrlService);
      var dto = new CreateUrlDto { OriginalUrl = null };
      // Act
      var result = controller.CreateShortUrl(dto);
      // Assert
      Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
  }

    [TestMethod]
    public void CreateShortUrl_InvalidlUrl_ReturnsBadRequest()
    {
        // Arrange
        var configMock = new Mock<IOptions<ShrinkUrlSettings>>();
        configMock.Setup(x => x.Value).Returns(new ShrinkUrlSettings { BaseUrl = "https://example.co", MaxLength = 6 });
        var generateUrlService = new GenerateUrlService();
        var controller = new UrlController(configMock.Object, generateUrlService);
        var dto = new CreateUrlDto { OriginalUrl = "IamAInvalidURL" };
        // Act
        var result = controller.CreateShortUrl(dto);
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

      [TestMethod]
      public void CreateShortUrl_EmptyOriginalUrl_ThrowArgumentException()
      {
          // Arrange
          var generateUrlService = new GenerateUrlService();
          string emptyUrl = "";
          string baseUrl = "https://example.co";
          int maxLength = 6;
          // Act & Assert
          Assert.ThrowsException<ArgumentException>(() => generateUrlService.GenerateShortUrl(emptyUrl, baseUrl, maxLength));
      }

      [TestMethod]
      public void CreateShortUrl_InvalidlUrl_ThrowArgumentException()
      {
          // Arrange
          var generateUrlService = new GenerateUrlService();
          string invalidUrl = "IamAInvalidURL";
          string baseUrl = "https://example.co";
          int maxLength = 6;
          // Act & Assert
          Assert.ThrowsException<FormatException>(() => generateUrlService.GenerateShortUrl(invalidUrl, baseUrl, maxLength));
      }
}