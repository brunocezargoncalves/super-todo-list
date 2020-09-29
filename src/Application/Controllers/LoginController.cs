using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Models;
using Repository.Interfaces;
using Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;   
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Application.Controllers
{    
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            // _configuration = configuration;
        }

        // public string GenerateToker(User user)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT").GetSection("Key").Value);

        //     var tokenDescriptor = new SecurityTokenDescriptor {
        //         Subject = new ClaimsIdentity(new Claim[]
        //         {
        //             new Claim(ClaimTypes.Name, user.Name.ToString()),
        //             new Claim(ClaimTypes.Email, user.Email.ToString()),
        //             new Claim(ClaimTypes.Role, user.Role.ToString())
        //         }),

        //         Expires = DateTime.UtcNow.AddHours(2),

        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };

        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }

        [AllowAnonymous]
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {            
            try
            {
                var authenticatedUser = _userRepository.Login(user.Email, Common.ComputeSha256Hash(user.Password));

                if(authenticatedUser != null)
                {
                    authenticatedUser.Password = "";
                    // authenticatedUser.Token = GenerateToker(authenticatedUser);
                    
                    var claims = new List<Claim>()
                    {
                        // Define o cookie
                        new Claim(ClaimTypes.Sid, authenticatedUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, authenticatedUser.Name),
                        new Claim(ClaimTypes.Email, authenticatedUser.Email),
                        new Claim(ClaimTypes.Role, authenticatedUser.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties{};
                    
                    // Cria o cookie                    
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "ToDo", authenticatedUser);                
                }
                
                Notification.Set(TempData, new Message() {
                    Text = "Credenciais inválidas!",
                    Type = NotificationType.danger
                });

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Notification.Set(TempData, new Message() {
                    Text = ex.Message,
                    Type = NotificationType.danger
                });

                return RedirectToAction("Index", "Login");
            }
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login", null);
        }
    }
}