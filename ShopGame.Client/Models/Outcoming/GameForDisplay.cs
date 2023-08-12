using System.ComponentModel.DataAnnotations;

namespace ShopGame.Client.Models.Outcoming;

public class GameForDisplay
{
    public int Id { get; set; }
    [Display(Name = "Title")]
    public string? Title { get; set; }
  
    [Display(Name = "ShortDesc")]
    public string? ShortDescription { get; set; }

  
    [Display(Name = "Desc")]
    public string? Description { get; set; }

  
    [Display(Name = "Url")]
    public string? ImageUrl { get; set; }
    [Display(Name= "ImageUrl")]
    public bool InStock { get; set; }
   
    [Display(Name = "С")]
    public decimal Cost { get; set; }
    
    public int GenreId { get; set; }
    
    [Display(Name = "Жанр игры")]
    public string Genre { get; set; }
    
   
}