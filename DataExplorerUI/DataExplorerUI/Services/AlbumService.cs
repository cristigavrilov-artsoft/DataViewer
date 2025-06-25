using System.Net.Http.Json;
using DataExplorerModels;
using DataExplorerDTOs;
using Microsoft.Extensions.Configuration;
namespace DataExplorerUI.Services;
public class AlbumService
{
    private readonly HttpClient _httpClient;
    private readonly string _albumsEndpoint;

    public AlbumService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
       _albumsEndpoint = configuration["API:AlbumsEndpoint"]
            ?? throw new InvalidOperationException("Missing Albums Endpoint");
    }

    public async Task<List<AlbumDTO>> GetAlbumsAsync()
    {
        try
        {
            var albums = await _httpClient.GetFromJsonAsync<List<AlbumDTO>>(_albumsEndpoint);
            return albums ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new();
        }
    }
}