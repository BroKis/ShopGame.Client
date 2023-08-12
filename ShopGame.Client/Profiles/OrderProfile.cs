using AutoMapper;
using ShopGame.Client.Models.DTOModels;
using ShopGame.Client.Models.Incoming;
using ShopGame.Client.Models.Outcoming;

namespace ShopGame.Client.Profiles;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<OrderForCreate, OrderDTO>().ReverseMap();
      
        CreateMap<OrderForDisplay, OrderDTO>().ReverseMap();
    }
}