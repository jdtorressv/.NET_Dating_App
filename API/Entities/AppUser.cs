namespace API.Entities;

public class AppUser
{
    public int Id { get; set; } // Using "Id" here lets the entity framework know to treat this as primary key 
    public string UserName { get; set; }

}
