using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopGame.Client.Models.DTOModels;
using ShopGame.Client.Models.Incoming;
using ShopGame.Client.Models.Outcoming;
using ShopGame.Identity.Models;

namespace ShopGame.Client.Controllers;

public class OrderController : Controller
{
    private IMapper _mapper;
    private static HttpClient httpClient = new HttpClient();
    private static OrderForCreate order = new OrderForCreate();
    private UserManager<ApplicationUser> _userManager;
    public OrderController(IMapper mapper,UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
        IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
        {
            using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return View("NotFound", "Home");
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                // считываем ответ
                GameDTO? game = await response.Content.ReadFromJsonAsync<GameDTO>();
                GameForDisplay GameReverse = _mapper.Map<GameForDisplay>(game);
                GameReverse.Genre = genres.First(x => x.Id == GameReverse.GenreId).Title;
                order.Title = GameReverse.Title;
                order.ImageUrl = GameReverse.ImageUrl;
                order.Genre = GameReverse.Genre;
                order.Cost = GameReverse.Cost;
                order.GameID = GameReverse.Id;
                return View(order);
            }
        }
        return View("BadRequest", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Create(string number,string expiry,string name, string cvv)
    {
        order.UserId = Convert.ToInt32(_userManager.GetUserId(User));
        OrderDTO orderDto = new OrderDTO()
        {
            OrderTime = order.OrderTime,
            GameID = order.GameID,
            UserId = order.UserId
        };
        using var response = await httpClient.PostAsJsonAsync($"http://localhost:5035/api/OrderApi", orderDto);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return View("BadRequest", "Home");
        }
        return View("Success", "Home");
    }

    public async Task<IActionResult> Orders()
    {
        var UserId = Convert.ToInt32(_userManager.GetUserId(User));
        using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi");
        IEnumerable<GameDTO>? game = await response.Content.ReadFromJsonAsync<IEnumerable<GameDTO>>();
        List<GameForDisplay> gameList = _mapper.Map<List<GameForDisplay>>(game);
        using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
        IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
        gameList.ForEach(x => x.Genre = genres.First(z => z.Id==x.GenreId).Title);
        var rsp = await httpClient.GetAsync($"http://localhost:5035/api/OrderApi/{UserId}");
        IEnumerable<OrderDTO>? orders = await rsp.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
        List<OrderForDisplay> orderForDisplays = _mapper.Map<List<OrderForDisplay>>(orders);
        orderForDisplays.ForEach(x => x.Game = gameList.Find(z => z.Id==x.GameID));
        return View(orderForDisplays);
    }
    

}