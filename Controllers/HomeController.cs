using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        
        private WeddingContext _context;
        public HomeController(WeddingContext context){
            _context = context;
        }

        public IActionResult Index(){
            int? UserID = HttpContext.Session.GetInt32("LogedUserID");
            
            if(UserID != null){
                return RedirectToAction("Weddings");
            }
            else{
                return View();
            }

            return View();
        }

        public IActionResult Weddings()
        {
            List<Wedding> weddings = _context.Weddings
                .Include(wed => wed.User)
                .OrderByDescending(msg => msg.CreatedAt)
                .ToList();
            ViewBag.allWeddings = weddings;

            List<UserWedd> userWedding = _context.UserWedding
            .Include(uw => uw.User)
            .Include(uw => uw.Wedding)
            .ToList();
            ViewBag.userWedd = userWedding;

            int? UserID = HttpContext.Session.GetInt32("LogedUserID"); 
            if(UserID != null){
                Person dbUser = _context.Users.SingleOrDefault(u => u.UserID == UserID);
                ViewBag.user = dbUser;

                return View();
            }
            else{
                return RedirectToAction("Index");
            }
        }


        public IActionResult LogIn(string Email, string Password){
            ViewBag.error ="";

            Person dbUser = _context.Users.SingleOrDefault(u => u.Email == Email);           
            if(dbUser != null){
                var Hasher = new PasswordHasher<Person>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(dbUser, dbUser.Password, Password))
                {
                    //Handle success
                    HttpContext.Session.SetInt32("LogedUserID", dbUser.UserID);
                    return RedirectToAction("Weddings");
                }
                else{
                    ViewBag.error = "Please check the password or email";
                    return View("Index");
                }
            }
            else{
                ViewBag.error = "Please check the password or email";
                return View("Index");
            }
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(Person person, string ConfirmPass){

            ViewBag.error ="";

            if(ModelState.IsValid && person.Password == ConfirmPass){
                PasswordHasher<Person> Hasher = new PasswordHasher<Person>();
                person.Password = Hasher.HashPassword(person, person.Password);
                //Save your user object to the database
                _context.Add(person);
                _context.SaveChanges();

                Person ReturnedValue = _context.Users.SingleOrDefault(u => u.Email == person.Email); 

                HttpContext.Session.SetInt32("LogedUserID", ReturnedValue.UserID);
                
                return RedirectToAction("Weddings");
            }
            else{
                if(person.Password != ConfirmPass){
                    ViewBag.error = "Password did not match";
                }
                return View("Register");
            }
        }

        public IActionResult LogOff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
