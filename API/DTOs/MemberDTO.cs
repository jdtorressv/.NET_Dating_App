namespace API.DTOs;

public class MemberDTO // We can use this as abstraction of AppUser
{
    public int Id { get; set; } // Using "Id" here lets the entity framework know to treat this as primary key 
    public string UserName { get; set; }
    public string PhotoUrl { get; set; } // Will hold URL of user's main photo
    public int Age { get; set; } // Automapper knows it can use AppUser's GetAge function to set this field 
    public string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<PhotoDTO> Photos { get; set; }
}
