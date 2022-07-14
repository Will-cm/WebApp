using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApp.Data.Context;
using WebApp.Models; //add

namespace WebApp.Controllers
{
    [Route("account")]  //add.
    public class AccesoController : Controller
    {
        private readonly DBContext _context;  //add

        public AccesoController(DBContext context)
        {
            _context = context;
        }

        [Route("")]  //add.
        [Route("~/")]  //add.
        [Route("index")]  //add.
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]  //add.
        public async Task<IActionResult> Login(string User, string pass)
        {
            /*
            if (User == null || _context.users == null)
            {
                return NotFound();
            }
            
            var account = await _context.users
                .FirstOrDefaultAsync(m => m.nombre == User);
            if (account == null)
            {
                return NotFound();
            }

            //return View(account);
            HttpContext.Session.SetString("User", User);  //add.
            return RedirectToAction("Index", "Home");  //add.
            */
            try
            {
                //var user = "admin";
                var account = await _context.users.FirstOrDefaultAsync(m => m.nombre == User && m.password == pass);

                if (account == null)
                {
                    ViewBag.msg = "Cuenta invalida";
                    return View("login");
                }
                else
                {
                    //ISession["User"] = user;
                    //HttpContext.Session.SetString("UserSession", User);  //add.
                    HttpContext.Session.SetString("UserSession",JsonSerializer.Serialize(account));  //add.
                    return RedirectToAction("Index", "Home");  //add.
                }
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            

        }

        [Route("welcome")]  //add.
        public IActionResult Welcome()  //add.
        {
            ViewBag.User = HttpContext.Session.GetString("UserSession");
            return View("Index", "Home");
        }

        [Route("logout")]  //add.
        public IActionResult Logout()  //addd.
        {
            HttpContext.Session.Remove("UserSession");
            return RedirectToAction("Login", "Acceso");  //add.
        }
    }
}
