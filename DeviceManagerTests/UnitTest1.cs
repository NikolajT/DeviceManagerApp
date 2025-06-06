using DeviceManagerApp.Helpers;

namespace DeviceManagerTests
{
    public class ApiUrlBuilderTests
    {
        [Test]
        public void BuildStatusUrl_ShouldReturnCorrectUrl()
        {
            // Arrange
            var ip = "192.168.1.100";
            var builder = new ApiUrlBuilder()
                .WithIp(ip)
                .ForStatus();

            // Act
            var url = builder.Build();

            // Assert
            var expected = $"https://{ip}/control/api/v1/livestreams/0";
            Assert.That(url, Is.EqualTo(expected));

            TestContext.WriteLine("Generated Status URL: " + url);
        }

        [Test]
        public void BuildConfigUrl_ShouldReturnCorrectUrl()
        {
            // Arrange
            var ip = "10.0.0.5";
            var builder = new ApiUrlBuilder()
                .WithIp(ip)
                .ForConfig();

            // Act
            var url = builder.Build();

            // Assert
            var expected = $"https://{ip}/control/api/v1/livestreams/customPlatforms/Custom.json";
            Assert.That(url, Is.EqualTo(expected));

            TestContext.WriteLine("Generated Config URL: " + url);
        }
    }
}