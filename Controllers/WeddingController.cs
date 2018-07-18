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
    public class WeddingController : Controller
    {
        
        private WeddingContext _context;
        public WeddingController(WeddingContext context){
            _context = context;
        }

        public IActionResult AddNewWedding(){
            return View();
        }

        public IActionResult WeddingInfo(int id){
            Wedding wedding = _context.Weddings.SingleOrDefault(u => u.WeddingID == id);
                
            List <UserWedd> userWedd = _context.UserWedding
            .Where(uw => uw.UserWeddingID == id)
            .Include(uw => uw.User)
            .ToList();

            ViewBag.wedding = wedding;
            ViewBag.guests = userWedd;
                
            return View();
        }

        public IActionResult Create(Wedding wedding){

            int? UserID = HttpContext.Session.GetInt32("LogedUserID"); 
            if(UserID != null){
                Person dbUser = _context.Users.SingleOrDefault(u => u.UserID == UserID);
                
                if(ModelState.IsValid){
                    wedding.User = dbUser;
                    dbUser.Wedding.Add(wedding);
                    _context.Add(wedding);
                    _context.SaveChanges();
                    return RedirectToAction("Weddings", "Home");
                }
                else{
                    return View("AddNewWedding");
                }
            }
            else{
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult UNRSVP(int id){
            int? UserID = HttpContext.Session.GetInt32("LogedUserID"); 
            if(UserID != null){
                Person dbUser = _context.Users.SingleOrDefault(u => u.UserID == UserID);
                
                UserWedd userWedd = _context.UserWedding.SingleOrDefault(u => u.UserWeddingID == id);
                
                if(userWedd.User == dbUser){
                _context.Remove(userWedd);
                _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id){
            int? UserID = HttpContext.Session.GetInt32("LogedUserID"); 
            if(UserID != null){
                Person dbUser = _context.Users.SingleOrDefault(u => u.UserID == UserID);
                
                Wedding wedding = _context.Weddings.SingleOrDefault(u => u.WeddingID == id);
                
                if(wedding.User == dbUser){
                _context.Remove(wedding);
                _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RSVP(int id){
            int? UserID = HttpContext.Session.GetInt32("LogedUserID"); 
            if(UserID != null){
                Person dbUser = _context.Users.SingleOrDefault(u => u.UserID == UserID);
                
                Wedding wedding = _context.Weddings.SingleOrDefault(u => u.WeddingID == id);
                
                
                UserWedd uw = new UserWedd(){
                    User = dbUser,
                    Wedding = wedding
                };
                dbUser.UserWedd.Add(uw);
                wedding.UserWedd.Add(uw);
                _context.Add(uw);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


    }
}