using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Customer : EvilCarUser
    {
     
        private Guid rentedCar;
        private int totalCostOfCurrentRental;
        private DateTime rentStartDate;
        private DateTime rentEndDate;

        public Customer(string firstName, string lastName): base(firstName, lastName)
        {
            userType = UserType.CUSTOMER;
        }

        public Guid RentedCar { get => rentedCar; set => rentedCar = value; }
        public int TotalCostOfCurrentRental { get => totalCostOfCurrentRental; set => totalCostOfCurrentRental = value; }
        public DateTime RentStartDate { get => rentStartDate; set => rentStartDate = value; }
        public DateTime RentEndDate { get => rentEndDate; set => rentEndDate = value; }
    }
}
