using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopGame.Client.Models.DTOModels;
using ShopGame.Client.Models.Incoming;
using ShopGame.Client.Models.Outcoming;

namespace ShopGame.Client.Controllers;
record Error(string Message);
public class CatalogController:Controller
{
    private IMapper _mapper;
    private static HttpClient httpClient = new HttpClient();
    private static int ID = 0;
    public CatalogController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return View("NotFound", "Home");
        }
        else if (response.StatusCode == HttpStatusCode.OK)
        {
            // считываем ответ
            IEnumerable<GameDTO>? game = await response.Content.ReadFromJsonAsync<IEnumerable<GameDTO>>();
            List<GameForDisplay> gameList = _mapper.Map<List<GameForDisplay>>(game);
            return View(gameList);

        }

        return View("BadRequest", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> GameDetail(int id)
    {
        using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
        IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();

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
            return View(GameReverse);

        }
        return View("BadRequest", "Home");
        
    }

    public async Task<IActionResult> Create()
    {
        using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
        IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
       
        var RevGenres = _mapper.Map<IEnumerable<GenresForDisplay>>(genres);
      
        ViewBag.Genres = RevGenres;
        
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> Create(GameForCreate game)
    {
        if (!ModelState.IsValid)
        {
            using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
            IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
            var RevGenres = _mapper.Map<IEnumerable<GenresForDisplay>>(genres);
            ViewBag.Genres = RevGenres;
            return View(game);
        }
        var mappedGame = _mapper.Map<GameDTO>(game);
        using var response = await httpClient.PostAsJsonAsync($"http://localhost:5035/api/GameApi", mappedGame);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return View("BadRequest", "Home");
        }

        return View("Success", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
        IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
        var RevGenres = _mapper.Map<IEnumerable<GenresForDisplay>>(genres);
        ViewBag.Genres = RevGenres;
        using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return View("NotFound", "Home");
        }
        else if (response.StatusCode == HttpStatusCode.OK)
        {
            ID = id;
            // считываем ответ
            GameDTO? game = await response.Content.ReadFromJsonAsync<GameDTO>();
            GameForUpdate GameReverse = _mapper.Map<GameForUpdate>(game);
            return View(GameReverse);

        }
        return View("BadRequest", "Home");
        
        
    }

    [HttpPost]
    public async Task<IActionResult> Update(GameForUpdate game)
    {
        if (!ModelState.IsValid)
        {
            using var gresponse = await httpClient.GetAsync($"http://localhost:5035/api/GenreApi");
            IEnumerable<GenreDTO>? genres = await gresponse.Content.ReadFromJsonAsync<IEnumerable<GenreDTO>>();
            
            var RevGenres = _mapper.Map<IEnumerable<GenresForDisplay>>(genres);
   
            ViewBag.Genres = RevGenres;
            
            return View(game);
        }

        GameDTO? mappedGame = _mapper.Map<GameDTO>(game);
        using var resp = await httpClient.PutAsJsonAsync($"http://localhost:5035/api/GameApi", mappedGame);
        if (resp.StatusCode == HttpStatusCode.NoContent)
        {
            return View("NotFound", "Home");
        }
        else if (resp.StatusCode == HttpStatusCode.OK)
        {
            return View("Success", "Home");
        }
        return View("BadRequest", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        
        using var responce = await httpClient.DeleteAsync($"http://localhost:5035/api/GameApi/{id}");
        if (responce.StatusCode == HttpStatusCode.NotFound)
        {
            return View("NotFound", "Home");
        }
        else if (responce.StatusCode == HttpStatusCode.OK)
        {
            return View("Success", "Home");
        }

        return View("BadRequest", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Search(string gameName)
    {
        using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi");
        IEnumerable<GameDTO>? game = await response.Content.ReadFromJsonAsync<IEnumerable<GameDTO>>();
        List<GameForDisplay> gameList = _mapper.Map<List<GameForDisplay>>(game);
        if (string.IsNullOrWhiteSpace(gameName))
        {
            return PartialView("_CatalogList", gameList);
        }

        var finder = gameList.Where(x => x.Title.Contains(gameName));
        return PartialView("_CatalogList", finder);
    }

    public async Task<ActionResult> ShowAllWithPartialView()
    {
        using var response = await httpClient.GetAsync($"http://localhost:5035/api/GameApi");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return View("NotFound", "Home");
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            // считываем ответ
            IEnumerable<GameDTO>? game = await response.Content.ReadFromJsonAsync<IEnumerable<GameDTO>>();
            List<GameForDisplay> gameList = _mapper.Map<List<GameForDisplay>>(game);
            return PartialView("_CatalogList", gameList);

        }

        return View("BadRequest","Home");
    }
    
}