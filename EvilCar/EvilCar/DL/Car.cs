using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Car
    {
        public enum Status { booked, free };
        public enum TypeOfCar { City, Convertible, SUV, Limousine  };

        private Guid carID;
        private int pricePerHour;
        private Status statusCar;
        private TypeOfCar typeCar;

        public Car(int price, TypeOfCar type)
        {
            carID = Guid.NewGuid();
            pricePerHour = price;
            statusCar = Status.free;
            typeCar = type;
        }

        public Status CarStatus { get => statusCar; set => statusCar = value; }
        public int PricePerHour { get => pricePerHour; set => pricePerHour = value; }
        public TypeOfCar CarType { get => typeCar; }

    }
}
