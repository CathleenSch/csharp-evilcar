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
        Services inputServices = new Services();

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
        public void addCarToFleet(Guid managerGuid)
        {
            Car newCar = new Car();
            Fleet fleet = xmlManager.getFleetInformation(managerGuid);
            char userSelection;

            Console.WriteLine("What Type of Car are you adding? \n 1. City \t 2. SUV \t 3. Convertible \t 4. Limousine");
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

            newCar.PricePerHour = inputServices.validIntInput("\n What is the hourly fee for this car?");
           
            Console.WriteLine("\n Enter a brief description");
            newCar.CarDescription = Console.ReadLine();
           if(String.IsNullOrEmpty(newCar.CarDescription))
            {
                newCar.CarDescription = "";
            }

            newCar.CarStatus = Car.Status.FREE;

            newCar.CreateNewCar(newCar.PricePerHour, newCar.CarType, fleet.FleetId);

            xmlManager.newCarNode(newCar);

            Console.WriteLine("Succesfully created a new {0} car, priced {1} per hour", newCar.CarType, newCar.PricePerHour);
        }

        //returns a fleet overview based on the guid of the logged in manager
        public void getFleetOverview(Guid managerGuid)
        {
            Fleet fleet = xmlManager.getFleetInformation(managerGuid);
            Console.WriteLine("Your branch is called: {0}", fleet.Name);
            Console.WriteLine("This is a list of all cars in your fleet.");
            List<Car> cars = xmlManager.getCarInformation(fleet.FleetId);
            foreach( Car c in cars)
            {
                Console.WriteLine("Status: {0} \t Type: {1} \t  Price: {2}", c.CarStatus, c.CarType, c.PricePerHour);
            }
            
        }

        //adds a branch
        public void addNewBranch(Branch branch)
        {
            xmlManager.newBranchNode(branch);
            Console.WriteLine("Successfully added branch {0}", branch.Name);
        }

        //assigns a fleet manager
        //puts the manager guid as references in the fleet DB
        public void assignFleetManager(Guid managerId)
        {
            string searchInput = inputServices.validInput("To which branch would you like to assign this manager?");
            
            try
            {
                xmlManager.newManagerNode(managerId, searchInput);
                Console.WriteLine("Successfully assigned this manager");
            } catch (Exception ex)
            {
                throw new System.MemberAccessException("Fleet not found");
            }

        }

        //estimates the possible rental costs
        //displays all cars available to the manager
        //asks for duration of booking and required sevices
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

            carSelection = inputServices.validIntInput("Select the number of the car to be rented.");
            int rentDuration = inputServices.validIntInput("How long will the car be needed (in hours)?");
            Console.WriteLine("Which services are required? Seperate by comma!\n");
            availableServices = xmlManager.availableServices();
            for (int i = 0; i < availableServices.Count; i++)
            {
                Console.Write("{0}. {1} ({2} per booking) \t", i, availableServices[i].Name, availableServices[i].Pricing);
            }
            Console.WriteLine("\n");
            string s1 = Console.ReadLine();
            if (String.IsNullOrEmpty(s1))
            {
                Console.WriteLine("No services selected.");
            } else
            {
                try
                {
                    int[] selectionArray = s1.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    for (int i = 0; i < selectionArray.Length; i++)
                    {
                        //in case of not a limousine tell user service is unavailable
                        if (availableServices[selectionArray[i]].Name == "Massage" && cars[carSelection].CarType != Car.TypeOfCar.LIMOUSINE)
                        {
                            Console.WriteLine("{0} Service unavailable.", availableServices[selectionArray[i]].Name);
                            serviceSelection[selectionArray[i]] = 0;
                        }
                        else
                        {
                            serviceSelection[selectionArray[i]] = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input. Start cost estimation over.");
                    return;
                }
            }
           

            totalCost = CalculateCost(cars[carSelection].PricePerHour, rentDuration, serviceSelection);
            Console.WriteLine("the estimated total for this rent process will be {0}", totalCost);

        }

        //Calculation happens here
        //get service prices from fleet DB
        //multiplies hourly Car Fee with selected duration
        //returns total
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
