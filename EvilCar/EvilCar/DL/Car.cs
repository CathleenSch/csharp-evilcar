using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Car
    {
        public enum Status { BOOKED, FREE };
        public enum TypeOfCar { CITY, CONVERTIBLE, SUV, LIMOUSINE  };

        private Guid carID;
        private int pricePerHour;
        private Status statusCar;
        private TypeOfCar typeCar;
        private Guid fleetGuid;
        private string carName;

        public void CreateNewCar(int price, TypeOfCar type, Guid fleetGuid)
        {
            carID = Guid.NewGuid();
            pricePerHour = price;
            statusCar = Status.FREE;
            typeCar = type;
            this.fleetGuid = fleetGuid;
        }

        public Status CarStatus { get => statusCar; set => statusCar = value; }
        public int PricePerHour { get => pricePerHour; set => pricePerHour = value; }
        public TypeOfCar CarType { get => typeCar; set => typeCar = value; }
        public Guid CarId { get => carID; }
        public Guid FleetGuid { get => fleetGuid; }
        public string CarDescription { get => carName; set => carName = value;  }

    }
}
