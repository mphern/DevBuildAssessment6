using PutMeOnTheList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PutMeOnTheList.Controllers
{
    public class RSVPController : Controller
    {
        // GET: RSVP
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(Guest guest)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            ORM.Guests.Add(guest);
            ORM.SaveChanges();

            return View(guest);
        }
    }
}