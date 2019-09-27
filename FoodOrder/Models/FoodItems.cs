using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodOrder.Models
{
    public class FoodItems
    {
        [Key]
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        
        [ForeignKey("Cat")]
        public int CategoryId { get; set; }

        public FCategory Cat { get; set; }
    }
}