using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.Models
{
    public class Restaurant
    {
        [Key]
        public int RId { get; set; }
        public string RName { get; set; }
        public string RLocation { get; set; }
        public string RContact { get; set; }
        public string OpenCloseTime { get; set; }

        public virtual ICollection<ResCat> rescat { get; set; }
        
        

    }
}