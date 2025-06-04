using DataExplorer.Services;
using DataExplorerModels;
using DataExplorerDTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/albums")]
public class AlbumController : ControllerBase
{
    private readonly AlbumService _albumService;

    public AlbumController(AlbumService albumService)
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
