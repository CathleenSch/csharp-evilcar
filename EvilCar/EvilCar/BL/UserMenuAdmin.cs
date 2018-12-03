using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class UserMenuAdminView
    {
        public void Start()
        {
            char userSelection;
            AdminManager manager = new AdminManager();

            Console.WriteLine("1.Admin Administration 2.Fleet Manager Administration 3.FleetAdministration 4.Exit");
            userSelection = Console.ReadKey().KeyChar;
            if (userSelection == '1')
            {
                Console.WriteLine("\n 1.New Admin 2.Info about a Admin 3. Change own information 4.Back to Main Menu");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                //NewAdmin or AdminInfo
                if (userSelection == '1') {
                    manager.CreateNewAdmin();
                } else if(userSelection == '2')
                {
                    manager.ReadAdminInfos();
                }
                else if (userSelection == '3')
                {
                    //manager.updateOwnProfile();
                }
                else
                {
                    Console.WriteLine("Back to Main Menu.");
                    Console.Read();
                    Start();
                }

            } else if (userSelection == '2') {
                Console.WriteLine("\n1.New Manager 2.Info about a Manager 3.Delete Manager 4.Change Manager Profile");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                //NewFM InfoFM DeleteFM
                if (userSelection == '1')
                {
                    manager.CreateNewFleetManager();
                }
                else if (userSelection == '2')
                {
                    manager.readFleetManagerInfo();
                } else if (userSelection == '3')
                {
                    manager.deleteFleetManager();
                } else if (userSelection == '4')
                {
                    manager.updateFleetManager();
                }
                else
                {
                    Console.WriteLine("Back to Main Menu.");
                    Console.Read();
                    Start();
                }
            }
            else if (userSelection == '3')
            {
                Console.WriteLine("\n 1.New Branch 2.Exit to main menu");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                if (userSelection == '1')
                {
                    manager.createNewBranch();
                }
                else
                {
                    Console.WriteLine("Back to Main Menu.");
                    Console.Read();
                    Start();
                }

            } else if (userSelection == '4')
            {
                Console.WriteLine("\nPress any key to exit");
                Console.Read();
                Environment.Exit(0);
            } else
            {
                Console.WriteLine("Invalid.");
                Start();
            }

        }
        

    }
}
