using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL {
    class User {
        private string username;
        private string role;
        

        protected register () {
            Console.WriteLine("Please enter your username:");
            username = Console.ReadLine();
            Console.WriteLine("This is your chosen username: " + username);
            Console.WriteLine("Is that correct? y/n");
            string answer = Console.ReadLine().ToLower();

            while (!string.Equals(answer, "y") || !string.Equals(answer, "yes")) {
                if (string.Equals(answer, "n") || string.Equals(answer, "no")) {
                    Console.WriteLine("Please enter your username:");
                    username = Console.ReadLine();
                    Console.WriteLine("This is your chosen username: " + username);
                    Console.WriteLine("Is that correct? y/n");
                    answer = Console.ReadLine().ToLower();
                } else {
                    Console.WriteLine("Please type 'y' if you want to use this username or 'n' if you wish to correct it.");
                    answer = Console.ReadLine().ToLower();
                }
            }

            
        }
    }
    
}