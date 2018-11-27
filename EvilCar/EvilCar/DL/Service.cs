using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Service
    {
        private string nameofService;
        private int pricePerBooking;
        private string description;

        public string Name { get => nameofService; set => nameofService = value; }
        public int Pricing { get => pricePerBooking; set => pricePerBooking = value; }
        public string Description { get => description; set => description = value; }

    }
}
