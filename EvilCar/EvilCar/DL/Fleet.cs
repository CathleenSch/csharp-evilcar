using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Fleet
    {
        protected string fleetName;
        protected Guid fleetID;

        public Fleet(string name)
        {
            fleetName = name;
            fleetID = Guid.NewGuid();
        }
    }
}
