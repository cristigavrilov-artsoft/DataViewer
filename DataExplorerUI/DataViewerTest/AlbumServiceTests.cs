using DataExplorerModels;
using DataExplorerDTOs;
using Microsoft.AspNetCore.Mvc;
using DataExplorerUI.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using System.Text;
namespace DataViewerTest
{
    public class AlbumServiceTests
    {
        public HttpClient httpClient { get; set; }
        public IConfiguration configuration { get; set; }
        public AlbumServiceTests()
        {
            var iConfigSettings = new Dictionary<string, string> {
                {"API:AlbumsEndpoint", "http://localhost/api/albums"},
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(iConfigSettings)
                .Build();

        }

        [Fact]
        public void Constructor_ThrowsExceptionWhenConfigIsMissing()
        {
            var configMock = new ConfigurationBuilder().Build();
            var httpClient = new HttpClient();

            var ex = Assert.Throws<InvalidOperationException>(() =>
                new AlbumService(httpClient, configMock));

            Assert.Equal("Missing Albums Endpoint", ex.Message);
        }

        [Fact]
        public async void getAlbumsAsync_ShouldReturnResultWithExpectedFields()
        {

            var mockedResponse = File.ReadAllText("Mocks/albumsResponse.json");
            var httpClient = TestUtils.CreateMockHttpClient(mockedResponse);
            var albumsService = new AlbumService(httpClient, configuration);

            var albums = await albumsService.GetAlbumsAsync();

            Assert.NotNull(albums);
            Assert.Equal(1, albums[0].Id);
            Assert.Equal("Test Album", albums[0].Title);
            Assert.Equal("testUser", albums[0].CreatedBy);
            Assert.Equal("Test Company", albums[0].FromCompany);
            Assert.Equal("testUrl", albums[0].Photo);
        }
    }
}
