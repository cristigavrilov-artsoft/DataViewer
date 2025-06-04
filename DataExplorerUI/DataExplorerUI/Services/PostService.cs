using DataExplorerModels;
using System.Net.Http.Json;
namespace DataExplorerUI.Services;

public class PostService
{
    private readonly HttpClient _httpClient;
    private readonly string _postsEndpoint;

    public PostService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _postsEndpoint = configuration["API:PostsEndpoint"]
            ?? throw new InvalidOperationException("Missing Posts Endpoint");
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        try
        {
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>(_postsEndpoint);
            return posts ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new();
        }
    }
}