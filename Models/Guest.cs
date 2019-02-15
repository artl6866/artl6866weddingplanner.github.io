using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace wedding.Models
{
    public class Guest
    {
        [Key]
        public int GuestId {get;set;}
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        public User Users {get;set;}
        public Wedding Weddings {get;set;}

    }
}