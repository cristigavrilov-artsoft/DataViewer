using DataExplorerModels;
using Microsoft.AspNetCore.Mvc;
using DataExplorer.Services;
namespace DataExplorer.Controllers;

[ApiController]
[Route("/api/posts")]
public class PostController : ControllerBase
{
    private readonly PostApiService _postService;

    public PostController(PostApiService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }
}
