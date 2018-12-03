using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class UserMenuManagerView
    {
        private Guid managerId;
        public UserMenuManagerView(Guid managerID)
        {
            this.managerId = managerID;
        }

        public void Start()
        {
            char userSelection;
            FleetManagerManager manager = new FleetManagerManager();

            Console.WriteLine("1. Customer Administration \t 2. Fleet Administration \t 3. Profile Changes \t 4. Exit");
            userSelection = Console.ReadKey().KeyChar;
            if (userSelection == '1')
            {
                Console.WriteLine("\n 1. New Customer \t 2. Info about a Customer \t 3. Update Customer \t 4. Estimate rental cost \t 5. Back to Main Menu");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                //NewAdmin or AdminInfo
                if (userSelection == '1')
                {
                    manager.CreateNewCustomer();
                }
                else if (userSelection == '2')
                {
                    manager.ReadCustomerInfo();
                }
                else if (userSelection == '3')
                {
                    manager.UpdateCustomerProfile();
                }
                else if (userSelection == '4')
                {
                    manager.MakeCostEstimation(managerId);
                }
                else
                {
                    Console.WriteLine("Back to Main Menu.");
                    Console.Read();
                    Start();
                }

            }
            else if (userSelection == '2')
            {
                Console.WriteLine("\n1. Add car \t 2 .Info about about own fleet \t 3. Main Menu");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                //NewFM InfoFM DeleteFM
                if (userSelection == '1')
                {
                    manager.AddNewCar(managerId);
                }
                else if (userSelection == '2')
                {
                    manager.getFleetOverview(managerId);
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
                Console.WriteLine("\n");
                manager.updateOwnProfile(managerId);

            }
            else if (userSelection == '4')
            {
                Console.WriteLine("Press any key to exit\n");
                Console.Read();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid.");
                Start();
            }

        }
    }
}
