using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class FleetManager : XmlManager
    {
        XmlManager xmlManager;

        public FleetManager(string xmlPath) : base(xmlPath)
        {
            this.xmlManager = new XmlManager(xmlPath);
        }

        /* Fleet Management Functions
         * Handle input and communication with user
         * interface between user communication and xml "database" conversion
        */
        //Input dialog for a new Car
        //requires the branchGuid of the branch the logged in manager manages
        public void addCarToFleet(Guid branchGuid)
        {
            Car newCar = new Car();
            char userSelection;

            Console.WriteLine("What Type of Car are you adding? \n 1.City 2.SUV 3.Convertible 4.Limousine");
            userSelection = Console.ReadKey().KeyChar;
            if (userSelection == '1')
            {
                newCar.CarType = Car.TypeOfCar.CITY;
            } else if (userSelection == '2')
            {
                newCar.CarType = Car.TypeOfCar.SUV;
            }
            else if (userSelection == '3')
            {
                newCar.CarType = Car.TypeOfCar.CONVERTIBLE;
            }
            else if (userSelection == '4')
            {
                newCar.CarType = Car.TypeOfCar.LIMOUSINE;
            } else
            {
                Console.WriteLine("Invalid selection. We can't process your request.");
                return;
            }

            Console.WriteLine("\n What is the hourly fee for this car?");
            newCar.PricePerHour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Enter a brief description");
            newCar.CarDescription = Console.ReadLine();
            newCar.CarStatus = Car.Status.FREE;

            newCar.CreateNewCar(newCar.PricePerHour, newCar.CarType, branchGuid);

            xmlManager.newCarNode(newCar);

            Console.WriteLine("Succesfully created a new {0} car, priced {1} per hour", newCar.CarType, newCar.PricePerHour);
        }

        public void getFleetOverview(Guid branchGuid)
        {
            Fleet fleet = xmlManager.getFleetInformation(branchGuid);
            Console.WriteLine("Your branch is called: {0}", fleet.Name);
            Console.WriteLine("This is a list of all cars in your fleet.");
            List<Car> cars = xmlManager.getCarInformation(fleet.FleetId);
            foreach( Car c in cars)
            {
                Console.WriteLine("Status: {0} \t Type: {1} \t Price: {2}", c.CarStatus, c.CarType, c.PricePerHour);
            }
            
        }

        public void addNewBranch(Branch branch)
        {
            xmlManager.newBranchNode(branch);
            Console.WriteLine("Successfully added branch {0}", branch.Name);
        }

        public void assignFleetManager(Guid managerId)
        {
            Console.WriteLine("To which branch would you like to assign this manager?");
            string searchInput = Console.ReadLine();
            xmlManager.newManagerNode(managerId, searchInput);
            Console.WriteLine("Successfully assigned this manager");
        }

        #region Cost Estimation
        public void estimateRentalCost(Guid managerGuid)
        {
            int carSelection;
            int[] serviceSelection = new int[4];
            int totalCost;
            List<Service> availableServices;

            Console.WriteLine("Select the car your customer wants to rent.");
            Fleet fleet = xmlManager.getFleetInformation(managerGuid);
            List<Car> cars = xmlManager.getCarInformation(fleet.FleetId);
            for (int i=0;i<cars.Count;i++)
            {
                Console.WriteLine("{0}. Status: {1} \t Type: {2} \t Price: {3}", i, cars[i].CarStatus, cars[i].CarType, cars[i].PricePerHour);
            }
            Console.WriteLine("Select the number of the car to be rented.");
            carSelection = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How long will the car be needed (in hours)?");
            int rentDuration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which services are required? Seperate by comma!\n");
            availableServices = xmlManager.availableServices();
            for (int i = 0; i < availableServices.Count; i++)
            {
                Console.Write("{0}. {1} ({2} per booking) \t", i, availableServices[i].Name, availableServices[i].Pricing);
            }
            Console.WriteLine("\n");
            string s1 = Console.ReadLine();
            try
            {
                int[] selectionArray = s1.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                for(int i=0;i< selectionArray.Length;i++)
                {
                    serviceSelection[selectionArray[i]] = 1;
                }
            } catch(Exception ex)
            {
                Console.WriteLine("Invalid input. Start cost estimation over.");
                return;
            }
            totalCost = CalculateCost(cars[carSelection].PricePerHour, rentDuration, serviceSelection);
            Console.WriteLine("the estimated total for this rent process will be {0}", totalCost);

        }

        private int CalculateCost(int carFee, int duration, int[] services)
        {
            int total = carFee*duration;

            List<Service> availableServices = xmlManager.availableServices();
            for (int i=0;i<services.Length;i++)
            {
                if(services[i] == 1)
                {
                    total += availableServices[i].Pricing;
                }
            }

            return total;
        }
        #endregion
    }
}
