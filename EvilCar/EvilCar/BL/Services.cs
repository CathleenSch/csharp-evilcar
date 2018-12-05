using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL {
    class Services {
        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\user.xml");

        //Generate a valid and unique username
        //Check DB if first letter firstName + lastname exists
        //appeand first name letters till none left
        //in case of persisiting double with DB appand numbers
        public string generateUsername (string firstName, string lastName) {
            firstName = firstName.ToLower();
            string username = firstName[0] + lastName;
            int count = 1;

            XmlManager xml = new XmlManager(path);

            for (int i = 1; i < firstName.Length; i++) {
                try {
                    xml.searchSystemUser(username);
                } catch (Exception e) {
                    return username;
                }

                username = "";
                for (int j = 0; j <= i; j++) {
                    username = username + firstName[j];
                }
                username = username + lastName;
            }

            while (true) {
                username = firstName + lastName + count;
                try {
                    xml.searchSystemUser(username);
                } catch (Exception e) {
                    break;
                }
                count++;
            }

            return username;
        }

        //validate the user input using the desired insert message
        //keep asking till valid input is given
        public string validInput(string inputMessage)
        {
            Console.WriteLine(inputMessage);
            string searchInput = Console.ReadLine();
            while (!validStringInput(searchInput))
            {
                Console.WriteLine(inputMessage);
                searchInput = Console.ReadLine();
            }

            return searchInput;
        }

        //Validate an input string against null or empty input
        private bool validStringInput (string inputFromConsole)
        {
            if(String.IsNullOrEmpty(inputFromConsole))
            {
                Console.WriteLine("Empty input.");
                return false;
            } else
            {
                return true;
            }
        }

        public int validIntInput(string inputMessage)
        {
            int returnInt;

            Console.WriteLine(inputMessage);
            returnInt = validateIntInput(Console.ReadLine());
            while (returnInt == -1)
            {
                Console.WriteLine(inputMessage);
                returnInt = validateIntInput(Console.ReadLine());
            }

            return returnInt;
        }

        //Validate number input
        private int validateIntInput (string intNumber)
        {
            int j;

            if (Int32.TryParse(intNumber, out j))
            {
                return j;
            }  else
            {
                Console.WriteLine("String could not be parsed.");
                return -1;
            }
               
        }
    }
}
