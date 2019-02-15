using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using wedding.Models;

namespace wedding.Models
{
   public class RegisterUser
{
    // [Key]
    public int UserId {get;set;}
    [Required]
    [MinLength(2, ErrorMessage="Must be at least 2 Characters")]
    public string regFirstName {get;set;}
    [Required]
    [MinLength(2, ErrorMessage="Must be at least 2 Characters")]
    public string regLastName {get;set;}
    [Required]
    [EmailAddress]
    public string regEmail {get;set;}
    [Required]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    [DataType(DataType.Password)]
    public string regPassword {get;set;}
   
    // Will not be mapped to your users table!
    [NotMapped]
    [Compare("regPassword")]
    [DataType(DataType.Password)]
    public string regConfirm {get;set;}
}    
}