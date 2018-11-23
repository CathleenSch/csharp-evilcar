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

        public void NewFleet(string name)
        {
            fleetName = name;
            fleetID = Guid.NewGuid();
        }

        public string Name { get => fleetName; set => fleetName = value; }
        public Guid FleetId { get => fleetID; set => fleetID = value; }

    }
}
