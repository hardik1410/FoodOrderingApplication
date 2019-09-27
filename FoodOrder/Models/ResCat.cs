using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FoodOrder.Models
{
    public class ResCat
    {
        [Key]
        [Column(Order = 1)]
        public int RId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CategoryId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int FoodId { get; set; }

        public int FoodPrice { get; set; }
        
        public Restaurant rest { get; set; }
        public FCategory cat { get; set; }
        public FoodItems foodit { get; set; }
        
}
}