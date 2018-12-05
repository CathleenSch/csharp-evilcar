using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class FleetManagerManager
    {
        private static string pathCustomerXml = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\customer.xml");
        private static string pathFleetXml = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\fleet.xml");
        private static string pathUserrXml = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\user.xml");
        FleetManager fleetManager;
        UserManager customerManager;
        UserManager userManager;
        Services inputService = new Services();


        //Constructor
        //Initiate instance of all required managers
        public FleetManagerManager()
        {
            fleetManager = new FleetManager(pathFleetXml);
            customerManager = new UserManager(pathCustomerXml);
            userManager = new UserManager(pathUserrXml);
        }

        #region administerCustomers
        /*
         * Functions needed to administer Customers
         * - CreateNewCustomer
         * - UpdateCustomerProfile
         * - ReadCustomerInfo
         */
        public void CreateNewCustomer()
        {
            Console.WriteLine("You are creating a new Customer Profile.");
            customerManager.newUser(EvilCarUser.UserType.CUSTOMER);
        }

        //Get Customer information from customer DB
        public void ReadCustomerInfo()
        {
            string searchInput;

            Console.WriteLine("You are requesting info about a customer.");
            searchInput = inputService.validInput("Enter an userName to get their details.");
            Console.WriteLine("You requested info on {0}", searchInput);
            customerManager.fetchUserInfo("customer", searchInput);
        }

        public void UpdateCustomerProfile()
        {
            //empty guid to trigger user selection in UserManager.changeUserInfo
            Byte[] bytes = new Byte[16];
            Guid emptyGuid = new Guid(bytes);

            Console.WriteLine("You choose to update a customer profile.");
            customerManager.changeUserInfo("customer", emptyGuid);
        }

        public void MakeCostEstimation(Guid managerGuid)
        {
            Console.WriteLine("You choose to make a cost estimation for a potential costumer.");
            fleetManager.estimateRentalCost(managerGuid);
        }
        #endregion

        #region administerSelf
        /*
         * Self service region.
         * - updateOwnProfile Information: firsName, lastName, userName, password
         */
        public void updateOwnProfile(Guid id)
        {
            Console.WriteLine("You choose to update your own profile.");
            userManager.changeUserInfo("manager", id);
        }
        #endregion

        #region administerFleet
        /* 
         * Region to Administer Own Fleet
         * - AddCarToFleet
         * - UpdateFleet
         * - RemoveCarFromFleet
         */
        public void AddNewCar(Guid managerGuid)
        {
            Console.WriteLine("You choose to add a new car to your fleet.");
            fleetManager.addCarToFleet(managerGuid);
            
        }

        public void getFleetOverview(Guid managerGuid)
        {
            Console.WriteLine("You choose to get information about the fleet you are managing.");
            fleetManager.getFleetOverview(managerGuid);
        }
        #endregion
    }
}
