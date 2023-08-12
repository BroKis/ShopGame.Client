using System.ComponentModel.DataAnnotations;

namespace ShopGame.Client.Models.IdentityModels;

public class UserForSignIn
{
    [Required(ErrorMessage = "NameRequired")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "PasswordRequired")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(100, ErrorMessage = 
        "LengthError", MinimumLength = 5)]
    public string Password { get; set; } = string.Empty;
}