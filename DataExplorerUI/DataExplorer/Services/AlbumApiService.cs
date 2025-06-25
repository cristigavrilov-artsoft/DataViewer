
using DataExplorerModels;
using DataExplorerDTOs;
namespace DataExplorer.Services;

public class AlbumApiService

{
    private readonly HttpClient _httpClient;
    private readonly string _graphQLEndpoint;
    public AlbumApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _graphQLEndpoint = configuration["db:graphQLEndpoint"]
    ?? throw new InvalidOperationException("Missing GraphQLEndpoint");
    }
    public async Task<List<AlbumDTO>> GetAlbumsAsync()
    {
        var query = new
        {
            query = @"{
                    albums {
                        data {
                            id
                            title
                            photos {
                              data {
                                id
                                title
                                url
                                thumbnailUrl
                              }
                            }
                            user {
                                id
                                username
                                company {
                                    name
                                }
                            }
                        }
                    }
                }"
        };
        var response = await _httpClient.PostAsJsonAsync(_graphQLEndpoint, query);

        if (!response.IsSuccessStatusCode)
            return new();
        var result = await response.Content.ReadFromJsonAsync<AlbumsGraphQLResponse>();
        if (result?.Data?.Albums?.Data == null)
            return new();

        return result.Data.Albums.Data.Select(album => new AlbumDTO
        {
            Id = album.Id,
            Title = album.Title,
            Photo = album?.Photos?.Data?[0].ThumbnailUrl,
            CreatedBy = album?.User?.Username ?? "",
            FromCompany = album?.User?.Company?.Name ?? "",
        }).ToList();
    }
}
