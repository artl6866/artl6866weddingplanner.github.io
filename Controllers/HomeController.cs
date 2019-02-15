using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using wedding.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace wedding.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext dbContext;
        public HomeController(WeddingContext context){
        dbContext = context;
    }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        // [HttpGet]
        // [Route("")]
        // public IActionResult Login()
        // {
        //     return View();
        // }
        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            User CurrentUser =dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            List<Wedding> Allweddings = dbContext.Weddings.Include(a => a.Guests).ToList();
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.Allweddings = Allweddings;
            return View ("Dashboard");

        }
        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("/register")]
        public IActionResult Register(RegisterUser newUser)
        {
            if (ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == newUser.regEmail))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }
            
                PasswordHasher<RegisterUser> Hasher = new PasswordHasher<RegisterUser>();
                User user = new User {
                    FirstName = newUser.regFirstName,
                    LastName = newUser.regLastName,
                    Email = newUser.regEmail,
                    Password = Hasher.HashPassword(newUser, newUser.regPassword)
                };
                dbContext.Add(user);
                dbContext.SaveChanges();

                // reach into db context and find the new users id that just registered
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                int? UserId = HttpContext.Session.GetInt32("UserId");

                return RedirectToAction("Dashboard");
            }
            else
            {
                System.Console.WriteLine("=========================");
                System.Console.WriteLine("Validation failed Register");
                System.Console.WriteLine("=========================");
                return View("Index");
            }
            
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                var userindb = dbContext.Users.FirstOrDefault(u => u.Email == user.logEmail);
                if(userindb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email and/or Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, userindb.Password, user.logPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email and/or Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", userindb.UserId);
                int ? UserId = HttpContext.Session.GetInt32("UserId");
                System.Console.WriteLine(UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                System.Console.WriteLine("=========================");
                System.Console.WriteLine("Validation failed Login");
                System.Console.WriteLine("=========================");
                return View("Index");
            }
            
        }
        //Wedding
        [HttpGet]
        [Route("/addWedding")]
        public IActionResult AddWedding(ViewWedding wedding)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("Index");
            }
            return View("AddWedding");
        }
        [HttpPost]
        [Route("/createWedding")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId is null)
            {
                RedirectToAction("Index");
            }
            
            if(ModelState.IsValid)
            {
                Wedding wedding = new Wedding
                {
                    WedderOne = newWedding.WedderOne,
                    WedderTwo = newWedding.WedderTwo,
                    Date = newWedding.Date,
                    Address = newWedding.Address,
                    UserId = (int)UserId
                };
                dbContext.Add(wedding);
                dbContext.SaveChanges();

                // HttpContext.Session.SetInt32("logged_in_userId", newWedding.UserId);
                return RedirectToAction("Dashboard");

            }
            return View("AddWedding");
        }
        [HttpGet]
        [Route("delete/{WeddingId}")]
        public IActionResult Delete(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("Index");
            }
            Wedding delete = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == WeddingId);
            dbContext.Weddings.Remove(delete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("show/{WeddingId}")]
        public IActionResult Show(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId")== null)
            {
                return RedirectToAction("Index");
            }
            Wedding CurrentWedding = dbContext.Weddings
            .Include(wedding => wedding.Guests)
            .ThenInclude(guest => guest.Users)
            .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            ViewBag.CurrentWedding = CurrentWedding;
            return View(CurrentWedding);
        }
        [HttpGet]
        [Route("rsvp/{WeddingId}")]
        public IActionResult rsvp(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("Index");
            }
            User CurrentUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Wedding CurrentWedding = dbContext.Weddings
            .Include(wedding => wedding.Guests)
            .ThenInclude(guest => guest.Users)
            .SingleOrDefault(wedding => wedding.WeddingId ==WeddingId);
            Guest newguest = new Guest
            {
                UserId = CurrentUser.UserId,
                Users = CurrentUser,
                WeddingId = CurrentWedding.WeddingId,
                Weddings = CurrentWedding
            };
            CurrentWedding.Guests.Add(newguest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("unrsvp/{WeddingId}")]
        public IActionResult unrsvp(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("Index");
            }
            User CurrentUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Guest CurrentGuest = dbContext.Guests.SingleOrDefault(guest => guest.UserId == HttpContext.Session.GetInt32("UserId")&& guest.WeddingId == WeddingId);
            Wedding CurrentWedding = dbContext.Weddings
            .Include(wedding => wedding.Guests)
            .ThenInclude(guest => guest.Users)
            .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            CurrentWedding.Guests.Remove(CurrentGuest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}