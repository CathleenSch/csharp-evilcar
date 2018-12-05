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
        Services inputService = new Services();

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
            string searchInput;


            Console.WriteLine("You are requesting info about another Admin.");
            searchInput = inputService.validInput("Enter an userName to get their details.");
            Console.WriteLine("You requested info on {0}", searchInput);
            fetchUserInfo("admin", searchInput);
        }

        #endregion

        #region administerFleetManager
        /*
         * Includes all functions to do with the administration of fleet Managers
         * - CreateNewAFleetManager
         * - ReadfleetManagerInfos
         * - Delete FleetManager
         * - UpdateFleetManager
         */
        public void CreateNewFleetManager()
        {
            Console.WriteLine("You are creating a new Fleet Manager.");
            EvilCarUser manager = newUser(EvilCarUser.UserType.FLEET_MANAGER);
            try
            {
                fleetManager.assignFleetManager(manager.UserID);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                deleteUser("manager", manager.UserName);
            }
            
        }

        public void readFleetManagerInfo()
        {
            string searchInput;

            Console.WriteLine("You are requesting info about a Fleet Manager.");
            searchInput = inputService.validInput("Enter an userName to get their details.");
            Console.WriteLine("You requested info on {0}", searchInput);
            fetchUserInfo("manager", searchInput);
        }

        public void deleteFleetManager()
        {
            string userName;

            Console.WriteLine("You are abbout to premanently delete a fleet manager.");
            userName = inputService.validInput("Please enter the userName of the manager.");

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

        public void updateFleetManager()
        {

            Byte[] bytes = new Byte[16];
            Guid emptyGuid = new Guid(bytes);


            Console.WriteLine("You choose to update one of the fleet managers profiles.");
            changeUserInfo("manager", emptyGuid);
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
            name = inputService.validInput("Please enter a name for the new branch.");   
            Branch newBranch = new Branch();
            newBranch.Name = name;
            newBranch.BranchId = Guid.NewGuid();
            fleetManager.addNewBranch(newBranch);
        }
        #endregion

        
        
    }
}
