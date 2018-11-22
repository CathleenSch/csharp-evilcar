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

        //Constructor
        public AdminManager() : base(path)
        {            

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
            fetchUserInfo("admin");
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
            newUser(EvilCarUser.UserType.FLEET_MANAGER);
        }

        public void readFleetManagerInfo()
        {
            Console.WriteLine("You are requesting info about a Fleet Manager.");
            fetchUserInfo("manager");
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
            Branch newBranch = new Branch(name);
        }
        #endregion

        
        
    }
}
