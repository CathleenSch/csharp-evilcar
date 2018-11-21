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
    class AdminManager
    {
        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) , @"Database\user.xml");
        private XmlManager userDatabaseManager;

        //Constructor
        public AdminManager()
        {            
            userDatabaseManager = new XmlManager(path);
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
            changeUserInfo(id, "admin");
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

        #region HelperFunctions
        //Some private Helper Functions
        //to handle business logic and communication with xml manager
        private void newUser(EvilCarUser.UserType type)
        {
            string firstName;
            string lastName;
            string userName;

            Console.WriteLine("Please enter their first name.");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter their last Name.");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter their user Name.");
            userName = Console.ReadLine();
            EvilCarUser newUser = new EvilCarUser(firstName, lastName, userName, type);

            userDatabaseManager.newUserNode(newUser);

            Console.WriteLine("Succesfully created a new {0} User: {1}, {2} ({3})", type, lastName, firstName, userName);
        }

        //Fetch information about a user, using their username
        //using the XMLManager to access the userDB xml file
        //handles spelling errors
        private void fetchUserInfo(string type)
        {
            string searchInput;
            string firstName;
            string lastName;

            Console.WriteLine("Enter an userName to get their details.");
            searchInput = Console.ReadLine();
            Console.WriteLine("You requested info on {0}", searchInput);

            try
            {
                firstName = userDatabaseManager.findInformation(type, "userName", searchInput, "firstName").ToString();
                lastName = userDatabaseManager.findInformation(type, "userName", searchInput, "lastName").ToString();

                Console.WriteLine("Entry: First Name: {0}. Last Name: {1}. User Type: {2}.", firstName, lastName, type);
            } catch(Exception ex)
            {
                Console.WriteLine("Something went wrong. Please check your spelling.");
            }

        }

        //Access User in DB file via their id and change their information
        private void changeUserInfo(Guid id, string type)
        {
            char selection;
            string newValue = "";
            string changeParam = "";

            Console.WriteLine("What info needs changining?\n 1.First Name 2.Last Name 3.User Name 4.Password");
            selection = Console.ReadKey().KeyChar;
            Console.WriteLine("\nEnter the new value.");
            switch (selection)
            {
                case '1':
                    newValue = Console.ReadLine();
                    changeParam = "firstName";
                    break;
                case '2':
                    newValue = Console.ReadLine();
                    changeParam = "lastName";
                    break;
                case '3':
                    newValue = Console.ReadLine();
                    changeParam = "userName";
                    break;
                case '4':
                    newValue = Console.ReadLine();
                    changeParam = "password";
                    break;
            }

            //Change entry in XML file use id to find user and param to change correct info
            userDatabaseManager.changeInformationBasedOnGuid(type, id.ToString().ToUpper(), changeParam, newValue);
            Console.WriteLine("Value changed.");
        }

        #endregion
        
    }
}
