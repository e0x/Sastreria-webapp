using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SastreriaWebApp.Models
{
    public class OrderDetailsViewModel
    {
        public Guid Id { get; set; }
        
        public Nullable<Guid> ClientId { get; set; } 
        public DateTime DeliveryDate { get; set; }
        public double Cost { get; set; }

        public string Details { get; set; }

        public bool Complete { get; set; }

        public string Tel { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Amount { get; set; }

        public IList<Payment> Payments { get; set; }


    }
}
