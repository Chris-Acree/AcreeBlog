﻿using System.Security.Claims;
using System.Threading.Tasks;
using AcreeBlog.Data.Models;
using AcreeBlog.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcreeBlog.Controllers
{

    public class AccountController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger _logger;
    //private UserManager<ApplicationUser> userManager;
    //private SignInManager<ApplicationUser> signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    public IActionResult Login(string returnUrl)
    {
      ViewData["returnUrl"] = returnUrl;
      return View();
    }


    public async Task LoginOAuth(string returnUrl = "/account/GoogleLogin")
    {
        await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountLoginViewModel alvm, string returnUrl)
    {
      if (!ModelState.IsValid) return View(alvm);

      var au = await _userManager.FindByEmailAsync(alvm.Email);

      if (au != null)
      {
        await _signInManager.SignOutAsync();
        var result = await _signInManager.PasswordSignInAsync(au, alvm.Password, alvm.RememberMe, false);
        if (result.Succeeded)
        {
          _logger.LogInformation("Info - Successful login for {0}", au.Email);
          return Redirect(returnUrl ?? "/");
        }
      }

      _logger.LogWarning("Warning - unsuccessful login attempt for {0}", au?.Email);
      ModelState.AddModelError(nameof(AccountLoginViewModel.Email), "Invalid user or passsword");
      return View(alvm);
    }

    public async Task<IActionResult> Logoff()
    {
      var curUser = await _userManager.GetUserAsync(HttpContext.User);
      _logger.LogInformation("Info - Logout for {0}", curUser.Email);
      await _signInManager.SignOutAsync();
      return Redirect("/");
    }


    [AllowAnonymous]
    public IActionResult GoogleLogin(string returnURL)
    {
        string redirectUrl = Url.Action("GoogleResponse", "Account", new { ReturUrl = returnURL });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    [AllowAnonymous]
    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction(nameof(Login));
        }        

        var result = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider, info.ProviderKey, false);

        if (result.Succeeded)
        {
            return Redirect(returnUrl);
        }
        else
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
            };


            // Does a User exist with this email?
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                await _signInManager.SignInAsync(existingUser, false);
                return Redirect(returnUrl);
            }


            IdentityResult identResult = await _userManager.CreateAsync(user);            
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl);
                }
            }
            return AccessDenied();

        }
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

        [HttpPost]
    public IActionResult FacebookLogin(AccountLoginViewModel alvm, string returnUrl)
    {
      string redirectUrl = Url.Action("FacebookResponse", "Account", new { ReturnUrl = returnUrl, RememberMe = alvm.RememberMe });
      var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
      return new ChallengeResult("Facebook", properties);
    }

    public async Task<IActionResult> FacebookResponse(bool RememberMe, string returnUrl = "/")
    {
      ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
      if (info == null)
      {
        return RedirectToAction(actionName: nameof(Login));
      }

      var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, RememberMe);
      if (result.Succeeded)
      {
        _logger.LogInformation("Info - Facebook Login succeeded for {0}", info.Principal.FindFirstValue(ClaimTypes.Email));

        return Redirect(returnUrl);
      }
      else
      {
        _logger.LogInformation("Info - New registration started via Facebook for {0}", info.Principal.FindFirstValue(ClaimTypes.Email));

        ViewData["ReturnUrl"] = returnUrl;
        ViewData["LoginProvider"] = info.LoginProvider;
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
      }
    }

    [HttpPost]
    public IActionResult TwitterLogin(AccountLoginViewModel alvm, string returnUrl)
    {
      string redirectUrl = Url.Action("TwitterResponse", "Account", new { ReturnUrl = returnUrl, RememberMe = alvm.RememberMe });
      var properties = _signInManager.ConfigureExternalAuthenticationProperties("Twitter", redirectUrl);
      return new ChallengeResult("Twitter", properties);
    }

    public async Task<IActionResult> TwitterResponse(bool RememberMe, string returnUrl = "/")
    {
      ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
      if (info == null)
      {
        return RedirectToAction(nameof(Login));
      }

      var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, RememberMe);
      if (result.Succeeded)
      {
        _logger.LogInformation("Info - Twitter Login succeeded for {0}", info.Principal.FindFirstValue(ClaimTypes.Name));

        return Redirect(returnUrl);
      }
      else
      {
        _logger.LogInformation("Info - New registration started via Twitter for {0}", info.Principal.FindFirstValue(ClaimTypes.Name));

        ViewData["ReturnUrl"] = returnUrl;
        ViewData["LoginProvider"] = info.LoginProvider;
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
      }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
    {
      // Get the information about the user from the external login provider
      var info = await _signInManager.GetExternalLoginInfoAsync();

      if (ModelState.IsValid)
      {
        if (info == null)
        {
          return View("ExternalLoginFailure");
        }

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
          result = await _userManager.AddLoginAsync(user, info);
          if (result.Succeeded)
          {
            await _signInManager.SignInAsync(user, isPersistent: false);

            _logger.LogInformation(6, "User created an account {0} using {1} provider.", model.Email, info.LoginProvider);

            return RedirectToLocal(returnUrl);
          }

        }
        else
        {
          _logger.LogWarning("Unable to create user with email {0} and login provider {1}.", model.Email, info.LoginProvider);

          ModelState.AddModelError("", result.ToString());
        }

        // AddErrors(result);
      }

      ViewData["ReturnUrl"] = returnUrl;
      ViewData["LoginProvider"] = info.LoginProvider;

      return View(model);
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }
  }
}
