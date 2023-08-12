using AutoMapper;
using ShopGame.Client.Models.DTOModels;
using ShopGame.Client.Models.Outcoming;

namespace ShopGame.Client.Profiles;

public class GenreProfile:Profile
{
    public GenreProfile()
    {
        CreateMap<GenresForDisplay,GenreDTO>().ReverseMap();
    }
}