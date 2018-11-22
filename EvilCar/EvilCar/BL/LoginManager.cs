using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL {
    class LoginManager {
        public void login () {
            string username;
            string password = null;

            Console.Write("Please enter your username: ");
             username = Console.ReadLine();
            Console.Write("Please enter your password: ");
            while (true) {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }



            Console.WriteLine("Logged in!");

        }

        private void dbLookup (string username, string password) {
            XmlManager lookup = new XmlManager("Database/user.xml");
            EvilCarUser user = lookup.getUserInformation();
        }

        private string decodePassword (string password) {
            var bytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}