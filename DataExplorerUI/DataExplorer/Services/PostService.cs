
using DataExplorerModels;
using DataExplorerDTOs;
namespace DataExplorer.Services;

public class PostService

{
    private readonly HttpClient _httpClient;
    private readonly string _graphQLEndpoint;
    public PostService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _graphQLEndpoint = configuration["db:graphQLEndpoint"]
    ?? throw new InvalidOperationException("Missing GraphQLEndpoint");
    }
    public async Task<List<Post>> GetPostsAsync()
    {
        var query = new
        {
            query = @"{
                    posts {
                        data {
                            id
                            title
                            body
                        }
                    }
                }"
        };

        var response = await _httpClient.PostAsJsonAsync(_graphQLEndpoint, query);

        if (!response.IsSuccessStatusCode)
            return new();

        var result = await response.Content.ReadFromJsonAsync<PostsGraphQLResponse>();
        return result?.Data?.Posts?.Data ?? new();

    }
}
