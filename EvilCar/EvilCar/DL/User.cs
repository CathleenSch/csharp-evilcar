using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class User
    {
        public enum UserType { CUSTOMER, ADMIN, FLEET_MANAGER };

        protected string firstName;
        protected string lastName;
        protected Guid userID;
        protected UserType userType;

        public User(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            userID = Guid.NewGuid();
        }
    }
}
