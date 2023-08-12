namespace ShopGame.Client.Models.DTOModels;

public class GameDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool InStock { get; set; }
    public decimal Cost { get; set; }
    public int GenreId { get; set; }
    public int PlatformId { get; set; }
}