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
    class AdminManager : UserManager
    {
        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) , @"Database\user.xml");
        private static string fleetXmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\fleet.xml");
        FleetManager fleetManager;

        //Constructor
        public AdminManager() : base(path)
        {
            fleetManager = new FleetManager(fleetXmlPath);
        }

        #region administrateAdmins
        /*
         * Includes all functions to do with the administration of fellow administrators
         * - CreateNewAdmin
         * - ReadAdminInfos
         */
        public void CreateNewAdmin()
        {
            Console.WriteLine("You are creating a new Admin.");
            newUser(EvilCarUser.UserType.ADMIN);
        }

        public void ReadAdminInfos()
        {
            Console.WriteLine("You are requesting info about another Admin.");
            string searchInput;
            Console.WriteLine("Enter an userName to get their details.");
            searchInput = Console.ReadLine();
            Console.WriteLine("You requested info on {0}", searchInput);
            fetchUserInfo("admin", searchInput);
        }

        #endregion

        #region administerFleetManager
        /*
         * Includes all functions to do with the administration of fleet Managers
         * - CreateNewAFleetManager
         * - ReadfleetManagerInfos
         * - ChangeFleetManagerPassword
         */
        public void CreateNewFleetManager()
        {
            Console.WriteLine("You are creating a new Fleet Manager.");
            EvilCarUser manager = newUser(EvilCarUser.UserType.FLEET_MANAGER);
            fleetManager.assignFleetManager(manager.UserID);
        }

        public void readFleetManagerInfo()
        {
            Console.WriteLine("You are requesting info about a Fleet Manager.");
            string searchInput;
            Console.WriteLine("Enter an userName to get their details.");
            searchInput = Console.ReadLine();
            Console.WriteLine("You requested info on {0}", searchInput);
            fetchUserInfo("manager", searchInput);
        }

        public void deleteFleetManager()
        {
            Console.WriteLine("You are abbout to premanently delete a fleet manager.");
            Console.WriteLine("Please enter the userName of the manager.");
            string userName = Console.ReadLine();

            EvilCarUser user = fetchUserInfo("manager", userName);

            if(user != null)
            {
                try
                {
                    fleetManager.removeNode("manager", "guid", user.UserID.ToString());
                    deleteUser("manager", user.UserName);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                
                Console.WriteLine("Successfully deleted {0} {1}", user.FirstName, user.LastName);
            }            
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
            changeUserInfo("admin", id);
        }
        #endregion

        #region administerBranch
        /*
         * Branch administration
         * - createNewBranch
         */
        public void createNewBranch()
        {
            string name;


            Console.WriteLine("You choose to add a new branch.\n Please enter a name for the new branch.");
            name = Console.ReadLine();
            Branch newBranch = new Branch();
            newBranch.Name = name;
            newBranch.BranchId = Guid.NewGuid();
            fleetManager.addNewBranch(newBranch);
        }
        #endregion

        
        
    }
}
