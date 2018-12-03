using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class UserManager
    {
        XmlManager xmlManager;
        LoginManager loginManager;
        EmailManager emailManager = new EmailManager();
        Services userService = new Services();

        public UserManager(string xmlPath)
        {
            this.xmlManager = new XmlManager(xmlPath);
            this.loginManager = new LoginManager();
        }

        #region User Management
        /*User Management Functions
         * Handle input and communication with user
         * interface between user communication and xml "database" conversion
        */
        //Create new user based on userType passed as parameter
        public EvilCarUser newUser(EvilCarUser.UserType type)
        {
            EvilCarUser newUser = new EvilCarUser();

            Console.WriteLine("Please enter their first name.");
            newUser.FirstName = Console.ReadLine();
            Console.WriteLine("Enter their last Name.");
            newUser.LastName = Console.ReadLine();
            newUser.UserName = userService.generateUsername(newUser.FirstName, newUser.LastName);
            Console.WriteLine("Automatically generate username is, {0}.", newUser.UserName);

            newUser.Type = type;
            newUser.UserID = Guid.NewGuid();

            if(type != EvilCarUser.UserType.CUSTOMER)
            {
                newUser.Password = loginManager.encodePassword("StartPassword");
            }            

            xmlManager.newUserNode(newUser);

            Console.WriteLine("Succesfully created a new {0} User: {1}, {2} ({3})", type, newUser.LastName, newUser.FirstName, newUser.UserName);
            return newUser;
        }

        //Fetch information about a user, using their username
        //using the XMLManager to access the userDB xml file
        //handles spelling errors and invalid search requests
        public EvilCarUser fetchUserInfo(string type, string searchInput)
        {
            EvilCarUser user;

            //try to get user info based on entered searchValue (username) 
            try
            {
                user = xmlManager.getUserInformation(type, searchInput);
                Console.WriteLine("Entry: First Name: {0}. Last Name: {1}. User Type: {2}.", user.FirstName, user.LastName, type);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Something went wrong. Please check your spelling.");
                return null;
            }
        }

        //Access User in DB file via their id and change their information
        //in case of empty id a username is required to find the user
        //userType is passed as parameter to ensure a user can't find users of types they're not supposed to view
        public async void changeUserInfo(string type, Guid id)
        {
            char selection;
            Guid userId = id;
            string newValue = "";
            string changeParam = "";

            if(id == Guid.Empty)
            {
                string userName;
                Console.WriteLine("Enter the username for the user whose information needs changing.");
                userName = Console.ReadLine();
                EvilCarUser user = xmlManager.getUserInformation(type, userName);
                userId = user.UserID;
            } 

            if(userId == Guid.Empty)
            {
                Console.WriteLine("User doesn't exist");
                return;
            }

            if (type != "customer")
            {
                Console.WriteLine("What info needs changining?\n 1.First Name 2.Last Name 3.User Name 4.Password");
            } else
            {
                Console.WriteLine("What info needs changining?\n 1.First Name 2.Last Name 3.User Name");
            }
           
            selection = Console.ReadKey().KeyChar;
            Console.WriteLine("\nEnter the new value.");
            if (selection == '1') {

                newValue = Console.ReadLine();
                changeParam = "firstName";
            } else if(selection == '2')
            {
                newValue = Console.ReadLine();
                changeParam = "lastName";
            } else if(selection == '3')
            {
                newValue = Console.ReadLine();
                changeParam = "userName";
            } else if(selection == '4' && type != "customer")
            {
                newValue = Console.ReadLine();
                changeParam = "password";
                newValue = loginManager.encodePassword(newValue);
            } else
            {
                Console.WriteLine("Invalid selection");
                return;
            }

            //Change entry in XML file use id to find user and param to change correct info
            xmlManager.changeInformationBasedOnGuid(type, userId.ToString(), changeParam, newValue);
            if (selection == '4')
            {
                await emailManager.sendMailToManager();
            }
        }

        public EvilCarUser deleteUser(string type, string userName)
        {
            try
            {
                EvilCarUser user = xmlManager.getUserInformation(type, userName);
                xmlManager.removeNode(type, "guid", user.UserID.ToString());
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Something went wrong. Please check your spelling.");
                return null;
            }
        }

        #endregion

    }
}
