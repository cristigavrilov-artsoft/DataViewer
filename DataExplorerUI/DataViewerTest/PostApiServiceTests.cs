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
    public class PostApiServiceTests
    {
        public HttpClient httpClient { get; set; }
        public IConfiguration configuration { get; set; }
        public PostApiServiceTests()
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
        public async void getPostsAsync_ShouldReturnResultWithExpectedFields()
        {

            var mockedResponse = File.ReadAllText("Mocks/postsApiResponse.json");
            var httpClient = TestUtils.CreateMockHttpClient(mockedResponse);
            var postsApiService = new PostApiService(httpClient, configuration);

            var posts = await postsApiService.GetPostsAsync();

            Assert.NotNull(posts);
            Assert.Equal(1, posts[0].Id);
            Assert.Equal("Test Title", posts[0].Title);
            Assert.Equal("Test Body", posts[0].Body);
        }
    }
}
