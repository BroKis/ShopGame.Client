using System.ComponentModel.DataAnnotations;

namespace ShopGame.Client.Models.Outcoming;

public class OrderForDisplay
{
    public int Id { get; set; }
    [Display(Name = "Дата заказа")]
    public DateTime OrderTime { get; set; } = new DateTime();
    public int GameID { get; set; }
    public int UserId { get; set; }
    public GameForDisplay Game { get; set; }
}