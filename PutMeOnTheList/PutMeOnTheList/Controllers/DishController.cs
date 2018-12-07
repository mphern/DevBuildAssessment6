using PutMeOnTheList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            PartyDBEntities ORM = new PartyDBEntities();
            ORM.Dishes.Add(dish);
            ORM.SaveChanges();

            return View(dish);
        }

        public ActionResult DishList()
        {

            PartyDBEntities ORM = new PartyDBEntities();
            ViewBag.DishList = ORM.Dishes.ToList();

            return View();

        }

        public ActionResult EditDish(int DishID)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            Dish dish = ORM.Dishes.Find(DishID);

            return View(dish);
        }

        public ActionResult SaveDishChanges(Dish updatedDish)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            Dish oldDish = ORM.Dishes.Find(updatedDish.DishID);

            oldDish.FirstName = updatedDish.FirstName;
            oldDish.LastName = updatedDish.LastName;
            oldDish.PhoneNumber = updatedDish.PhoneNumber;
            oldDish.DishName = updatedDish.DishName;
            oldDish.DishDescription = updatedDish.DishDescription;
            oldDish.GlutenFree = updatedDish.GlutenFree;
            oldDish.Vegan = updatedDish.Vegan;
            oldDish.Nuts = updatedDish.Nuts;

            ORM.Entry(oldDish).State = EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("DishList");

        }

        public ActionResult DeleteDish(int DishID)
        {
            PartyDBEntities ORM = new PartyDBEntities();
            Dish dish = ORM.Dishes.Find(DishID);

            ORM.Dishes.Remove(dish);
            ORM.SaveChanges();

            return RedirectToAction("DishList");
        }
    }
}