using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using wedding.Models;

namespace wedding.Models
{
    public class Dashboard
    {
        public List<Wedding> Weddings {get;set;}
        public User User {get;set;}
    }
    
}