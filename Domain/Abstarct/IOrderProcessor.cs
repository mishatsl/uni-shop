﻿using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstarct
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart,ShippingDetails shippingDetails);
    }
}
