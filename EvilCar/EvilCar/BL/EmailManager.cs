using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL
{
    class EmailManager
    {

        public async Task sendMailToManager()
        {
            Console.WriteLine("Sending update information");

            // random waiting time between 5-10 sec
            Random rnd = new Random();
            int value = rnd.Next(5, 11); // return a value between 5 and 10 inclusive
            value = value * 1000;

            await Task.Delay(value);

            Console.WriteLine("E-Mail notification has beeen send.");
        }
    }
}
