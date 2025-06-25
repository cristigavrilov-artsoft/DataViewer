using DataExplorer.Services;
using DataExplorerModels;
using DataExplorerDTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/albums")]
public class AlbumController : ControllerBase
{
    private readonly AlbumApiService _albumService;

    public AlbumController(AlbumApiService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AlbumDTO>>> GetAlbums()
    {
        var albums = await _albumService.GetAlbumsAsync();
        return Ok(albums);
    }
}
