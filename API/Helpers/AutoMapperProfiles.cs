using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDTO>() // Map AppUser into MemberDTO
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos
                .FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<Photo, PhotoDTO>(); // Map Photo into PhotoDTO
        CreateMap<MemberUpdateDTO, AppUser>(); // Map member updates to App User
        CreateMap<RegisterDTO, AppUser>(); // Maps RegisterDTO details to App User
        CreateMap<Message, MessageDTO>() // Maps Message to MessageDTO
            .ForMember(dest => dest.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos
                .FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos
                .FirstOrDefault(x => x.IsMain).Url));
    }
}
