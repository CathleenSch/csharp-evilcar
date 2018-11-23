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
        private static string pathUserXml = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\user.xml");
        private static string pathFleetXml = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\fleet.xml");
        FleetManager fleetManager;
        UserManager userManager;

        //Constructor
        //Initiate instance of all required managers
        public FleetManagerManager()
        {
            fleetManager = new FleetManager(pathFleetXml);
            userManager = new UserManager(pathUserXml);
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
            userManager.newUser(EvilCarUser.UserType.CUSTOMER);
        }

        public void ReadCustomerInfo()
        {
            Console.WriteLine("You are requesting info about a customer.");
            string searchInput;
            Console.WriteLine("Enter an userName to get their details.");
            searchInput = Console.ReadLine();
            Console.WriteLine("You requested info on {0}", searchInput);
            userManager.fetchUserInfo("customer", searchInput);
        }

        public void UpdateCustomerProfile()
        {
            //empty guid to trigger user selection in UserManager.changeUserInfo
            Byte[] bytes = new Byte[16];
            Guid emptyGuid = new Guid(bytes);

            Console.WriteLine("You choose to update a customer profile.");
            userManager.changeUserInfo("customer", emptyGuid);
        }

        public void CalculateCustomerFeeTotal()
        {

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
            userManager.changeUserInfo("customer", id);
        }
        #endregion

        #region administerFleet
        /* 
         * Region to Administer Own Fleet
         * - AddCarToFleet
         * - UpdateFleet
         * - RemoveCarFromFleet
         */
        public void AddNewCar(Guid branchGuid)
        {
            Console.WriteLine("You choose to add a new car to your fleet.");
            fleetManager.addCarToFleet(branchGuid);
            
        }

        public void getFleetOverview(Guid branchGuid)
        {
            Console.WriteLine("You choose to get information about the fleet you are managing.");
            fleetManager.getFleetOverview(branchGuid);
        }
        #endregion
    }
}
