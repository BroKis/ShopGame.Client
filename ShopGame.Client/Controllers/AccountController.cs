using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopGame.Client.Models;
using ShopGame.Client.Models.IdentityModels;
using ShopGame.Identity.Models;
using ShopGame.Identity.Service;

namespace ShopGame.Client.Controllers;

public class AccountController : Controller
{
  private readonly IAccountService _accountService;
  private readonly IMapper _mapper;
  private readonly UserManager<ApplicationUser> _userManager;
  public AccountController(IAccountService accountService, IMapper mapper, UserManager<ApplicationUser> userManager)
  {
    _accountService = accountService;
    _mapper = mapper;
    _userManager = userManager;
  }

  public IActionResult LoginForm()
  {
    return View();
  }

  [HttpGet]
  public IActionResult RegistrationForm()
  {
    return View();
  }
  [Authorize]
  [HttpGet]
  public async Task<IActionResult> Update()
  {
    // Get the current user
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
      return NotFound();
    }

    // Map the user properties to the view model
    var model = new UserForUpdate
    {
      Id = user.Id,
      Name = user.Name,
      Email =user.Email,
      Telephone = user.PhoneNumber
    };

    return View(model);
  }
 

  

  [HttpPost]
  public async Task<IActionResult> RegistrationForm(UserForSignUp model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    ApplicationUser user = _mapper.Map<ApplicationUser>(model);
    user.UserName = model.Email;

    var result =await  _accountService.RegisterAsync(user, model.Password,model.Role);
    if (result.StatusCode == ShopGame.Identity.StatusCode.OK)
    {
      return RedirectToAction("Index", "Home");
    }
    ModelState.AddModelError(string.Empty, result.Description);
    return View(model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> LoginForm(UserForSignIn model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }
    var result = await _accountService.LoginAsync(model.Email, model.Password);
    if (result.StatusCode == ShopGame.Identity.StatusCode.OK)
    {
      return RedirectToAction("Index", "Home");
                
    }
    ModelState.AddModelError(string.Empty, result.Description);

    return View(model);
  }
  
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    var result = await _accountService.LogoutAsync();

    if (result.StatusCode != ShopGame.Identity.StatusCode.OK)
    {
      View("Error",
        new ErrorViewModel
        {
          Controller = "Home",
          Description = result.Description
        });
    }

    return RedirectToAction("Index", "Home");
  }
  [Authorize]
  [HttpPost]
  public async Task<IActionResult> Update(UserForUpdate model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    // Get the current user
    var user = await _userManager.Users.FirstAsync(x => x.Id == model.Id);
    if (user == null)
    {
      return NotFound();
    }

    // Update the user properties
    user.Name = model.Name;
    user.Email = model.Email;
    user.PhoneNumber = model.Telephone;

    // Update the user in the database
    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      ModelState.AddModelError("", "Failed to update user");
      return View(model);
    }

    return View("Success", "Home");
  }
  
  [HttpGet]
  public async Task<IActionResult> Edit(int id)
  {
    var user = await _userManager.Users.FirstAsync(x => x.Id == id);

    if (user == null)
    {
      return NotFound();
    }
    var model = new UserForUpdate
    {
      Id = user.Id,
      Name = user.Name,
      Email =user.Email,
      Telephone = user.PhoneNumber
    };
    return View("Update",model);

  }

  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    ApplicationUser user = _mapper.Map<ApplicationUser>(await _userManager.Users.FirstAsync(x => x.Id == id));
    if (user == null)
    {
      return NotFound();
    }
    var mark = _userManager.DeleteAsync(user);
    if (mark.IsCompleted)
    {
      return View("Success", "Home");
    }
    return View("BadRequest", "Home");
  }

  public IActionResult Accounts()
  {
    List<UserForUpdate> list = new List<UserForUpdate>();
    foreach (var userManagerUser in _userManager.Users)
    {
      UserForUpdate user = new UserForUpdate()
      {
        Id = userManagerUser.Id,
        Name = userManagerUser.Name,
        Email = userManagerUser.Email,
        Telephone = userManagerUser.PhoneNumber,
      };
      list.Add(user);
    }

    return View(list);
  }

}

 
