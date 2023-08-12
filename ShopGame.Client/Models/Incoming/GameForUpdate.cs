using System.ComponentModel.DataAnnotations;

namespace ShopGame.Client.Models.Incoming;

public class GameForUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "NameRequired")]
    [StringLength(50,ErrorMessage = "NameError",MinimumLength = 10)]
    [Display(Name = "Name")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "ShortDescRequired")]
    [StringLength(150,ErrorMessage = "ShortError",MinimumLength = 10)]
    [Display(Name = "ShortDesc")]
    public string? ShortDescription { get; set; }

    [Required(ErrorMessage = "DescRequired")]
    [StringLength(2000,ErrorMessage = "DescError",MinimumLength = 1000)]
    [Display(Name = "Desc")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "UrlRequire")]
    [Url]
    [Display(Name = "Url")]
    public string? ImageUrl { get; set; }
    [Display(Name = "InStock")]
    public bool InStock { get; set; }
    [Required(ErrorMessage = "CostRequired")]
    [Range(1.00,200.00,ErrorMessage = "CostError")]
    [Display(Name = "Cost")]
    public decimal Cost { get; set; }
    [Display(Name = "Genre")]
    [Required(ErrorMessage = "GenreRequired")]
    public int GenreId { get; set; }
  
}