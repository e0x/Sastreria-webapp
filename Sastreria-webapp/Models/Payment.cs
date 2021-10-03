using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SastreriaWebApp.Models
{
    public class Payment
    {

        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }


    }
}
