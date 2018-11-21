using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Customer : User
    {
     
        private Guid rentedCar;
        private int totalCostOfCurrentRental;
        private DateTime rentStartDate;
        private DateTime rentEndDate;

        public Customer(string firstName, string lastName): base(firstName, lastName)
        {
            userType = UserType.CUSTOMER;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public Guid CustomerID { get => userID;}
        public Guid RentedCar { get => rentedCar; set => rentedCar = value; }
        public int TotalCostOfCurrentRental { get => totalCostOfCurrentRental; set => totalCostOfCurrentRental = value; }
        public DateTime RentStartDate { get => rentStartDate; set => rentStartDate = value; }
        public DateTime RentEndDate { get => rentEndDate; set => rentEndDate = value; }
    }
}
