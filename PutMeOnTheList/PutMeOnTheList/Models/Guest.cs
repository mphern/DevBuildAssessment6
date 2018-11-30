using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PutMeOnTheList.Models
{
    public class Guest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Attending { get; set; }
        public string PartyDate { get; set; }
        public string PlusOne { get; set; }
    }
}