using AutoMapper;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.BLL.MapperProfiles.User;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<CreateUserDTO, UserEntity>();

        CreateMap<UserEntity, UserDTO>();


        CreateMap<RegisterUserDTO, UserEntity>()
            .ForMember(dest => dest.Avatar, opt => opt.Ignore());
    }
}