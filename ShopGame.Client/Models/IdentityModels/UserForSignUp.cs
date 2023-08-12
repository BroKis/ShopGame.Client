using System.ComponentModel.DataAnnotations;
using ShopGame.Identity.Models;

namespace ShopGame.Client.Models.IdentityModels;

public class UserForSignUp
{
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "NameRequired")]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "NumRequired")]
    [Display(Name = "PhoneNumber")]
    [StringLength(16,ErrorMessage = "NumError", MinimumLength = 10)]
    public string PhoneNumber { get; set; } = string.Empty;

    

    [Required(ErrorMessage = "PassRequired")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(100, ErrorMessage =
        "PassError", MinimumLength = 5)]
    
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "ConfRequired")] 
    [Display(Name = "ConfirmPassword")]
    [Compare("Password", ErrorMessage = "ConfError")]
    [DataType(DataType.Password)]
   
    public string PasswordConfirm { get; set; } = string.Empty;

    [Required(ErrorMessage = "RoleRequired")] 
    [Display(Name = "Role")] 
    public string Role { get; set; } = AvailableRoles.Client;
}