using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PutMeOnTheList.Models
{
    public class Dish
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public string GlutenFree { get; set; }
        public string Vegan { get; set; }
        public string Nuts { get; set; }
    }
}