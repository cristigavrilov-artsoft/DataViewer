

namespace DataExplorerModels;

public class Album
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public Photos? Photos { get; set; }

    public int? UserId { get; set; }
    public string? CreatedBy { get; set; }
    public User? User { get; set; }

}
