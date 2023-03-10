using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using EmployeeMVC.Repositories.Interface;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeMVC.Controllers;

public class AccountController : Controller
{
    private readonly AccountRepository repoAccount;
    private readonly IConfiguration configuration;

    public AccountController(AccountRepository repoAccount, IConfiguration configuration)
    {
        this.repoAccount = repoAccount;
        this.configuration = configuration;
    }
    public IActionResult Index()
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        //var account = repoAccount.GetAll();
        return View();
    }
    //public IActionResult Details(string NIK)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    var account = context.Accounts.Find(NIK);
    //    return View(account);
    //}
    //public IActionResult Create()
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    context.Accounts.ToList();
    //    return View();
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Create(Account account)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    context.Add(account);
    //    var result = context.SaveChanges();
    //    if (result > 0)
    //        return RedirectToAction(nameof(Index));
    //    return View();
    //}
    //public IActionResult Edit(string NIK)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    var account = context.Accounts.Find(NIK);
    //    return View(account);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(Account account)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    context.Entry(account).State = EntityState.Modified;
    //    var result = context.SaveChanges();
    //    if (result > 0)
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View();
    //}
    //public IActionResult Delete(string NIK)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    var account = context.Accounts.Find(NIK);
    //    return View(account);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Remove(string NIK)
    //{
    //    if (HttpContext.Session.GetString("email") == null)
    //    {
    //        return RedirectToAction("Unauthorized", "Error");
    //    }
    //    if (HttpContext.Session.GetString("role") != "Admin")
    //    {
    //        return RedirectToAction("Forbidden", "Error");
    //    }
    //    var account = context.Accounts.Find(NIK);
    //    context.Remove(account);
    //    var result = context.SaveChanges();
    //    if (result > 0)
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View();
    //}

    public IActionResult Register()
    {
        //var gender = new List<SelectListItem>
        //{ new SelectListItem
        //{
        //    Value = "0",
        //    Text = "Male"
        //},
        //new SelectListItem
        //{
        //    Value = "1",
        //    Text = "Female"
        //},
        //};
        //ViewBag.Gender = gender;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            var result = repoAccount.Register(registerVM);
            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM loginVM)
    {
        if (repoAccount.Login(loginVM))
        {
            var userdata = repoAccount.GetUserdata(loginVM.Email);
            var roles = repoAccount.GetRolesByNIK(loginVM.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signIn
                );

            var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
            //const string email = "email";
            //const string fullname = "fullname";
            //const string role = "role";

            HttpContext.Session.SetString("jwtoken", generateToken);
            //HttpContext.Session.SetString(email, userdata.Email);
            //HttpContext.Session.SetString(fullname, userdata.FullName);
            //HttpContext.Session.SetString(role, userdata.Role);

            return RedirectToAction(nameof(Index), "Home");
        }
        ModelState.AddModelError(string.Empty, "Account or Password Not Found!");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index), "Home");
    }
}
