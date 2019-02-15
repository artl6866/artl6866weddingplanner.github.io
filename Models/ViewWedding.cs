using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using wedding.Models;

namespace wedding.Models
{
    public class ViewWedding
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
        // [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Must be letters only")]
        // [Display(Name = "Wedder One")]
        public string WedderOne {get;set;}

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
        // [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Must be letters only")]
        // [Display(Name = "Wedder Two")]
        public string WedderTwo {get;set;}

        [Required]
        [FutureDate]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        [Required]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        // [Display(Name = "Address")]
        public string Address {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


    }
    
}