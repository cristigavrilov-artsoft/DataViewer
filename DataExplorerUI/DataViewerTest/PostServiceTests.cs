using DataExplorerModels;
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
    public class PostServiceTests
    {
        public HttpClient httpClient { get; set; }
        public IConfiguration configuration { get; set; }
        public PostServiceTests()
        {
            var iConfigSettings = new Dictionary<string, string> {
                {"API:PostsEndpoint", "http://localhost/api/posts"},
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
                new PostService(httpClient, configMock));

            Assert.Equal("Missing Posts Endpoint", ex.Message);
        }

        [Fact]
        public async void getPostsAsync_ShouldReturnResultWithExpectedFields()
        {
            var mockedResponse = File.ReadAllText("Mocks/postsResponse.json");
            var httpClient = TestUtils.CreateMockHttpClient(mockedResponse);
            var postsService = new PostService(httpClient, configuration);

            var posts = await postsService.GetPostsAsync();

            Assert.NotNull(posts);
            Assert.Equal(1, posts[0].Id);
            Assert.Equal("Test Title", posts[0].Title);
            Assert.Equal("Test Body", posts[0].Body);
        }
    }
}
