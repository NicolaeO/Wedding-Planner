using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace WeddingPlanner.Models
{
    public abstract class BaseEntity{}
    
    public class Person : BaseEntity
    {
        [Key]
        public int UserID { get; set; }
        
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set;}

        [DataType(DataType.Date)]
        public DateTime UpdatedAt {get; set;}

        public List<Wedding> Wedding { get; set; }
        public List<UserWedd> UserWedd { get; set; }
       
        public Person()
        {
            Wedding = new List<Wedding>();
            UserWedd = new List<UserWedd>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}