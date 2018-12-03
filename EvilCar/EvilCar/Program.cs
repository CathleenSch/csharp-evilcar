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

            Console.Write("Welcome to the evil car management portal. Please login to continue.\n");

            while(CurrentUser == null)
            {
                CurrentUser = loginManager.UserLogin();
            }


            if(CurrentUser.Type == EvilCarUser.UserType.ADMIN)
            {
                UserMenuAdminView menu = new UserMenuAdminView(CurrentUser.UserID);

                while (true)
                {
                    menu.Start();
                }

            } else if (CurrentUser.Type == EvilCarUser.UserType.FLEET_MANAGER)
            {
                UserMenuManagerView menu = new UserMenuManagerView(CurrentUser.UserID);

                while (true)
                {
                    menu.Start();
                }
            } else
            {
                Console.WriteLine("You don't have access to the admin or manager options. Please contact your system administator.");
            }

            Console.Read();
            
        }
    }
}
