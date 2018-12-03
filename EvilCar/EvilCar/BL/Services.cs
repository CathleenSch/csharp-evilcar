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

        public bool validInput (string inputFromConsole)
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

        public int validIntInput (string intNumber)
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
