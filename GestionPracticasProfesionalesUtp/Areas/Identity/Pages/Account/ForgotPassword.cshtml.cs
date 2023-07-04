// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GestionPracticasProfesionalesUtp.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Users> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reestablecer contraseña",
                    $"<!DOCTYPE html>\r\n<html\r\n  lang=\"es\"\r\n  xmlns=\"https://www.w3.org/1999/xhtml\"\r\n  xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n>\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width,initial-scale=1\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">\r\n    <title></title>\r\n    <!--[if mso]>\r\n      <style>\r\n        table {{\r\n          border-collapse: collapse;\r\n          border-spacing: 0;\r\n          border: none;\r\n          margin: 0;\r\n        }}\r\n        div,\r\n        td {{\r\n          padding: 0;\r\n        }}\r\n        div {{\r\n          margin: 0 !important;\r\n        }}\r\n      </style>\r\n      <noscript>\r\n        <xml>\r\n          <o:OfficeDocumentSettings>\r\n            <o:PixelsPerInch>96</o:PixelsPerInch>\r\n          </o:OfficeDocumentSettings>\r\n        </xml>\r\n      </noscript>\r\n    <![endif]-->\r\n    <style>\r\n      table,\r\n      td,\r\n      div,\r\n      h1,\r\n      p {{\r\n        font-family: Arial, sans-serif;\r\n      }}\r\n\r\n      /* Buttons for mobile */\r\n      @media screen and (max-width: 530px) {{\r\n        .unsub {{\r\n          display: block;\r\n          padding: 8px;\r\n          margin-top: 14px;\r\n          border-radius: 6px;\r\n          background-color: #057d35;\r\n          text-decoration: none !important;\r\n          font-weight: bold;\r\n        }}\r\n        /* Más mejoras con las consultas de medios */\r\n        .col-lge {{\r\n          max-width: 100% !important;\r\n        }}\r\n      }}\r\n      @media screen and (min-width: 531px) {{\r\n        .col-sml {{\r\n          max-width: 27% !important;\r\n        }}\r\n        .col-lge {{\r\n          max-width: 73% !important;\r\n        }}\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body\r\n    style=\"margin: 0; padding: 0; word-spacing: normal; background-color: #eee\"\r\n  >\r\n    <div\r\n      role=\"article\"\r\n      aria-roledescription=\"email\"\r\n      lang=\"en\"\r\n      style=\"\r\n        text-size-adjust: 100%;\r\n        -webkit-text-size-adjust: 100%;\r\n        -ms-text-size-adjust: 100%;\r\n        background-color: #eee;\r\n      \"\r\n    >\r\n      <!-- ===== Outer Escafold ===== -->\r\n      <table\r\n        role=\"presentation\"\r\n        style=\"width: 100%; border: none; border-spacing: 0\"\r\n      >\r\n        <tr>\r\n          <td align=\"center\" style=\"padding: 0\">\r\n            <!-- ===== Ghost Table ===== -->\r\n            <!--[if mso]>\r\n            <table role=\"presentation\" align=\"center\" style=\"width:600px;\">\r\n            <tr>\r\n            <td>\r\n            <![endif]-->\r\n\r\n            <!-- ===== Container ===== -->\r\n            <table\r\n              role=\"presentation\"\r\n              style=\"\r\n                width: 94%;\r\n                max-width: 600px;\r\n                border: none;\r\n                border-spacing: 0;\r\n                text-align: left;\r\n                font-family: Arial, sans-serif;\r\n                font-size: 16px;\r\n                line-height: 22px;\r\n                color: #363636;\r\n              \"\r\n            >\r\n              <!-- ===== Header ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 40px 30px 30px 30px;\r\n                    text-align: center;\r\n                    font-size: 24px;\r\n                    font-weight: bold;\r\n                  \"\r\n                >\r\n                  <a\r\n                    href=\"#\"\r\n                    style=\"text-decoration: none\"\r\n                    ><img\r\n                      src=\"https://icones.pro/wp-content/uploads/2021/05/icones-de-messagerie-vert.png\"\r\n                      width=\"165\"\r\n                      alt=\"Logo\"\r\n                      style=\"\r\n                        width: 165px;\r\n                        max-width: 80%;\r\n                        height: auto;\r\n                        border: none;\r\n                        text-decoration: none;\r\n                        color: #ffffff;\r\n                      \"\r\n                    />\r\n                  </a>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Header ===== -->\r\n\r\n              <!-- ===== Content Row 1 ===== -->\r\n              <tr>\r\n                <td style=\"padding: 30px; background-color: #ffffff\">\r\n                  <h1\r\n                    style=\"\r\n                      margin-top: 0;\r\n                      margin-bottom: 16px;\r\n                      font-size: 26px;\r\n                      line-height: 32px;\r\n                      font-weight: bold;\r\n                      letter-spacing: -0.02em;\r\n                    \"\r\n                  >\r\n                    Reestablecer contrasena\r\n                  </h1>\r\n                  <p style=\"margin: 0\">\r\n                    Por favor, restablezca su contrasena haciendo\r\n                    <a\r\n                      href=\"{HtmlEncoder.Default.Encode(callbackUrl)}\"\r\n                      style=\"color: #057d35; text-decoration: underline\"\r\n                      >click aqui</a\r\n                    >\r\n                  </p>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Content Row 1 ===== -->\r\n\r\n              <!-- ===== Full Width Image ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 0 0 12px 0;\r\n                    font-size: 24px;\r\n                    line-height: 28px;\r\n                    font-weight: bold;\r\n                  \"\r\n                >\r\n                  <div style=\"text-decoration: none\">\r\n                    <img\r\n                      src=\"https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1170&q=80\"\r\n                      width=\"600\"\r\n                      alt=\"\"\r\n                      style=\"\r\n                        width: 100%;\r\n                        height: auto;\r\n                        display: block;\r\n                        border: none;\r\n                        text-decoration: none;\r\n                        color: #363636;\r\n                      \"\r\n                    />\r\n                  </div>\r\n                </td>\r\n              </tr>\r\n              <!-- ===== Full Width Image ===== -->\r\n\r\n              <!-- ===== Footer Section ===== -->\r\n              <tr>\r\n                <td\r\n                  style=\"\r\n                    padding: 30px;\r\n                    text-align: center;\r\n                    font-size: 12px;\r\n                    background-color: #057d35;\r\n                    color: #cccccc;\r\n                  \"\r\n                >\r\n                  <!-- ===== Footer Content  ===== -->\r\n                  <p style=\"margin: 0 0 8px 0\">\r\n                    <a href=\"#\" style=\"text-decoration: none\"\r\n                      ><img\r\n                        src=\"https://www.edigitalagency.com.au/wp-content/uploads/facebook-icon-white-png.png\"\r\n                        width=\"40\"\r\n                        height=\"40\"\r\n                        alt=\"f\"\r\n                        style=\"display: inline-block; color: #cccccc\"\r\n                    /></a>\r\n                    <a href=\"#\" style=\"text-decoration: none\"\r\n                      ><img\r\n                        src=\"https://icon-library.com/images/twitter-icon-black-and-white/twitter-icon-black-and-white-12.jpg\"\r\n                        width=\"40\"\r\n                        height=\"40\"\r\n                        alt=\"t\"\r\n                        style=\"display: inline-block; color: #cccccc\"\r\n                    /></a>\r\n                  </p>\r\n                  <p style=\"margin: 0; font-size: 14px; line-height: 20px\">\r\n                    &reg; 2023 - Gestion de practicas profesionales en la UTP<br />\r\n                  </p>\r\n                  <!-- ===== End Footer Content  ===== -->\r\n                </td>\r\n              </tr>\r\n              <!-- ===== End Footer Section ===== -->\r\n            </table>\r\n            <!-- ===== End Container ===== -->\r\n\r\n            <!--[if mso]>\r\n            </td>\r\n            </tr>\r\n            </table>\r\n            <![endif]-->\r\n            <!-- ===== Ghost Table ===== -->\r\n          </td>\r\n        </tr>\r\n      </table>\r\n      <!-- ===== End Outer Escafold ===== -->\r\n    </div>\r\n  </body>\r\n</html>\r\n");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
