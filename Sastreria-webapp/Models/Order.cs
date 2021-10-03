using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SastreriaWebApp.Models
{
    public class Order
    {
        
        public Guid Id { get; set; }
        
        public DateTime DeliveryDate {get; set;}
        public double Cost { get; set; }

        public string Details { get; set; }

        public bool Complete { get; set; }

        public Client Client { get; set; }

        public IList<Payment> Payments { get; set; } = new List<Payment>();
    }
}
