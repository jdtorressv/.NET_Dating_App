using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);

    // Predicate refers to "likes" or "liked by"
    Task<PagedList<LikeDTO>> GetUserLikes(LikesParams likesParams);
}
