using EvilCar.BL;
using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar
{
    class Program
    {
        static void Main(string[] args)
        {
            EvilCarUser CurrentUser = null;
            LoginManager loginManager = new LoginManager();

            Console.Write("Welcome to the evil car management portal. Please login to continue.");

            while(CurrentUser == null)
            {
                CurrentUser = loginManager.UserLogin();
            }

            //CurrentUser.Type = EvilCarUser.UserType.ADMIN;
            //CurrentUser.UserID = new Guid("84b47e6c-4c99-4969-b13b-3063af8de9cf");

            if(CurrentUser.Type == EvilCarUser.UserType.ADMIN)
            {
                AdminUserMenu menu = new AdminUserMenu();
                menu.Start();
            } else if (CurrentUser.Type == EvilCarUser.UserType.FLEET_MANAGER)
            {
                ManagerUserMenu menu = new ManagerUserMenu(CurrentUser.UserID);
                menu.Start();
            } else
            {
                Console.WriteLine("You don't have access to the admin or manager options. Please contact your system administator.");
            }
            Console.Read();
            
        }
    }
}
