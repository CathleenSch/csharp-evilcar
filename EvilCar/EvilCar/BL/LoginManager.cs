using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL {
    class LoginManager {

        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Database\user.xml");
        XmlManager manager = new XmlManager(path);

        public EvilCarUser UserLogin()
        {
            EvilCarUser user = new EvilCarUser();
            string username;
            string password = null;

            Console.Write("Please enter your username: ");
            username = Console.ReadLine();
            Console.Write("Please enter your password: ");
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }

            user = manager.searchUser(username);

            if(decodePassword(user.Password) == password)
            {
                Console.WriteLine("Logged in!");
                return user;
            } else
            {
                Console.WriteLine("Invalid LogIn. Please try again.");
                return null;
            }
        }

        protected string decodePassword (string base64EncodedData) {
            var bytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public string encodePassword (string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}