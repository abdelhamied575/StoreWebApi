﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Entities.Order
{
    public class DeliveryMethod:BaseEntity<int>
    {

        public string ShortName { get; set; }
        public string Dscription { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }



    }
}
