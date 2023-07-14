// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace GestionPracticasProfesionalesUtp.Areas.Identity.Pages.Account
{
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly IUserStore<Users> _userStore;
    private readonly IUserEmailStore<Users> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<Users> userManager,
        IUserStore<Users> userStore,
        SignInManager<Users> signInManager,
        ILogger<RegisterModel> logger,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender)
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      _roleManager = roleManager;
      _emailSender = emailSender;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      [Required]
      [Display(Name = "Nombre")]
      public string Nombre { get; set; }

      [Required]
      [Display(Name = "Apellido paterno")]
      public string ApellidoPaterno { get; set; }

      [Required]
      [Display(Name = "Apellido materno")]
      public string ApellidoMaterno { get; set; }
    }


    public async Task OnGetAsync(string returnUrl = null)
    {
      ReturnUrl = returnUrl;
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        //var user = CreateUser();
        var user = new Users { UserName = Input.Email, Nombre = Input.Nombre, ApellidoPaterno = Input.ApellidoPaterno, ApellidoMaterno = Input.ApellidoMaterno, };

        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
          var defaultrole = _roleManager.FindByNameAsync("ESTUDIANTE").Result;

          if (defaultrole != null)
          {
            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
          }

          _logger.LogInformation("User created a new account with password.");

          var userId = await _userManager.GetUserIdAsync(user);
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          var callbackUrl = Url.Page(
              "/Account/ConfirmEmail",
              pageHandler: null,
              values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
              protocol: Request.Scheme);

          await _emailSender.SendEmailAsync(Input.Email, "Confirma tu correo electronico",
              $"<!DOCTYPE html>\r\n<html\r\n  lang=\"es\"\r\n  xmlns=\"https://www.w3.org/1999/xhtml\"\r\n  xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n>\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width,initial-scale=1\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">\r\n    <title></title>\r\n    <!--[if mso]>\r\n      <style>\r\n        table {{\r\n          border-collapse: collapse;\r\n          border-spacing: 0;\r\n          border: none;\r\n          margin: 0;\r\n        }}\r\n        div,\r\n        td {{\r\n          padding: 0;\r\n        }}\r\n        div {{\r\n          margin: 0 !important;\r\n        }}\r\n      </style>\r\n      <noscript>\r\n        <xml>\r\n          <o:OfficeDocumentSettings>\r\n            <o:PixelsPerInch>96</o:PixelsPerInch>\r\n          </o:OfficeDocumentSettings>\r\n        </xml>\r\n      </noscript>\r\n    <![endif]-->\r\n    <style>\r\n      table,\r\n      td,\r\n      div,\r\n      h1,\r\n      p {{\r\n        font-family: Arial, sans-serif;\r\n      }}\r\n\r\n      /* Buttons for mobile */\r\n      @media screen and (max-width: 530px) {{\r\n        .unsub {{\r\n          display: block;\r\n          padding: 8px;\r\n          margin-top: 14px;\r\n          border-radius: 6px;\r\n          background-color: #057d35;\r\n          text-decoration: none !important;\r\n          font-weight: bold;\r\n        }}\r\n        /* Más mejoras con las consultas de medios */\r\n        .col-lge {{\r\n          max-width: 100% !important;\r\n        }}\r\n      }}\r\n      @media screen and (min-width: 531px) {{\r\n        .col-sml {{\r\n          max-width: 27% !important;\r\n        }}\r\n        .col-lge {{\r\n          max-width: 73% !important;\r\n        }}\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body\r\n    style=\"margin: 0; padding: 0; word-spacing: normal; background-color: #eee\"\r\n  >\r\n    <div\r\n      role=\"article\"\r\n      aria-roledescription=\"email\"\r\n      lang=\"en\"\r\n      style=\"\r\n        text-size-adjust: 100%;\r\n        -webkit-text-size-adjust: 100%;\r\n        -ms-text-size-adjust: 100%;\r\n        background-color: #eee;\r\n      \"\r\n    >\r\n      <!-- ===== Outer Escafold ===== -->\r\n      <table\r\n        role=\"presentation\"\r\n        style=\"width: 100%; border: none; border-spacing: 0\"\r\n      >\r\n        <tr>\r\n          <td align=\"center\" style=\"padding: 0\">\r\n            <!-- ===== Ghost Table ===== -->\r\n            <!--[if mso]>\r\n            <table role=\"presentation\" align=\"center\" style=\"width:600px;\">\r\n            <tr>\r\n            <td>\r\n            <![endif]-->\r\n\r\n            <!-- ===== Container ===== -->\r\n            <table\r\n              role=\"presentation\"\r\n              style=\"\r\n                width: 94%;\r\n                max-width: 600px;\r\n                border: none;\r\n                border-spacing: 0;\r\n                text-align: left;\r\n                font-family: Arial, sans-serif;\r\n                font-size: 16px;\r\n                line-height: 22px;\r\n                color: #363636;\r\n              \"\r\n            >\r\n              <!-- ===== Header ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 40px 30px 30px 30px;\r\n                    text-align: center;\r\n                    font-size: 24px;\r\n                    font-weight: bold;\r\n                  \"\r\n                >\r\n                  <a\r\n                    href=\"#\"\r\n                    style=\"text-decoration: none\"\r\n                    ><img\r\n                      src=\"https://virtual.utpuebla.edu.mx/pluginfile.php/1/theme_academi/logo/1672678911/logoUTP.png\"\r\n                      width=\"165\"\r\n                      alt=\"Logo\"\r\n                      style=\"\r\n                        width: 165px;\r\n                        max-width: 80%;\r\n                        height: auto;\r\n                        border: none;\r\n                        text-decoration: none;\r\n                        color: #ffffff;\r\n                      \"\r\n                    />\r\n                  </a>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Header ===== -->\r\n\r\n              <!-- ===== Content Row 1 ===== -->\r\n              <tr>\r\n                <td style=\"padding: 30px; background-color: #ffffff\">\r\n                  <h1\r\n                    style=\"\r\n                      margin-top: 0;\r\n                      margin-bottom: 16px;\r\n                      font-size: 26px;\r\n                      line-height: 32px;\r\n                      font-weight: bold;\r\n                      letter-spacing: -0.02em;\r\n                    \"\r\n                  >\r\n                    Confirmacion de correo electronico\r\n                  </h1>\r\n                  <p style=\"margin: 0\">\r\n                    Por favor, confirma tu correo electronico haciendo\r\n                    <a\r\n                      href=\"{HtmlEncoder.Default.Encode(callbackUrl)}\"\r\n                      style=\"color: #057d35; text-decoration: underline\"\r\n                      >click aqui</a\r\n                    >\r\n                  </p>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Content Row 1 ===== -->\r\n\r\n              <!-- ===== Full Width Image ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 0 0 12px 0;\r\n                    font-size: 24px;\r\n                    line-height: 28px;\r\n                    font-weight: bold;\r\n                  \"\r\n                >\r\n                  <div style=\"text-decoration: none\">\r\n                    <img\r\n                      src=\"https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1170&q=80\"\r\n                      width=\"600\"\r\n                      alt=\"\"\r\n                      style=\"\r\n                        width: 100%;\r\n                        height: auto;\r\n                        display: block;\r\n                        border: none;\r\n                        text-decoration: none;\r\n                        color: #363636;\r\n                      \"\r\n                    />\r\n                  </div>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== Full Width Image ===== -->\r\n\r\n              <!-- ===== Footer Section ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 30px;\r\n                    text-align: center;\r\n                    font-size: 12px;\r\n                    background-color: #057d35;\r\n                    color: #cccccc;\r\n                  \"\r\n                >\r\n                  <!-- ===== Footer Content  ===== -->\r\n                  <p style=\"margin: 0 0 8px 0\">\r\n                    <a href=\"#\" style=\"text-decoration: none\"\r\n                      ><img\r\n                        src=\"https://www.edigitalagency.com.au/wp-content/uploads/facebook-icon-white-png.png\"\r\n                        width=\"40\"\r\n                        height=\"40\"\r\n                        alt=\"f\"\r\n                        style=\"display: inline-block; color: #cccccc\"\r\n                    /></a>\r\n                    <a href=\"#\" style=\"text-decoration: none\"\r\n                      ><img\r\n                        src=\"https://icon-library.com/images/twitter-icon-black-and-white/twitter-icon-black-and-white-12.jpg\"\r\n                        width=\"40\"\r\n                        height=\"40\"\r\n                        alt=\"t\"\r\n                        style=\"display: inline-block; color: #cccccc\"\r\n                    /></a>\r\n                  </p>\r\n                  <p style=\"margin: 0; font-size: 14px; line-height: 20px\">\r\n                    &reg; 2023 - Gestion de practicas profesionales en la UTP<br />\r\n                  </p>\r\n                  <!-- ===== End Footer Content  ===== -->\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Footer Section ===== -->\r\n            </table>\r\n            <!-- ===== End Container ===== -->\r\n\r\n            <!--[if mso]>\r\n            </td>\r\n            </tr>\r\n            </table>\r\n            <![endif]-->\r\n            <!-- ===== Ghost Table ===== -->\r\n          </td>\r\n        </tr>\r\n      </table>\r\n      <!-- ===== End Outer Escafold ===== -->\r\n    </div>\r\n  </body>\r\n</html>\r\n");

          if (_userManager.Options.SignIn.RequireConfirmedAccount)
          {
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
          }
          else
          {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
          }
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    private Users CreateUser()
    {
      try
      {
        return Activator.CreateInstance<Users>();
      }
      catch
      {
        throw new InvalidOperationException($"Can't create an instance of '{nameof(Users)}'. " +
            $"Ensure that '{nameof(Users)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
      }
    }

    private IUserEmailStore<Users> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<Users>)_userStore;
    }
  }
}
