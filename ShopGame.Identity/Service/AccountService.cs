using System.Text;
using Microsoft.AspNetCore.Identity;
using ShopGame.Identity.Models;
using ShopGame.Identity.Utils;

namespace ShopGame.Identity.Service;

public class AccountService:IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<Response<bool>> RegisterAsync(ApplicationUser user, string password,string role)
    {
        var response = new Response<bool>();

        try
        {
            if (user is null)
            {
                response.Data = false;
                response.Description = "Data can't be null";
                response.StatusCode = StatusCode.BadRequest;

                return response;
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = new StringBuilder(16);

                result.Errors.ToList().ForEach(i => errors.Append(i.Description));

                response.Data = false;
                response.Description = errors.ToString();
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            await _userManager.AddToRoleAsync(user, role);
            await _signInManager.SignInAsync(user, false);
            response.Data = true;
            response.Description = "The user was created successfully";
            response.StatusCode = StatusCode.OK;

            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Description = ex.Message;
            response.StatusCode = StatusCode.OK;

            return response;
        }
    }

    public async Task<Response<bool>> LoginAsync(string email, string password)
    {
        var response = new Response<bool>();

        try
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
            {
                response.Data = false;
                response.Description = "Invalid email or password";
                response.StatusCode = StatusCode.NotFound;
                return response;
            }

            response.Data = true;
            response.Description = "The user was sign in successfully";
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Description = ex.Message;
            response.StatusCode = StatusCode.BadRequest;

            return response;
        }
    }

    public async Task<Response<bool>> LogoutAsync()
    {
        var response = new Response<bool>();

        try
        {
            await _signInManager.SignOutAsync();

            response.Data = true;
            response.Description = "The user was log out successfully";
            response.StatusCode = StatusCode.OK;

            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Description = ex.Message;
            response.StatusCode = StatusCode.BadRequest;

            return response;
        }
    }

    
}