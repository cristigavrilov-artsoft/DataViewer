using DataExplorerModels;
using Microsoft.AspNetCore.Mvc;
using DataExplorer.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using System.Text;
namespace DataViewerTest
{
    public class AlbumApiServiceTests
    {
        public HttpClient httpClient { get; set; }
        public IConfiguration configuration { get; set; }
        public AlbumApiServiceTests()
        {
            var iConfigSettings = new Dictionary<string, string> {
                {"db:graphQLEndpoint", "https://graphqlzero.almansi.me/api"},
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
                new PostApiService(httpClient, configMock));

            Assert.Equal("Missing GraphQLEndpoint", ex.Message);
        }

        [Fact]
        public async void geAlbumsAsync_ShouldReturnResultWithExpectedFields()
        {



            var mockedResponse = File.ReadAllText("Mocks/albumsApiResponse.json");
            var httpClient = TestUtils.CreateMockHttpClient(mockedResponse);
            var albumsApiService = new AlbumApiService(httpClient, configuration);

            var albums = await albumsApiService.GetAlbumsAsync();

            Assert.Equal("Test Album", albums[0].Title);
            Assert.Equal("testUser", albums[0].CreatedBy);
            Assert.Equal("Test Company", albums[0].FromCompany);
    }
    }
}
