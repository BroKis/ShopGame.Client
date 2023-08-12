using AutoMapper;
using ShopGame.Client.Models.IdentityModels;
using ShopGame.Identity.Models;

namespace ShopGame.Client.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserForSignUp, ApplicationUser>().ForMember(dest => dest.UserName,
            opt => opt.MapFrom(src => src.Email));
    }
}