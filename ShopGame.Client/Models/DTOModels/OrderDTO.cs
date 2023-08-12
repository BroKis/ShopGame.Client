namespace ShopGame.Client.Models.DTOModels;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderTime { get; set; }
    public int GameID { get; set; }
    public int UserId { get; set; }
}