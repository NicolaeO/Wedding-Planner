using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace WeddingPlanner.Models{

    public class Wedding : BaseEntity
    {
        [Key]
        public int WeddingID { get; set; }
        
        [Required]
        [MinLength(2)]
        public string Wedder1 { get; set; }
        
        [Required]
        [MinLength(2)]
        public string Wedder2 { get; set; }

        [Required]        
        [DataType(DataType.Date)]
        public DateTime WeddingDate {get; set;}
        
        [Required]
        public string Location {get; set;}

        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set;}

        [DataType(DataType.Date)]
        public DateTime UpdatedAt {get; set;}

        public int UserID {get; set;}
        public Person User {get; set;}

        public List<UserWedd> UserWedd { get; set; }
       
        public Wedding()
        {
            UserWedd = new List<UserWedd>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}