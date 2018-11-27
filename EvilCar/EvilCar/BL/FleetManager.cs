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
        public void estimateRentalCost(Guid branchGuid)
        {
            Console.WriteLine("Select the car your customer wants to rent.");
            Fleet fleet = xmlManager.getFleetInformation(branchGuid);
            List<Car> cars = xmlManager.getCarInformation(fleet.FleetId);
            Console.WriteLine("How long will the car be needed (in hours)?");
            int rentDuration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which services are required? Seperate by comma\n" +
                                "1.Spotify 2.Parker 3.Navigation 4.Massage");
            
        }
        #endregion
    }
}
