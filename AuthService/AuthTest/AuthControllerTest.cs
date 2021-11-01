using Application.Services;
using AuthEndpoint.Controllers;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AuthTest
{
    public class AuthControllerTest
    {
        [Fact]
        public void AuthControlerTest()
        {
            //Arrange
            var Content = new UserLoginViewModel { UserName = "Abbas", Password = "123456" };
            var MoqJWTClient = new Mock<ITokenGenerationService>();
            MoqJWTClient.Setup(u => u.ValidateUserAndGetJWT(Content)).ReturnsAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWJiYXMiLCJpc3MiOiJSYW1hbmQiLCJhdWQiOiJSYW1hbmQifQ.wBB4mAPo7ny3a-70tRI8r6bkn-rpptV2e5GEdL1CuU8");
            AuthController authController = new AuthController(MoqJWTClient.Object);

            //Act
            var result = authController.ValidateAsync(Content).Result as ObjectResult;
            var actualResult = result.Value;
            //Assert
            Assert.NotNull(actualResult);
        }

    }
}
