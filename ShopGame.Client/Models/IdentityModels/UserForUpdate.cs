using System.ComponentModel.DataAnnotations;

namespace ShopGame.Client.Models.IdentityModels;

public class UserForUpdate
{
    [Required]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "NameRequired")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "EmailRequired")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "NumRequired")]
    [Display(Name = "PhoneNumber")]
    [StringLength(16,ErrorMessage = "NumError", MinimumLength = 10)]
    public string Telephone { get; set; } = string.Empty;
    
   



}