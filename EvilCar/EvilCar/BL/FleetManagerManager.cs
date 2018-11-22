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
    class FleetManagerManager : UserManager
    {
        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\user.xml");

        //Constructor
        public FleetManagerManager() : base(path)
        {


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
            newUser(EvilCarUser.UserType.CUSTOMER);
        }

        public void ReadCustomerInfo()
        {
            Console.WriteLine("You are requesting info about a customer.");
            fetchUserInfo("customer");
        }

        public void UpdateCustomerProfile()
        {
            Console.WriteLine("You choose to update a customer profile.");
            changeUserInfo(id, "customer");
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
            changeUserInfo(id, "manager");
        }
        #endregion

        #region administerFleet
        public void addCar()
        {

        }

        public void updateFleet()
        {

        }
        #endregion
    }
}
