using AutoMapper;
using ShopGame.Client.Models.DTOModels;
using ShopGame.Client.Models.Incoming;
using ShopGame.Client.Models.Outcoming;

namespace ShopGame.Client.Profiles;

public class GameProfile:Profile
{
    public GameProfile()
    {
        CreateMap<GameForCreate, GameDTO>().ReverseMap();
        CreateMap<GameForUpdate, GameDTO>().ReverseMap();
        CreateMap<GameForDisplay, GameDTO>().ReverseMap();
    }
    
}