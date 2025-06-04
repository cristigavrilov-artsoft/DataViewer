using DataExplorerModels;

namespace DataExplorerDTOs;

public class PostsContainer
{
    public List<Post>? Data { get; set; }
}

public class PostsResponse
{
    public PostsContainer? Posts { get; set; }
}

public class PostsGraphQLResponse
{
    public PostsResponse? Data { get; set; }
}
