using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class UserMenuAdminView
    {
        private Guid managerId;
        public UserMenuAdminView(Guid managerID)
        {
            this.managerId = managerID;
        }

        public void Start()
        {
            char userSelection;
            AdminManager manager = new AdminManager();

            Console.WriteLine("1. Admin Administration \t 2. Fleet Manager Administration \t 3. FleetAdministration \t 4. Exit");
            userSelection = Console.ReadKey().KeyChar;
            if (userSelection == '1')
            {
                Console.WriteLine("\n 1. New Admin \t 2. Info about a Admin \t 3. Change own information \t 4. Back to Main Menu");
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
                    manager.updateOwnProfile(managerId);
                }
                else
                {
                    Console.WriteLine("Back to Main Menu.");
                    Console.Read();
                    Start();
                }

            } else if (userSelection == '2') {
                Console.WriteLine("\n1. New Manager \t 2. Info about a Manager \t 3. Delete Manager \t 4. Change Manager Profile");
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
                Console.WriteLine("\n 1. New Branch \t 2. Exit to main menu");
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
