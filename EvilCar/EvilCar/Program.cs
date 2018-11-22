using EvilCar.BL;
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
            //AdminManager manager = new AdminManager();

            FleetManagerManager manager = new FleetManagerManager();

            Byte[] bytes = new Byte[16];
            Guid emptyGuid = new Guid(bytes);

            //manager.CreateNewAdmin();

            manager.CreateNewCustomer();

            Console.Read();
        }
    }
}
