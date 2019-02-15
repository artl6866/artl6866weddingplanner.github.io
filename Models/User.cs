using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using wedding.Models;

namespace wedding.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List <Guest> Guests {get;set;}
        public List <Wedding> Weddings {get;set;}

        // public User()
        // {
        //     Rsvps = new List <RSVP>();
        //     Weddings = new List <Wedding>(); 
        // }
    }
}