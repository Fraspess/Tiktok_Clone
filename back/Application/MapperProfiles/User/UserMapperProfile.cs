using Application.Dtos.User;
using AutoMapper;
using Domain.Entities.Identity;

namespace Application.MapperProfiles.User;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {

        CreateMap<UserEntity, UserDTO>()
            .ForMember(u => u.FollowersCount,
                o => o.Ignore())

            .ForMember(u => u.FollowingCount,
                o => o.Ignore())

            .ForMember(u => u.IsOwnProfile,
                o => o.Ignore())

            .ForMember(u => u.IsFollowing,
                o => o.Ignore())

            .ForMember(u => u.Username,
                o => o.MapFrom(u => $"@{u.UserName}"));

        CreateMap<UserEntity, UserMeDTO>()
            .ForMember(u => u.FollowersCount,
            o => o.Ignore())

            .ForMember(u => u.FollowingCount,
                o => o.Ignore())

           .ForMember(u => u.IsOwnProfile,
                    o => o.Ignore())

            .ForMember(u => u.Username,
                o => o.MapFrom(u => $"@{u.UserName}"));

        CreateMap<RegisterUserDTO, UserEntity>()
            .ForMember(dest => dest.Avatar, opt => opt.Ignore());

        CreateMap<UserEntity, UserAuthorDTO>()
            .ForMember(u => u.Username, o => o.MapFrom(u => $"@{u.UserName}"));

        CreateMap<UserEntity, SimpleUserDTO>()
            .ForMember(u => u.Username,
                o => o.MapFrom(u => $"@{u.UserName}"));
    }
}