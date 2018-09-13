using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ShippingDetails
    {
        [Required(ErrorMessage="Enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Delevary Adress")]
        [Display(Name="Adress")]
        public string Address { get; set; }
        
        [Required(ErrorMessage ="Enter your email")]
        [EmailAddress]
        public string Email { set; get; }

        [Required(ErrorMessage = "Enter your city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter your Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter your Telephone")]
        [Display(Name = "Telephone")]
        [Phone]
        public string Telephone { get; set; }
    }
}
