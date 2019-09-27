using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.Models
{
    public class FCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ResCat> rescat { get; set; }
        public virtual ICollection<FoodItems> foodit { get; set; }
    }
}