namespace API.Entities;

public class AppUser // Entities will be converted and inserted into SQLite db via migration
{
    public int Id { get; set; } // Using "Id" here lets the entity framework know to treat this as primary key 
    public string UserName { get; set; }
    public byte[] passwordHash { get; set; }
    public byte[] passwordSalt { get; set; }

}
