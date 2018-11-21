using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Services
    {
        private string nameofService;
        private int pricePerBooking;
        private string description;

        public Services(string name, int price)
        {
            nameofService = name;
            pricePerBooking = price;
        }

        public string Name { get => nameofService; set => nameofService = value; }
        public int Pricing { get => pricePerBooking; set => pricePerBooking = value; }
        public string Description { get => description; set => description = value; }

    }
}
