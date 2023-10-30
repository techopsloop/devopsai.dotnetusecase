using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi;
using WebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<ILogger<WeatherForecastController>> loggerMock;
        private Mock<IWeatherForecastRepository> repositoryMock;
        private Fixture fixture;

        [TestInitialize]
        public void Init()
        {
            fixture = new Fixture();
            loggerMock = new Mock<ILogger<WeatherForecastController>>();
            repositoryMock = new Mock<IWeatherForecastRepository>();
        }

        [TestMethod]
        public async Task WeatherForecast_Get_ReturnsList()
        {
            // Arrange
            var sut = new WeatherForecastController(loggerMock.Object, repositoryMock.Object);
            var repoResult = fixture.CreateMany<WeatherForecast>(6);

            repositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(repoResult);

            // Act
            var result = await sut.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == repoResult.Count());
        }
    }
}