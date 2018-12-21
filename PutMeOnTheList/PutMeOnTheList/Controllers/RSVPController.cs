using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using PutMeOnTheList.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PutMeOnTheList.Controllers
{
    public class RSVPController : Controller
    {
        const string userAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

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
                        Attending = guest.Attending,
                        CharacterNumber = guest.CharacterNumber
                    };

                    var userManager = HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    user = userManager.Find(guest.EmailAddress, "NoPassNeeded");
                    if (user != null)
                    {

                        var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);


                        authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                        
                    }

                    AddCharacter(guest.CharacterNumber);

                    return RedirectToAction("Welcome", newGuest);
                }

                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

                return View();
            }
            return View();
        }

        public void AddCharacter(string characterNumber)
        {

            HttpWebRequest request = WebRequest.CreateHttp("https://anapioficeandfire.com/api/characters/" + characterNumber);

            request.UserAgent = userAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                string JsonData = data.ReadToEnd();
                JObject characterData = JObject.Parse("{Character:" + JsonData + "}");

                Character character = new Character();
                character.CharacterNumber = characterNumber;
                character.Name = characterData["Character"]["name"].ToString();
                character.Gender = characterData["Character"]["gender"].ToString();
                character.Culture = characterData["Character"]["culture"].ToString();
                character.Died = characterData["Character"]["died"].ToString();

                PartyDBEntities ORM = new PartyDBEntities();
                ORM.Characters.Add(character);
                ORM.SaveChanges();
            }



        }

        public string GetCharacterName(string characterNumber)
        {
            PartyDBEntities ORM = new PartyDBEntities();

            Character character = ORM.Characters.Find(characterNumber);

            string name = character.Name;

            return name;

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
            ViewBag.Characters = ORM.Characters.ToList();

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

        public ActionResult ViewCharacter(string CharacterNumber)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            ViewBag.Characters = ORM.Characters.ToList();
            ViewBag.CharacterNumber = CharacterNumber;

            return View();
        }
    }
}