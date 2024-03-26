using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);

    // Predicate refers to "likes" or "liked by"
    Task<IEnumerable<LikeDTO>> GetUserLikes(string predicate, int userId);
}
