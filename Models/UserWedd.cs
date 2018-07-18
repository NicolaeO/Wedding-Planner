using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace WeddingPlanner.Models{
    public class UserWedd : BaseEntity{
        [Key]
        public int UserWeddingID {get; set;}

        public int UserID {get; set;}
        public Person User {get; set;}

        public int WeddingID {get; set;}
        public Wedding Wedding {get; set;}
    }
    
}