using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string logEmail {get;set;}
        
        [Required]
        [DataType(DataType.Password)]
        public string logPassword {get;set;}
    }
}