using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using wedding.Models;

namespace wedding.Models
{
    public class Wedding 
    {
        [Key]
        public int WeddingId {get;set;}
        [Required]
        [FutureDate]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters")]

        public string WedderOne {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
        public string WedderTwo {get;set;}
        [Required]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        public string Address {get;set;}
        public int UserId {get;set;}
        public List <Guest> Guests {get;set;}
        
        
       

        
        // public Wedding()
        // {
        //     Users = new List <RSVP>();
        // }
        
    }
}