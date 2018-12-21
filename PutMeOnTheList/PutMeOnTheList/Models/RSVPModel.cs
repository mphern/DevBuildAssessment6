using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PutMeOnTheList.Models
{
    public class RSVPModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string AttendanceDate { get; set; }
        public string PlusOne { get; set; }
        [Required]
        public string Attending { get; set; }
        public string CharacterNumber { get; set; }
    }
}