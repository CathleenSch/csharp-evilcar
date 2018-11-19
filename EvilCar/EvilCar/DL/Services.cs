using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Services
    {
        private String nameofService;
        private int pricePerBooking;
        private String description;

        public String getNameOfService()
        {
            return nameofService;
        }

        public int getPricePerBooking()
        {
            return pricePerBooking;
        }

        public String getDescription()
        {
            return description;
        }

        public void setPricePerBooking(int price)
        {
            pricePerBooking = price;
        }

        public void setDescription(String description)
        {
            this.description = description
        }
    }
}
