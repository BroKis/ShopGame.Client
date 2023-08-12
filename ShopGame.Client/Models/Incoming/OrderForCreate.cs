using System.ComponentModel.DataAnnotations;
using ShopGame.Client.Models.Outcoming;

namespace ShopGame.Client.Models.Incoming;

public class OrderForCreate
{
    
    public int Id { get; set; }
    [Display(Name = "Date")]
    public DateTime OrderTime { get; set; } = DateTime.Now;
    public int GameID { get; set; }
    public int UserId { get; set; }
    [Display(Name = "Name")]
    public string? Title { get; set; }
    [Display(Name = "Picture")]
    public string? ImageUrl { get; set; }
    [Display(Name = "Cost")]
    public decimal Cost { get; set; }
    [Display(Name = "Genre")]
    public string Genre { get; set; }
        
}