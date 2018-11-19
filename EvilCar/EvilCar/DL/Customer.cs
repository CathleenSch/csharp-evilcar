using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Customer
    {
        private String firstName;
        private String lastName;
        private Guid customerID;
        private Guid rentedCar;
        private int totalCostOfCurrentRental;
        private DateTime rentStartDate;
        private DateTime rentEndDate;

        public Customer(String fName, String lName)
        {
            firstName = fName;
            lastName = lName;
            customerID = Guid.NewGuid();
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public Guid CustomerID { get => customerID;}
        public Guid RentedCar { get => rentedCar; set => rentedCar = value; }
        public int TotalCostOfCurrentRental { get => totalCostOfCurrentRental; set => totalCostOfCurrentRental = value; }
        public DateTime RentStartDate { get => rentStartDate; set => rentStartDate = value; }
        public DateTime RentEndDate { get => rentEndDate; set => rentEndDate = value; }
    }
}
