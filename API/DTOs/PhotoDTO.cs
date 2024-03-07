namespace API.DTOs;

public class PhotoDTO // Abstracts Photo without use of AppUser (needed there for relational database) as to avoid infinit loop of a user having a set of photos having a user having...
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
}
