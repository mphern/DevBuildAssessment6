using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PutMeOnTheList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PutMeOnTheList.Controllers
{
    public class RSVPController : Controller
    {
        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        // GET: RSVP
        public ActionResult RSVP()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RSVP(RSVPModel guest)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = guest.EmailAddress,
                    Email = guest.EmailAddress
                };

                var identityResult = await UserManager.CreateAsync(user, "NoPassNeeded");

                if(identityResult.Succeeded)
                {
                    Guest newGuest = new Guest()
                    {
                        FirstName = guest.FirstName,
                        LastName = guest.LastName,
                        AttendanceDate = guest.AttendanceDate,
                        EmailAddress = guest.EmailAddress,
                        PlusOne = guest.PlusOne,
                        Attending = guest.Attending
                    };

                    var userManager = HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    user = userManager.Find(guest.EmailAddress, "NoPassNeeded");
                    if (user != null)
                    {

                        var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);


                        authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                        
                    }

                    return RedirectToAction("Welcome", newGuest);
                }

                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

                return View();
            }
            return View();
        }

        public ActionResult Welcome(Guest guest)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            ORM.Guests.Add(guest);
            ORM.SaveChanges();

            return View(guest);
        }

        public ActionResult GuestList()
        {
            PartyDBEntities ORM = new PartyDBEntities();
            ViewBag.GuestList = ORM.Guests.ToList();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if(ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = userManager.Find(login.EmailAddress, "NoPassNeeded");
                if (user != null)
                {

                    var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);


                    authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid Email Address");
            return View();

        }

        public ActionResult LogOut(LoginModel login)
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}