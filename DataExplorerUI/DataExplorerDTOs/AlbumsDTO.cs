using DataExplorerModels;

namespace DataExplorerDTOs;

public class AlbumDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Photo { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string FromCompany { get; set; } = "";
}

public class AlbumsContainer
{
    public List<Album>? Data { get; set; }
}

public class AlbumsResponse
{
    public AlbumsContainer? Albums { get; set; }
}

public class AlbumsGraphQLResponse
{
    public AlbumsResponse? Data { get; set; }
}
