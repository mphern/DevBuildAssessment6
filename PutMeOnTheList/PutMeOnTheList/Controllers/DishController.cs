using PutMeOnTheList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PutMeOnTheList.Controllers
{
    public class DishController : Controller
    {
        // GET: Dish
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitDish(Dish dish)
        {
            return View(dish);
        }
    }
}