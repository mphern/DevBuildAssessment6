using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PutMeOnTheList.Controllers
{
    public class APIController : Controller
    {
        const string userAgent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv: 47.0) Gecko / 20100101 Firefox / 47.0";

        // GET: API
        public ActionResult GetCharacter(string characterNumber)
        {
            


            return View();
        }
    }
}