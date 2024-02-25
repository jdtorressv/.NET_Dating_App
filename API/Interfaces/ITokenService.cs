using API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user); // That which uses this interface must implement this function 
}
