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

        public UserManager(string xmlPath)
        {
            this.xmlManager = new XmlManager(xmlPath);
        }

        #region HelperFunctions
        //Some private Helper Functions
        //to handle business logic and communication with xml manager
        protected void newUser(EvilCarUser.UserType type)
        {
            EvilCarUser newUser = new EvilCarUser();

            Console.WriteLine("Please enter their first name.");
            newUser.FirstName = Console.ReadLine();
            Console.WriteLine("Enter their last Name.");
            newUser.LastName = Console.ReadLine();
            Console.WriteLine("Enter their user Name.");
            newUser.UserName = Console.ReadLine();

            xmlManager.newUserNode(newUser);

            Console.WriteLine("Succesfully created a new {0} User: {1}, {2} ({3})", type, newUser.LastName, newUser.FirstName, newUser.UserName);
        }

        //Fetch information about a user, using their username
        //using the XMLManager to access the userDB xml file
        //handles spelling errors
        protected void fetchUserInfo(string type)
        {
            string searchInput;

            Console.WriteLine("Enter an userName to get their details.");
            searchInput = Console.ReadLine();
            Console.WriteLine("You requested info on {0}", searchInput);

            try
            {
                EvilCarUser user = getUserInformation(type, searchInput);
                Console.WriteLine("Entry: First Name: {0}. Last Name: {1}. User Type: {2}.", user.FirstName, user.LastName, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Please check your spelling.");
            }
        }

        //Access User in DB file via their id and change their information
        protected void changeUserInfo(string type)
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
            xmlManager.changeInformationBasedOnGuid(type, id.ToString().ToUpper(), changeParam, newValue);
            Console.WriteLine("Value changed.");
        }

        //find a user based on their userName
        private EvilCarUser getUserInformation(string type, string searchInput)
        {
            EvilCarUser user = new EvilCarUser();

            user.FirstName = xmlManager.findInformation(type, "userName", searchInput, "firstName").ToString();
            user.LastName = xmlManager.findInformation(type, "userName", searchInput, "lastName").ToString();
            user.UserID = new Guid(xmlManager.findInformation(type, "userName", searchInput, "guid"));
            user.UserName = searchInput;

            return user;
        }

        #endregion
    }
}
