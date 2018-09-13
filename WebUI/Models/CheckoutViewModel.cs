using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CheckoutViewModel
    {
        public ShippingDetails shippingDetails { set; get; }
        public Cart cart { set; get; }
    }
}