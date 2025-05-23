using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FileUploadMvcApp.Data;
using FileUploadMvcApp.Models;

namespace FileUploadMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        
        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                
                if (user != null && VerifyPassword(model.Password, user.PasswordHash))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email)
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "FileUpload");
                }
                
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kullanılıyor.");
                    return View(model);
                }
                
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(model);
                }
                
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password)
                };
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                
                return RedirectToAction("Index", "FileUpload");
            }
            
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
        
        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
    }
} 