using AutoMapper;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Features.User.GetByUsername;
using Tiktok_Clone.BLL.Features.User.GetCurrentUser;
using Tiktok_Clone.BLL.Features.User.Register;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.BLL.MapperProfiles.User;

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

        CreateMap<UserEntity, UserAuthorDTO>();
    }
}