using Core.DTOs;
using InfoEndpoint.Controllers;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace InfoTest
{
    public class UserControllerTest
    {
        [Fact]
        public async Task GetUsersTest()
        {
            //Arrange
            var MoqUsersClient = new Mock<IUserInfosRepository>();
            var FakeList = new List<PeapleInfoDTO>()
            {
                new PeapleInfoDTO(){ID = 1 , BirthDate ="1999-02-24" ,City ="Shiraz" , Country = "Iran" ,FirstName = "Abbas" , LastName = "joza" , IsMarried = false , NationalCode =0011526884 ,UserAccessID =2},
                new PeapleInfoDTO(){ID = 1 , BirthDate ="1997-04-15" ,City ="Araq" , Country = "Iran" ,FirstName = "Arash" , LastName = "Ramezani" , IsMarried = true , NationalCode =0011747884 ,UserAccessID =5},
            };

            MoqUsersClient.Setup(u => u.GetAllPeapleInfo()).ReturnsAsync(FakeList);
            UserController userController = new UserController(MoqUsersClient.Object);
            var actualAttribute = userController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true);

            //Act
            var result = userController.GetAll().Result as ObjectResult;
            var actualResult = result.Value;

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());

        }


        [Fact]
        public async Task GetTokenTest()
        {
            var url = "http://localhost:5002/api/Auth";
            string Content = JsonSerializer.Serialize(new { userName = "Abbas", password = "123456" });
            var Token = "";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(url, Content);
                if (response.IsSuccessStatusCode)
                {
                    Token = await response.Content.ReadAsStringAsync();
                }
            }

            Assert.NotNull(Token);
        }

    }
}
