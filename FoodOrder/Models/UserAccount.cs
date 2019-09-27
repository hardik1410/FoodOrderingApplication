using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodOrder.Models
{
    public class UserAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage ="Firstname is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "username is required.")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
    }
}